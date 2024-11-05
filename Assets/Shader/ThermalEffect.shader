Shader "Custom/ThermalEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Calculate the grayscale value
                float gray = dot(col.rgb, float3(0.3, 0.59, 0.11));

                // Check if the red component is significantly higher than the green and blue
                if (col.r > col.g * 1.2 && col.r > col.b * 1.2)
                {
                    // Keep the color as it is
                    return col;
                }
                else
                {
                    // Convert to grayscale
                    return fixed4(gray, gray, gray, col.a);
                }
            }
            ENDCG
        }
    }
}
