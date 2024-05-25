Shader "Custom/CenterTransparency"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1) // �������� ����
        _Center("Center Position", Vector) = (0.5, 0.5, 0, 0) // ��������� ������
        _Radius("Radius", Float) = 0.5 // ������
        _CenterAlpha("Center Transparency", Range(0,1)) = 1.0 // ������������ � ������
        _EdgeAlpha("Transparency", Range(0,1)) = 0.0 // ������������ �� �����
    }
        SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color; // �������� ����
            float4 _Center; // ��������� ������
            float _Radius; // ������
            float _CenterAlpha; // ������������ � ������
            float _EdgeAlpha; // ������������ �� �����

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float dist = distance(IN.uv, _Center.xy);
                float t = saturate(dist / _Radius);
                float alpha = lerp(_CenterAlpha, _EdgeAlpha, t);
                fixed4 color = _Color;
                color.a *= alpha; // ��������� ���������� ������������
                return color;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}
