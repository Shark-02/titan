// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader ""
{
	Properties
	{
		_Main("Main", 2D) = "white" {}
		[KeywordEnum(A,R)] _panel("panel", Float) = 0
		_outcolor("outcolor", Color) = (1,1,1,0)
		[HDR]_innercolor("innercolor", Color) = (0.06289297,0.2984785,1,0)
		_height("height", Range( 0 , 1)) = 0
		_remap("remap", Vector) = (0,1,1,0)
		[KeywordEnum(off,on)] _Keyword0("Keyword 0", Float) = 1
		_speed("speed", Vector) = (-1,0,0,0)
		_Textureimage2("Texture image2", 2D) = "white" {}
		_Textureimage("Texture image", 2D) = "white" {}
		[Toggle(_KEYWORD1_ON)] _Keyword1("Keyword 1", Float) = 0
		_hardness("hardness", Range( 0.5 , 1)) = 0.5
		_dissolve("dissolve", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#include "UnityStandardBRDF.cginc"
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#pragma shader_feature_local _KEYWORD0_OFF _KEYWORD0_ON
			#pragma shader_feature_local _PANEL_A _PANEL_R
			#pragma shader_feature_local _KEYWORD1_ON


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_tangent : TANGENT;
				float3 ase_normal : NORMAL;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float4 _outcolor;
			uniform sampler2D _Textureimage2;
			uniform float4 _speed;
			uniform float4 _innercolor;
			uniform sampler2D _Main;
			uniform float4 _Main_ST;
			uniform float4 _remap;
			uniform float _height;
			uniform float _hardness;
			uniform sampler2D _Textureimage;
			uniform float4 _Textureimage_ST;
			uniform float _dissolve;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldTangent = UnityObjectToWorldDir(v.ase_tangent);
				o.ase_texcoord2.xyz = ase_worldTangent;
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord3.xyz = ase_worldNormal;
				float ase_vertexTangentSign = v.ase_tangent.w * ( unity_WorldTransformParams.w >= 0.0 ? 1.0 : -1.0 );
				float3 ase_worldBitangent = cross( ase_worldNormal, ase_worldTangent ) * ase_vertexTangentSign;
				o.ase_texcoord4.xyz = ase_worldBitangent;
				
				o.ase_texcoord1 = v.ase_texcoord;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.w = 0;
				o.ase_texcoord3.w = 0;
				o.ase_texcoord4.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 appendResult30 = (float2(_speed.x , _speed.y));
				float2 CenteredUV15_g1 = ( i.ase_texcoord1.xy - float2( 0.5,0.5 ) );
				float4 _Vector0 = float4(1,5,0,0);
				float2 break17_g1 = CenteredUV15_g1;
				float2 appendResult23_g1 = (float2(( length( CenteredUV15_g1 ) * _Vector0.x * 2.0 ) , ( atan2( break17_g1.x , break17_g1.y ) * ( 1.0 / 6.28318548202515 ) * _Vector0.y )));
				float2 appendResult32 = (float2(_Vector0.z , _Vector0.w));
				float2 texCoord24 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				#if defined(_KEYWORD0_OFF)
				float2 staticSwitch31 = texCoord24;
				#elif defined(_KEYWORD0_ON)
				float2 staticSwitch31 = (appendResult23_g1*1.0 + appendResult32);
				#else
				float2 staticSwitch31 = (appendResult23_g1*1.0 + appendResult32);
				#endif
				float2 panner33 = ( 1.0 * _Time.y * appendResult30 + staticSwitch31);
				float2 texCoord4 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 uv_Main = i.ase_texcoord1.xy * _Main_ST.xy + _Main_ST.zw;
				float4 tex2DNode2 = tex2D( _Main, uv_Main );
				#if defined(_PANEL_A)
				float staticSwitch17 = tex2DNode2.a;
				#elif defined(_PANEL_R)
				float staticSwitch17 = tex2DNode2.r;
				#else
				float staticSwitch17 = tex2DNode2.a;
				#endif
				float3 ase_worldTangent = i.ase_texcoord2.xyz;
				float3 ase_worldNormal = i.ase_texcoord3.xyz;
				float3 ase_worldBitangent = i.ase_texcoord4.xyz;
				float3 tanToWorld0 = float3( ase_worldTangent.x, ase_worldBitangent.x, ase_worldNormal.x );
				float3 tanToWorld1 = float3( ase_worldTangent.y, ase_worldBitangent.y, ase_worldNormal.y );
				float3 tanToWorld2 = float3( ase_worldTangent.z, ase_worldBitangent.z, ase_worldNormal.z );
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(WorldPosition);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_tanViewDir =  tanToWorld0 * ase_worldViewDir.x + tanToWorld1 * ase_worldViewDir.y  + tanToWorld2 * ase_worldViewDir.z;
				ase_tanViewDir = Unity_SafeNormalize( ase_tanViewDir );
				float2 Offset1 = ( ( (_remap.z + (staticSwitch17 - _remap.x) * (_remap.w - _remap.z) / (_remap.y - _remap.x)) - 1 ) * ase_tanViewDir.xy * _height ) + texCoord4;
				float4 tex2DNode7 = tex2D( _Main, Offset1 );
				#if defined(_PANEL_A)
				float staticSwitch20 = tex2DNode7.a;
				#elif defined(_PANEL_R)
				float staticSwitch20 = tex2DNode7.r;
				#else
				float staticSwitch20 = tex2DNode7.a;
				#endif
				float4 lerpResult9 = lerp( _outcolor , ( tex2D( _Textureimage2, panner33 ) * _innercolor ) , staticSwitch20);
				float4 tex2DNode14 = tex2D( _Main, uv_Main );
				#if defined(_PANEL_A)
				float staticSwitch19 = tex2DNode14.a;
				#elif defined(_PANEL_R)
				float staticSwitch19 = tex2DNode14.r;
				#else
				float staticSwitch19 = tex2DNode14.a;
				#endif
				float2 uv_Textureimage = i.ase_texcoord1.xy * _Textureimage_ST.xy + _Textureimage_ST.zw;
				float4 texCoord38 = i.ase_texcoord1;
				texCoord38.xy = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				#ifdef _KEYWORD1_ON
				float staticSwitch43 = texCoord38.x;
				#else
				float staticSwitch43 = _dissolve;
				#endif
				float smoothstepResult50 = smoothstep( ( 1.0 - _hardness ) , _hardness , saturate( ( ( tex2D( _Textureimage, uv_Textureimage ).r + 1.0 ) - ( staticSwitch43 * 2.0 ) ) ));
				float4 appendResult12 = (float4(lerpResult9.rgb , ( staticSwitch19 * smoothstepResult50 )));
				
				
				finalColor = appendResult12;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;42.57294,-228.836;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;6;-22.36939,380.4656;Inherit;False;Tangent;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;7;684.9087,-83.01833;Inherit;True;Property;_TextureSample1;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;747.3119,-351.6407;Inherit;True;Property;_TextureSample2;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;2;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ParallaxMappingNode;1;391.5729,-70.836;Inherit;False;Normal;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;20;1116.096,-34.08776;Inherit;False;Property;_Keyword3;Keyword 0;1;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;A;R;Reference;17;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;17;-677.4938,-162.4243;Inherit;False;Property;_panel;panel;1;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;A;R;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;22;-391.2661,-128.6045;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-96.8002,135.9292;Inherit;False;Property;_height;height;4;0;Create;True;0;0;0;False;0;False;0;0.013;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-732.8766,684.0796;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;23;-705.5116,148.8209;Inherit;False;Property;_remap;remap;5;0;Create;True;0;0;0;False;0;False;0,1,1,0;0,1,1,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;29;-173.4177,896.8052;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;-469.293,1026.436;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;30;211.6885,1046.781;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-1013.84,-234.7009;Inherit;True;Property;_Main;Main;0;0;Create;True;0;0;0;False;0;False;-1;341485d51a9c9484ea594fd6db7875e2;341485d51a9c9484ea594fd6db7875e2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;19;1214.95,-270.9118;Inherit;False;Property;_Keyword2;Keyword 0;1;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;A;R;Reference;17;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;9;2019.625,132.2799;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;10;1100.666,142.5036;Inherit;False;Property;_outcolor;outcolor;2;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.1037736,0.1037736,0.1037736,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;1527.325,415.4748;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;1236.345,846.0474;Inherit;False;Property;_innercolor;innercolor;3;1;[HDR];Create;True;0;0;0;False;0;False;0.06289297,0.2984785,1,0;0.9882353,1.396078,2.996078,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;12;2924.497,-35.23412;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;834.973,1699.901;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;944.1495,1901.681;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;734.8736,2180.9;Inherit;False;Constant;_Float4;Float 4;10;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;46;1143.073,1725.9;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;1096.739,2078.268;Inherit;False;Property;_hardness;hardness;11;0;Create;True;0;0;0;False;0;False;0.5;0.52;0.5;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;48;1348.288,1888.046;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;49;1397.564,1680.928;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;50;1902.557,1450.827;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;25;-773.4176,963.2051;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;0;False;0;False;1,5,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;33;474.013,763.1312;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;31;145.4779,713.5407;Inherit;False;Property;_Keyword0;Keyword 0;6;0;Create;True;0;0;0;False;0;False;0;1;0;True;;KeywordEnum;2;off;on;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;2063.657,404.5198;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;34;749.5548,753.0775;Inherit;True;Property;_Textureimage2;Texture image2;8;0;Create;True;0;0;0;False;0;False;-1;None;0937d491ea93a35438d945bb47a8609f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;40;206.6623,1633.374;Inherit;True;Property;_Textureimage;Texture image;9;0;Create;True;0;0;0;False;0;False;-1;None;ccc8ae0ce6d4c454d9c9a28e98952a08;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;41;353.7498,1833.116;Inherit;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;38;141.7464,1956.023;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;43;551.8037,1923.958;Inherit;False;Property;_Keyword1;Keyword 1;10;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;26;-343.0177,1202.405;Inherit;False;Property;_speed;speed;7;0;Create;True;0;0;0;False;0;False;-1,0,0,0;-0.05,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;27;-527.3485,773.4957;Inherit;True;Polar Coordinates;-1;;1;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;3370.335,-70.46763;Float;False;True;-1;2;ASEMaterialInspector;100;5;;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;True;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.RangedFloatNode;44;45.79346,2194.798;Inherit;False;Property;_dissolve;dissolve;12;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
WireConnection;7;1;1;0
WireConnection;1;0;4;0
WireConnection;1;1;22;0
WireConnection;1;2;21;0
WireConnection;1;3;6;0
WireConnection;20;1;7;4
WireConnection;20;0;7;1
WireConnection;17;1;2;4
WireConnection;17;0;2;1
WireConnection;22;0;17;0
WireConnection;22;1;23;1
WireConnection;22;2;23;2
WireConnection;22;3;23;3
WireConnection;22;4;23;4
WireConnection;29;0;27;0
WireConnection;29;2;32;0
WireConnection;32;0;25;3
WireConnection;32;1;25;4
WireConnection;30;0;26;1
WireConnection;30;1;26;2
WireConnection;19;1;14;4
WireConnection;19;0;14;1
WireConnection;9;0;10;0
WireConnection;9;1;36;0
WireConnection;9;2;20;0
WireConnection;36;0;34;0
WireConnection;36;1;11;0
WireConnection;12;0;9;0
WireConnection;12;3;51;0
WireConnection;42;0;40;1
WireConnection;42;1;41;0
WireConnection;37;0;43;0
WireConnection;37;1;45;0
WireConnection;46;0;42;0
WireConnection;46;1;37;0
WireConnection;48;0;47;0
WireConnection;49;0;46;0
WireConnection;50;0;49;0
WireConnection;50;1;48;0
WireConnection;50;2;47;0
WireConnection;33;0;31;0
WireConnection;33;2;30;0
WireConnection;31;1;24;0
WireConnection;31;0;29;0
WireConnection;51;0;19;0
WireConnection;51;1;50;0
WireConnection;34;1;33;0
WireConnection;43;1;44;0
WireConnection;43;0;38;1
WireConnection;27;3;25;1
WireConnection;27;4;25;2
WireConnection;0;0;12;0
ASEEND*/
//CHKSM=4DF1BA587DA62E94E56B4AFC523C72D1129379B6