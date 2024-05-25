Shader "Custom/UICyclicRainShaderNoGaps"
{
    Properties
    {
        _RainTex("Rain Texture", 2D) = "white" {}
        _RainSpeed("Rain Speed", float) = 1.0
        _RainTilingY("Rain Tiling Y", float) = 1.0
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _RainTex;
                float _RainSpeed;
                float _RainTilingY;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // ѕлитка и модульное деление времени на репетативность и смещение
                    float tilingFactor = _RainTilingY / _RainSpeed;
                    float2 rainUV = i.uv;
                    rainUV.y *= _RainTilingY;

                    #if UNITY_EDITOR
                    // ¬ редакторе фиксируем значение, чтобы дождь не двигалс€
                    rainUV.y += 0;
                    #else
                    // ¬о врем€ игры движем дождь вниз
                    rainUV.y += fmod(_Time.y, tilingFactor);
                    #endif

                    // —емплирование текстуры дожд€ с использованием созданных координат
                    fixed4 rainTexCol = tex2D(_RainTex, rainUV);
                    return rainTexCol;
                }
                ENDCG
            }
        }
            FallBack "Transparent/Diffuse"
}