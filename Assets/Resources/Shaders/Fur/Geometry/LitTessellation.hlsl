#ifndef FUR_FIN_LIT_TESSELLATION_HLSL
#define FUR_FIN_LIT_TESSELLATION_HLSL

#include "./Param.hlsl"
// 순서 : Vertex Shader => Hull Shader => Tessllation => Domain Shader => Geometry Shader => Fragment Sahder

//=================== Hull =========================================================
struct HsConstantOutput
{
    float fTessFactor[3]    : SV_TessFactor; // edge[3] 앳지 백터   SV_....는  DX 시맨틱임
    float fInsideTessFactor : SV_InsideTessFactor; //inside 
    float3 f3B210 : POS3;
    float3 f3B120 : POS4;
    float3 f3B021 : POS5;
    float3 f3B012 : POS6;
    float3 f3B102 : POS7;
    float3 f3B201 : POS8;
    float3 f3B111 : CENTER;
    float3 f3N110 : NORMAL3;
    float3 f3N011 : NORMAL4;
    float3 f3N101 : NORMAL5;
};

[domain("tri")] // [UNITY_domain("tri")] 삼각형 속성 설정 
[partitioning("integer")] // 패치 분할 방식 중 interger
[outputtopology("triangle_cw")] // UNITY_outputtopology("triangle_cw") 삼각형 생성시 시계방향으로 (유니티는 모든 도형을 시계방향으로 해야함)
[patchconstantfunc("hullConst")] // 정의할 함수 명 설정 (마음대로 함수 명에 따라 적어주면 됨)
[outputcontrolpoints(3)] // 각 패치(꼭지점)당 3개의 제어점을 설정

// Hull Program or Hull Shader ()
// InputPatch<구조체, 각 패치에 설정된 정점 | 제어점 갯수>
Attributes hull(InputPatch<Attributes, 3> input, uint id : SV_OutputControlPointID)
{
    return input[id];
}

HsConstantOutput hullConst(InputPatch<Attributes, 3> i)
{
    HsConstantOutput o = (HsConstantOutput)0;

    float distance = length(float3(UNITY_MATRIX_MV[0][3], UNITY_MATRIX_MV[1][3], UNITY_MATRIX_MV[2][3]));
    float factor = (_TessMaxDist - _TessMinDist) / max(distance - _TessMinDist, 0.01);
    factor = min(factor, 1.0);
    factor *= _TessFactor;

    o.fTessFactor[0] = o.fTessFactor[1] = o.fTessFactor[2] = factor;
    o.fInsideTessFactor = factor;

    float3 f3B003 = i[0].positionOS.xyz;
    float3 f3B030 = i[1].positionOS.xyz;
    float3 f3B300 = i[2].positionOS.xyz;

    float3 f3N002 = i[0].normalOS;
    float3 f3N020 = i[1].normalOS;
    float3 f3N200 = i[2].normalOS;
        
    o.f3B210 = ((2.0 * f3B003) + f3B030 - (dot((f3B030 - f3B003), f3N002) * f3N002)) / 3.0;
    o.f3B120 = ((2.0 * f3B030) + f3B003 - (dot((f3B003 - f3B030), f3N020) * f3N020)) / 3.0;
    o.f3B021 = ((2.0 * f3B030) + f3B300 - (dot((f3B300 - f3B030), f3N020) * f3N020)) / 3.0;
    o.f3B012 = ((2.0 * f3B300) + f3B030 - (dot((f3B030 - f3B300), f3N200) * f3N200)) / 3.0;
    o.f3B102 = ((2.0 * f3B300) + f3B003 - (dot((f3B003 - f3B300), f3N200) * f3N200)) / 3.0;
    o.f3B201 = ((2.0 * f3B003) + f3B300 - (dot((f3B300 - f3B003), f3N002) * f3N002)) / 3.0;

    float3 f3E = (o.f3B210 + o.f3B120 + o.f3B021 + o.f3B012 + o.f3B102 + o.f3B201) / 6.0;
    float3 f3V = (f3B003 + f3B030 + f3B300) / 3.0;
    o.f3B111 = f3E + ((f3E - f3V) / 2.0);
    
    float fV12 = 2.0 * dot(f3B030 - f3B003, f3N002 + f3N020) / dot(f3B030 - f3B003, f3B030 - f3B003);
    float fV23 = 2.0 * dot(f3B300 - f3B030, f3N020 + f3N200) / dot(f3B300 - f3B030, f3B300 - f3B030);
    float fV31 = 2.0 * dot(f3B003 - f3B300, f3N200 + f3N002) / dot(f3B003 - f3B300, f3B003 - f3B300);
    o.f3N110 = normalize(f3N002 + f3N020 - fV12 * (f3B030 - f3B003));
    o.f3N011 = normalize(f3N020 + f3N200 - fV23 * (f3B300 - f3B030));
    o.f3N101 = normalize(f3N200 + f3N002 - fV31 * (f3B003 - f3B300));
           
    return o;
}

//=================== Domain =========================================================

[domain("tri")] // 도메인 작업을 위해 삼각형임을 다시 알려줘야 함
Attributes domain(
    HsConstantOutput hsConst,  // Hull에서 작업한 데이터 가져오기
    const OutputPatch<Attributes, 3> i, // 각 patch 
    float3 bary : SV_DomainLocation // 각 패치(꼭지점)의 중심 좌표(무게중심 좌표)  (도메인에서 이 좌표를 어떻게 사용할지가 관건)
	)
{
    Attributes o = (Attributes)0;

    float fU = bary.x;
    float fV = bary.y;
    float fW = bary.z;
    float fUU = fU * fU;
    float fVV = fV * fV;
    float fWW = fW * fW;
    float fUU3 = fUU * 3.0f;
    float fVV3 = fVV * 3.0f;
    float fWW3 = fWW * 3.0f;
    
    o.positionOS = float4(
        i[0].positionOS.xyz * fWW * fW +
        i[1].positionOS.xyz * fUU * fU +
        i[2].positionOS.xyz * fVV * fV +
        hsConst.f3B210 * fWW3 * fU +
        hsConst.f3B120 * fW * fUU3 +
        hsConst.f3B201 * fWW3 * fV +
        hsConst.f3B021 * fUU3 * fV +
        hsConst.f3B102 * fW * fVV3 +
        hsConst.f3B012 * fU * fVV3 +
        hsConst.f3B111 * 6.0f * fW * fU * fV, 
        1.0);
    o.normalOS = normalize(
        i[0].normalOS * fWW +
        i[1].normalOS * fUU +
        i[2].normalOS * fVV +
        hsConst.f3N110 * fW * fU +
        hsConst.f3N011 * fU * fV +
        hsConst.f3N101 * fW * fV);
    o.texcoord = 
        i[0].texcoord * fW + 
        i[1].texcoord * fU + 
        i[2].texcoord * fV;
    o.lightmapUV = 
        i[0].lightmapUV * fW + 
        i[1].lightmapUV * fU + 
        i[2].lightmapUV * fV;
    o.id = i[0].id;

    return o;
}

#endif
