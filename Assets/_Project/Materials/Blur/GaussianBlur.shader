Shader "Custom/BlurAndScaleAdvanced"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
        _Scale ("Scale", Float) = 2.0
        _BlurIterations ("Blur Iterations", Range(1, 50)) = 2
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize;
            float _Scale;
            int _BlurIterations;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = (i.uv - 0.5) / _Scale + 0.5;

                if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
                    return float4(0, 0, 0, 0);

                float2 texel = _MainTex_TexelSize.xy * _BlurSize;
                fixed4 col = tex2D(_MainTex, uv) * 1.0;
                float w = 1.0;

                // Ручной обход 4 итераций
                [unroll]
                for (int i = 1; i <= 4; i++)
                {
                    if (_BlurIterations < i) break;

                    float fi = float(i);
                    float weight = exp(-(fi * fi) / (_BlurIterations));
                    float2 offset = texel * fi;

                    col += tex2D(_MainTex, uv + float2(offset.x, 0)) * weight;
                    col += tex2D(_MainTex, uv - float2(offset.x, 0)) * weight;
                    col += tex2D(_MainTex, uv + float2(0, offset.y)) * weight;
                    col += tex2D(_MainTex, uv - float2(0, offset.y)) * weight;

                    w += weight * 4.0;
                }

                return col / w;
            }
            ENDCG
        }
    }
}