Shader "Fur/Shell/Lit"
{

Properties
{
    [Header(Basic)][Space]
    [MainColor] _BaseColor("Color", Color) = (0.5, 0.5, 0.5, 1)
    _AmbientColor("Ambient Color", Color) = (0.0, 0.0, 0.0, 1)
    _BaseMap("Albedo", 2D) = "white" {}
    [Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.5
    _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5

    [Header(Fur)][Space]
    _FurMap("Fur", 2D) = "white" {}
    [Normal] _NormalMap("Normal", 2D) = "bump" {}
    _NormalScale("Normal Scale", Range(0.0, 2.0)) = 1.0
    [IntRange] _ShellAmount("Shell Amount", Range(1, 14)) = 14
    _ShellStep("Shell Step", Range(0.0, 0.01)) = 0.001
    _AlphaCutout("Alpha Cutout", Range(0.0, 1.0)) = 0.2
    _FurScale("Fur Scale", Range(0.0, 10.0)) = 1.0
    _Occlusion("Occlusion", Range(0.0, 1.0)) = 0.5
    _BaseMove("Base Move", Vector) = (0.0, -0.0, 0.0, 3.0)
    _WindFreq("Wind Freq", Vector) = (0.5, 0.7, 0.9, 1.0)
    _WindMove("Wind Move", Vector) = (0.2, 0.3, 0.2, 1.0)

    [Header(Lighting)][Space]
    _RimLightPower("Rim Light Power", Range(1.0, 20.0)) = 6.0
    _RimLightIntensity("Rim Light Intensity", Range(0.0, 1.0)) = 0.5
    _ShadowExtraBias("Shadow Extra Bias", Range(-1.0, 1.0)) = 0.0
	
	[Space(20)] [Header(_OutlineColor)]
	_OutlineColor("OutLine Color", Color) = (1,1,1,1)
	_OutlineThickness("OutLine Thick", float) = 1
}

SubShader
{
    Tags
    {
        "RenderType" = "Opaque"
        "RenderPipeline" = "UniversalPipeline"
        "UniversalMaterialType" = "Lit"
        "IgnoreProjector" = "True"
    }

    LOD 100

    ZWrite On
    Cull Back

    Pass
    {
        Name "ForwardLit"
        Tags { "LightMode" = "UniversalForward" }

        HLSLPROGRAM
        // URP のキーワード
#if (UNITY_VERSION >= 202111)
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile_fragment _ _LIGHT_LAYERS
#else
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#endif
        #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
        #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION

        // Unity のキーワード
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile_fog

        #pragma prefer_hlslcc gles
        #pragma exclude_renderers d3d11_9x
        #pragma target 2.0
        #pragma vertex vert
        #pragma require geometry
        #pragma geometry geom
        #pragma fragment frag
        #include "./Lit.hlsl"
        ENDHLSL
    }

    Pass
    {
        Name "DepthOnly"
        Tags { "LightMode" = "DepthOnly" }

        ZWrite On
        ColorMask 0

        HLSLPROGRAM
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma require geometry
        #pragma geometry geom
        #pragma fragment frag
        #include "./Depth.hlsl"
        ENDHLSL
    }

    Pass
    {
        Name "DepthNormals"
        Tags { "LightMode" = "DepthNormals" }

        ZWrite On

        HLSLPROGRAM
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma require geometry
        #pragma geometry geom
        #pragma fragment frag
        #include "./DepthNormals.hlsl"
        ENDHLSL
    }

    Pass
    {
        Name "ShadowCaster"
        Tags { "LightMode" = "ShadowCaster" }

        ZWrite On
        ZTest LEqual
        ColorMask 0

        HLSLPROGRAM
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma require geometry
        #pragma geometry geom
        #pragma fragment frag
        #include "./Shadow.hlsl"
        ENDHLSL
    }

	Pass
        {
			Name "OutLine"
			Tags{"LightMode"= "OutLine"}
			ZWrite Off
			Cull Front

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct VertexInput
            {
                float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
            };

            struct VertexOutput
            {
                float4 positionCS : SV_POSITION;
            };

			half4 _OutlineColor;
			half _OutlineThickness;

            VertexOutput vert (VertexInput IN)
            {
                VertexOutput o;
				
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				/*
				// 월드 노말 방식, 카메라 거리에 따른 원근 교정 X
				float3 positionWS = TransformObjectToWorld(IN.vertex.xyz);
                float3 normalWS = mul(UNITY_MATRIX_M, IN.normal.xyz);
                positionWS += normalWS * _OutlineThickness;
                o.vertex = TransformWorldToHClip(positionWS);
				*/

				
				// 월드 노멀 방식, 카메라 거리에 따른 원근 교정 O
				float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);
				float3 normalWS = mul(UNITY_MATRIX_M, IN.normalOS.xyz);
				float distToCam = length(_WorldSpaceCameraPos - positionWS);
				positionWS += normalWS * _OutlineThickness * distToCam;
				o.positionCS = TransformWorldToHClip(positionWS);
				

				/*
				// 스크린 노멀 방식, 카메라 거리에 따른 원근 교정 O
				o.vertex = TransformObjectToHClip(IN.vertex.xyz);
                float3 clipNormal = TransformObjectToHClip(IN.normal * 1); // 100을 곱하는 이유 : 1 이하로 작은 값인 노말 방향은 클립스페이스의 퍼스펙티브가 적용되면서 화면 바깥쪽에서는 방향이 뒤집히는 왜곡이 발생하므로 클립 변환 전에 방향이 뒤집히지 않을 정도로 충분히 큰 벡터로 가공
                clipNormal = normalize(float3(clipNormal.xy, 0)); // 매우 큰 방향값을 정규화
                o.vertex.xyz += normalize(clipNormal) * _OutlineThickness * o.vertex.w; // 클립공간의 w 값은 카메라 공간의 z값과 같다. 즉, 카메라로부터 버택스까지의 거리
                */

				float4 _ClipCameraPos = mul(UNITY_MATRIX_VP, float4(_WorldSpaceCameraPos.xyz, 1));
				half _Offset_Z;
				#if defined(UNITY_REVERSED_Z)
                    //v.2.0.4.2 (DX)
                    _Offset_Z = UNITY_NEAR_CLIP_VALUE * -0.01;
                #else
                    //OpenGL
                    _Offset_Z = UNITY_NEAR_CLIP_VALUE * 0.01;
                #endif

				o.positionCS.z = o.positionCS.z + _Offset_Z * _ClipCameraPos.z;
				return o;
            }

            half4 frag (VertexOutput i) : SV_Target
            {
				return _OutlineColor;
            }
            ENDHLSL
        }
}

}
