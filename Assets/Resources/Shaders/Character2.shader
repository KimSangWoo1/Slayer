Shader "URP/Character2"
{
    Properties
    {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)

		[Space(20)] [Header(_EmissionColor)]
		[Toggle(_EMISSION)] _EmissionToggle ("Emission", Float) = 0
		_EmissionMap ("EmissionMap", 2D) = "white" {}
		[HDR] _EmissionColor ("Emission Color", Color) = (0.0, 0.0, 0.0)
		_EmissionPower("Emission Power", Range(0, 1)) = 0

		[Space(20)] [Header(_ShadowDepthTreshold)] 
		_ShadowDepthTreshold("Shadow Depth Threshold", Range(0, 1)) = 0.25

		[Space(20)] [Header(_SpecColor)]
		[Toggle(_SPECULAR)] _SpecularToggle ("Specular", Float) = 0
		[HDR] _SpecColor ("Specular Color", Color) = (0.2, 0.2, 0.2)
		_SpecPower("Specular Power (Smoothness)", Float) = 10

		[Space(20)] [Header(_FresnelColor)]
		[Toggle(_FRESNEL)] _FresnelToggle ("Fresnel", Float) = 0
		[HDR]_FresnelColor("Fresnel Color", Color) = (0.2, 0.2, 0.2)
        _FresnelPower("Fresnel Power", Float) = 4
		
		[Space(20)] [Header(_Cel)]
		_CelTreshold("Cel Threshold", Range(1., 20.)) = 2

		[Space(20)] [Header(_OutlineColor)]
		_OutlineColor("OutLine Color", Color) = (1,1,1,1)
		_OutlineThickness("OutLine Thick", float) = 1

		[Space(20)] [Header(_NORMALMAP)]
		[Toggle(_NORMALMAP)] _NormalMapToggle ("Normal Mapping", Float) = 0
		[NoScaleOffset] _BumpMap("Normal Map", 2D) = "bump" {}
		_NormalStrength("Normal Strength",float) = 1

		[Space(20)] [Header(_ALPHATEST_ON)]
		[Toggle(_ALPHATEST_ON)] _AlphaTestToggle ("Alpha Clipping", Float) = 0
		_Cutoff("Alpha Cut Off", Range(0, 1)) = 0.5

		[Space(20)] [Header(_Cull)]
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 0 

    }
    SubShader
    {
        Tags 
		{ 
			"RenderPipeline" = "UniversalPipeline"
			"RenderType"="AlphaTest"
			"Queue" = "AlphaTest" 
			"IgnoreProjector" = "True" 
			"ShaderModel"="2.0"
		}
		LOD 200
	
        Pass
        {
			Tags{"LightMode" = "UniversalForward"}
			Cull [_Cull] 

            HLSLPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			// <Unity defined keywords>
			//Shader Model & GPU target
			#pragma target 2.0

			//Lightmap
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED

			//Other
			#pragma multi_compile_fog

            // <Universal Pipeline keywords>
			//Shadow
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile _ _SHADOWS_SOFT
			
			// <My Variant GlobalKewords>
			#pragma multi_compile __ LIGHT_REAL
			#pragma multi_compile __ LIGHTMAP

			// <Material Keywords>
			#pragma shader_feature_local _EMISSION
			#pragma shader_feature_local _SPECULAR
			#pragma shader_feature_local _FRESNEL
			#pragma shader_feature_local _NORMALMAP
			#pragma shader_feature_local _ALPHATEST_ON
			//#pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			TEXTURE2D(_MainTex);
			TEXTURE2D(_EmissionMap);
			TEXTURE2D(_BumpMap);

			SAMPLER(sampler_MainTex);
			SAMPLER(sampler_EmissionMap);
			SAMPLER(sampler_BumpMap);

			CBUFFER_START(UnityPerMaterial)
			float4 _MainTex_ST;
			float4 _EmissionMap_ST;
			float4 _BumpMap_ST;

			float4 _Color;
			float4 _EmissionColor;
			float4 _SpecColor;
			float4 _FresnelColor;

			half _EmissionPower;
			half _SpecPower;
			half _FresnelPower;
			half _NormalStrength;
			half _ShadowDepthTreshold;
			half _CelTreshold;
			half _Cutoff;
			CBUFFER_END

    		struct Attributes
			{
				float4 positionOS	: POSITION;
				float3 normalOS		: NORMAL;

				#if defined(_NORMALMAP)
					float4 tangentOS		: TANGENT;
				#endif

				float2 uv			: TEXCOORD0;
				float2 lightmapUV	: TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS		: SV_POSITION;
				float2 uv				: TEXCOORD0;
				DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 1);
				float3 positionWS		: TEXCOORD2;
				#if defined(_NORMALMAP)
					half3 normalWS		: TEXCOORD3;
					half3 tangentWS		: TEXCOORD4;
					half3 biTangentWS	: TEXCOORD5;
				#else
					half3 normalWS		: TEXCOORD3;
				#endif

				//#if defined(_MAIN_LIGHT_SHADOWS)
					half4 shadowCoord	: TEXCOORD6;
				//#endif
				half fogFactor			: TEXCOORD7;
			};

			float4 GetShadowCasterPositionCS(float3 positionWS, float3 normalWS){
				float3 lightDirectionWS = normalize(_MainLightPosition.xyz);
				float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

				#if UNITY_REVERSED_Z
					positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE);
					//positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
				#else
					positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE);
					//positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
				#endif

				return positionCS;
			}

			Varyings vert(Attributes IN)
			{
				Varyings o;

				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_TRANSFER_INSTANCE_ID(IN, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				//Texture Sample
				o.uv = TRANSFORM_TEX(IN.uv, _MainTex);

				//Normal(Bump)
				#if defined(_NORMALMAP)
					o.normalWS = TransformObjectToWorldNormal(IN.normalOS);
					o.tangentWS = TransformObjectToWorldDir(IN.tangentOS.xyz);
					o.biTangentWS = cross(o.normalWS, o.tangentWS) * IN.tangentOS.w;
				#else
					o.normalWS = NormalizeNormalPerVertex(IN.normalOS); // 정점당 법선 정규화(원근감)
				#endif

				//LightMap
				#if defined(LIGHTMAP)
					OUTPUT_LIGHTMAP_UV(IN.lightmapUV, unity_LightmapST, o.lightmapUV); //Unity LightMap 가져와 설정
					OUTPUT_SH(o.normalWS.xyz, o.vertexSH); //Light Prob 가져와 설정
				#endif

				//Transform Cal
				VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
				o.positionCS = vertexInput.positionCS; 
				
				#if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
					o.positionWS = vertexInput.positionWS;
				#endif

				#ifdef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
					o.shadowCoord = GetShadowCoord(vertexInput);
				#endif

				//Fog
				o.fogFactor = ComputeFogFactor(vertexInput.positionCS.z);

				return o;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				//Texture Sample
				float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) *_Color;
				
				//Normal(Bump)
				#if defined(_NORMALMAP)
					half3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, IN.uv)) * half3(_NormalStrength, _NormalStrength, 1);//SampleNormal(IN.uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap));
					half3x3 tangentToWorld = half3x3(IN.tangentWS, IN.biTangentWS, IN.normalWS);
					IN.normalWS = TransformTangentToWorld(normalTS, tangentToWorld);
				#else
					IN.normalWS = NormalizeNormalPerPixel(IN.normalWS); // 픽셀당 법선 정규화(원근감)
				#endif
				

				//Light : Half-Lambert Light 방법
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					IN.shadowCoord = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					IN.shadowCoord = TransformWorldToShadowCoord(IN.positionWS);
				#else
					IN.shadowCoord = float4(0, 0, 0, 0);
				#endif

				// Light
				Light light = GetMainLight(IN.shadowCoord);
				half NdotL = saturate(dot(IN.normalWS, light.direction)) * 0.5f +0.5f; //Half-Lambert 공식
				half3 ambient = SampleSH(IN.normalWS); //엠비언트(환경광)를 가장 간단하게 받는 함수
				half3 lighting = NdotL * _MainLightColor.rgb + ambient; // RealLight + ambient
				float cel = floor(NdotL * _CelTreshold) / (_CelTreshold - 0.5);
				color.rgb *= lighting * cel; // Light * Cel

				// RecieveShadow
				light.shadowAttenuation = saturate(light.shadowAttenuation + _ShadowDepthTreshold);
				float3 recieveShadow = lerp(0.0f, 1.0f, light.shadowAttenuation );
				color.rgb *= recieveShadow;

				//Emission
				#if defined(_EMISSION)
				{
					TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap);
					half3 emission = SAMPLE_TEXTURE2D(_EmissionMap, sampler_EmissionMap, IN.uv).rgb * _EmissionColor;
					color.rgb += emission * _EmissionPower;
				}
				#endif

				half3 viewDir	= normalize(_WorldSpaceCameraPos.xyz - IN.positionWS);

				//BlinnPhong Specular
				#if defined(_SPECULAR)
				{
					float3 NdotH = normalize(light.direction + viewDir); // BlinnPhong 공식
					half3 specular = saturate(dot(NdotH, IN.normalWS));
					specular = pow(specular, _SpecPower);
					float specularSmooth =  smoothstep(0.005, 0.1f , specular); //보간
					half3 specularReflection = specularSmooth * _SpecColor.rgb; 
					color.rgb += specularReflection; // Light + specualr 
				}
				#endif

				// Fresnel
				#if defined(_FRESNEL)
				{
					half fresnel = 1 - saturate(dot(IN.normalWS, viewDir));
					fresnel = pow(fresnel, _FresnelPower);
					half3 fresnelColor = fresnel * _FresnelColor.rgb;
					color.rgb += fresnelColor; // Light + fresnel
				}
				#endif

				//LightMap
				#if defined(LIGHTMAP)
					half3 bakedGI = SAMPLE_GI(IN.lightmapUV, IN.vertexSH, IN.normalWS); // LightMap + LightProb 적용 (LightMap 있을경우만 계산함)  //half3 bakedGI = SampleLightmap(IN.lightmapUV, IN.normalWS); // LightMap만 적용  :  Fregment Shader에서 사용
					color.rgb *= bakedGI;  // LightMap + LightProb 
				#endif
				
				//Alpha
				#if defined(_ALPHATEST_ON)
					clip(color.a - _Cutoff);
				#endif

				//Fog
				color.rgb = MixFog(color,IN.fogFactor);
				
				return color;
			}
			ENDHLSL
        }
		UsePass "Universal Render Pipeline/Lit/ShadowCaster"
		UsePass "Universal Render Pipeline/Lit/DepthOnly"

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