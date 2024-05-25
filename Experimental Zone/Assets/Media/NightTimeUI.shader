Shader "Custom/CenterTransparency"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1) // Основной цвет
        _Center("Center Position", Vector) = (0.5, 0.5, 0, 0) // Положение центра
        _Radius("Radius", Float) = 0.5 // Радиус
        _CenterAlpha("Center Transparency", Range(0,1)) = 1.0 // Прозрачность в центре
        _EdgeAlpha("Transparency", Range(0,1)) = 0.0 // Прозрачность на краях
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

            fixed4 _Color; // Основной цвет
            float4 _Center; // Положение центра
            float _Radius; // Радиус
            float _CenterAlpha; // Прозрачность в центре
            float _EdgeAlpha; // Прозрачность на краях

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
                color.a *= alpha; // Применяем изменяемую прозрачность
                return color;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}
