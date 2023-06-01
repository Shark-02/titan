Shader "LX/BoxBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize("BlurSize",float)=1
    }
    SubShader
    {
       
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
            uniform half4 _MainTex_TexelSize;
            fixed _BlurSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed2 delta=_MainTex_TexelSize.xy*_BlurSize;
                return (
                    tex2D(_MainTex,i.uv)+
                    tex2D(_MainTex,i.uv+fixed2(delta.x,0))+
                    tex2D(_MainTex,i.uv-fixed2(delta.x,0))+
                    tex2D(_MainTex,i.uv+fixed2(0,delta.y))+
                    tex2D(_MainTex,i.uv-fixed2(0,delta.y))
                    )*0.2;
            }

            
            ENDCG
        }
    }
}