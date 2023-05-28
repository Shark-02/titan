Shader "Custom/DepthFog"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FogColor("FogColor",COLOR)=(1,1,1,1)
        _StartDist("StartDist",float)=0
        _Density("Density",float)=0.1
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                uint id : SV_VERTEXID;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 ray : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _MainTex_ST;
            fixed4 _FogColor;
            float4x4 _Ray;
            float _StartDist;
            float _EndDist;
            float _Density;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.ray=_Ray[v.id].xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float depth=SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv);
                float dist=LinearEyeDepth(depth)-_StartDist;
                float factor=exp(-_Density*dist);
                factor=saturate(factor);
                fixed4 col = tex2D(_MainTex, i.uv);
                //return 0;
                //return fixed4(i.uv.x,i.uv.y,1,1);
                //return col;
                return lerp(_FogColor,col,factor);
            }
            ENDCG
        }
    }
}
