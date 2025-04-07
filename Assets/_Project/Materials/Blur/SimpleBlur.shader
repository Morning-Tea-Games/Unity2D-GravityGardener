Shader "Unlit/SimpleBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
_BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
float _BlurSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

float2 uv = i.uv;
float2 texelSize = _BlurSize * float2(1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y);

fixed4 color = tex2D(_MainTex, uv) * 0.2270270270;
color += tex2D(_MainTex, uv + float2(texelSize.x * 1, texelSize.y * 0)) * 0.1945945946;
color += tex2D(_MainTex, uv + float2(texelSize.x * -1, texelSize.y * 0)) * 0.1945945946;
color += tex2D(_MainTex, uv + float2(texelSize.x * 0, texelSize.y * 1)) * 0.1945945946;
color += tex2D(_MainTex, uv + float2(texelSize.x * 0, texelSize.y * -1)) * 0.1945945946;
color += tex2D(_MainTex, uv + float2(texelSize.x * 2, texelSize.y * 0)) * 0.1216216216;
color += tex2D(_MainTex, uv + float2(texelSize.x * -2, texelSize.y * 0)) * 0.1216216216;
color += tex2D(_MainTex, uv + float2(texelSize.x * 0, texelSize.y * 2)) * 0.1216216216;
color += tex2D(_MainTex, uv + float2(texelSize.x * 0, texelSize.y * -2)) * 0.1216216216;
color += tex2D(_MainTex, uv + float2(texelSize.x * 1, texelSize.y * 1)) * 0.0540540541;
color += tex2D(_MainTex, uv + float2(texelSize.x * -1, texelSize.y * 1)) * 0.0540540541;
color += tex2D(_MainTex, uv + float2(texelSize.x * 1, texelSize.y * -1)) * 0.0540540541;
color += tex2D(_MainTex, uv + float2(texelSize.x * -1, texelSize.y * -1)) * 0.0540540541;


                return color;
            }
            ENDCG
        }
    }
}