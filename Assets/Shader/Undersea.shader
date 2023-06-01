Shader "Custom/Undersea"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FogColor("FogColor",COLOR)=(1,1,1,1)
        _StartDist("StartDist",float)=0
        _Density("Density",float)=0.1
        _MaxIntensity("MaxIntensity",Range(0,1))=1
        _BlendColor("BlendColor",COLOR)=(1,1,1,1)
        _BlendStrength("BlendStrength",Range(0,1))=0.1
        _VignetteColor("VignetteColor",COLOR)=(0,0,0,1)
        _VignetteRadiusInner("VignetteRadiusInner",Range(0,1))=.25
        _VignetteRadiusOuter("VignetteRadiusOuter",Range(0,1))=.5
    }
    SubShader
    {
        //Tags { "RenderType"="Opaque" }
        //LOD 100

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
            UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
            float4 _MainTex_ST;
            fixed4 _FogColor;
            float4x4 _Ray;
            fixed _StartDist;
            fixed _EndDist;
            fixed _Density;
            fixed _MaxIntensity;
            fixed4 _BlendColor;
            fixed _BlendStrength;
            fixed4 _VignetteColor;
            fixed _VignetteRadiusInner;
            fixed _VignetteRadiusOuter;

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
                fixed4 foggedCol= lerp(_FogColor,col,max(factor,1-_MaxIntensity));
                fixed4 blendedCol=lerp(foggedCol,_BlendColor,_BlendStrength);
                fixed4 vignettedCol=lerp(blendedCol,_VignetteColor,pow(max((distance(i.uv,fixed2(.5,.5))-min(_VignetteRadiusInner,_VignetteRadiusOuter)),0)/abs(_VignetteRadiusOuter-_VignetteRadiusInner),2));
                return vignettedCol;
            }
            ENDCG
        }
    }
}