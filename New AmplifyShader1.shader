// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New AmplifyShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Tini("Tini", Color) = (0,0,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Normal("Normal", 2D) = "bump" {}
		_AmbientOcclusion("Ambient Occlusion", 2D) = "white" {}
		[HDR]_GlowColour("Glow Colour", Color) = (3.902679,24.84706,22.89572,0)
		_Teleport("Teleport", Range( -20 , 20)) = 0.3
		[Toggle]_Reverse("Reverse", Float) = 1
		_NoiseScale("NoiseScale", Float) = 0
		_Thickness("Thickness", Float) = 0.3
		_VertOffsetStrength("VertOffset Strength", Float) = 0
		_Speed("Speed", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _Teleport;
		uniform float _Reverse;
		uniform float _VertOffsetStrength;
		uniform float _NoiseScale;
		uniform float _Speed;
		uniform float _Thickness;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _AmbientOcclusion;
		uniform float4 _AmbientOcclusion_ST;
		uniform float4 _Tini;
		uniform float4 _GlowColour;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform79 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float Y_gradient76 = saturate( ( ( transform79.y + _Teleport ) / lerp(-10.0,10.0,_Reverse) ) );
			float2 temp_cast_1 = (_NoiseScale).xx;
			float2 panner135 = ( ( _Time.y * _Speed ) * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord52 = v.texcoord.xy * temp_cast_1 + panner135;
			float Noise68 = step( frac( uv_TexCoord52.y ) , _Thickness );
			float3 m122 = ( ( ( ase_vertex3Pos * Y_gradient76 ) * _VertOffsetStrength ) * Noise68 );
			v.vertex.xyz += m122;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 Normal113 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			o.Normal = Normal113;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_AmbientOcclusion = i.uv_texcoord * _AmbientOcclusion_ST.xy + _AmbientOcclusion_ST.zw;
			float4 Albedo109 = ( tex2D( _Albedo, uv_Albedo ) * tex2D( _AmbientOcclusion, uv_AmbientOcclusion ) * _Tini );
			o.Albedo = Albedo109.rgb;
			float2 temp_cast_1 = (_NoiseScale).xx;
			float2 panner135 = ( ( _Time.y * _Speed ) * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord52 = i.uv_texcoord * temp_cast_1 + panner135;
			float Noise68 = step( frac( uv_TexCoord52.y ) , _Thickness );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 transform79 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float Y_gradient76 = saturate( ( ( transform79.y + _Teleport ) / lerp(-10.0,10.0,_Reverse) ) );
			float4 Emission99 = ( _GlowColour * ( Noise68 * Y_gradient76 ) );
			o.Emission = Emission99.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
			float temp_output_91_0 = ( Y_gradient76 * 1.0 );
			float myVarName288 = ( ( ( ( 1.0 - Y_gradient76 ) * Noise68 ) - temp_output_91_0 ) + ( 1.0 - temp_output_91_0 ) );
			clip( myVarName288 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
2093;230;1308;822;2680.936;368.5644;1.486814;True;False
Node;AmplifyShaderEditor.CommentaryNode;83;-1926.067,659.3774;Float;False;1643.794;867.8497;;10;130;76;82;80;129;81;75;74;79;73;Y_Gradient;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;84;-1928.218,-123.1377;Float;False;1717.573;701.3774;Comment;10;52;68;131;132;133;134;135;137;138;139;Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;137;-1906.306,128.0313;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;138;-1910.767,357.0007;Float;False;Property;_Speed;Speed;13;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;73;-1870.054,726.8884;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;79;-1628.43,715.7227;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;81;-1460.179,957.6535;Float;False;Constant;_NegativeNumber;Negative Number;13;0;Create;True;0;0;False;0;-10;-10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-1738.295,958.772;Float;False;Property;_Teleport;Teleport;8;0;Create;True;0;0;False;0;0.3;0.3;-20;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;-1696.666,257.3843;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;129;-1464.305,1190.558;Float;False;Constant;_PositiveNumber;Positive Number;4;0;Create;True;0;0;False;0;10;4.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;74;-1349.399,726.8483;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;135;-1466.209,238.0555;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;132;-1405.249,-19.16318;Float;False;Property;_NoiseScale;NoiseScale;10;0;Create;True;0;0;False;0;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;130;-1207.536,986.8967;Float;False;Property;_Reverse;Reverse;9;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;80;-931.7479,759.1122;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;52;-1159.535,50.08654;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FractNode;131;-874.4565,-25.11049;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-880.4033,239.5424;Float;False;Property;_Thickness;Thickness;11;0;Create;True;0;0;False;0;0.3;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;95;-162.5974,663.559;Float;False;1719.153;835.2444;Comment;10;88;94;93;90;91;92;86;87;85;77;Opacity Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;82;-707.7087,759.1127;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;77;-102.5941,742.7242;Float;False;76;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;133;-611.2898,-41.46537;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;76;-513.6064,781.9111;Float;False;Y_gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;128;-1900.243,1673.167;Float;False;1468.569;644.8958;Comment;8;119;121;120;125;124;127;126;122;Vert Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;68;-446.6656,189.1406;Float;False;Noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;90;-122.8105,1191.355;Float;False;76;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;87;123.3071,971.8563;Float;False;68;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;85;152.8768,747.5031;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;119;-1843.346,1723.167;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;121;-1850.243,1937.227;Float;False;76;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;104;-160.7079,-119.0423;Float;False;1179.526;637.6872;Comment;6;99;103;98;102;96;97;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;-1526.755,1796.921;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;125;-1517.184,2054.271;Float;False;Property;_VertOffsetStrength;VertOffset Strength;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;118;1084.591,-122.8133;Float;False;1682.417;702.322;Comment;7;106;105;107;108;109;112;113;Base Stuff;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;376.3383,751.3621;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;187.0529,1181.847;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;96;-131.1162,54.32462;Float;False;68;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;97;-132.1162,283.3246;Float;False;76;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;105;1184.977,376.5088;Float;False;Property;_Tini;Tini;1;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;102;126.8564,-47.13828;Float;False;Property;_GlowColour;Glow Colour;7;1;[HDR];Create;True;0;0;False;0;3.902679,24.84706,22.89572,0;3.902679,24.84706,22.89572,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;156.6212,162.1443;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;127;-1209.443,2088.063;Float;False;68;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;92;677.2596,919.5889;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;93;691.941,1196.047;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;106;1137.912,162.6804;Float;True;Property;_AmbientOcclusion;Ambient Occlusion;6;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;124;-1206.284,1850.091;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;108;1134.591,-47.08226;Float;True;Property;_Albedo;Albedo;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;488.7656,120.5886;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;112;2133.43,-62.1994;Float;True;Property;_Normal;Normal;5;0;Create;True;0;0;False;0;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-953.4426,1859.063;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;107;1522.172,69.9147;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;94;974.9392,1076.137;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;116;1676.113,986.9756;Float;False;Property;_Metallic;Metallic;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;117;1685.843,1249.959;Float;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;123;1950.245,1685.388;Float;False;122;0;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;99;764.8674,124.0916;Float;False;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;115;1970.125,861.1791;Float;False;113;0;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;111;1970.286,665.3925;Float;False;109;0;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;89;1964.645,1428.651;Float;True;88;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;88;1264.579,978.6224;Float;True;myVarName2;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;113;2524.008,-72.8133;Float;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;72;1974.747,1060.701;Float;False;99;0;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;109;1761.849,64.19852;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;122;-674.6736,1857.45;Float;False;m;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;11;2336.838,955.946;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New AmplifyShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;79;0;73;0
WireConnection;139;0;137;0
WireConnection;139;1;138;0
WireConnection;74;0;79;2
WireConnection;74;1;75;0
WireConnection;135;1;139;0
WireConnection;130;0;81;0
WireConnection;130;1;129;0
WireConnection;80;0;74;0
WireConnection;80;1;130;0
WireConnection;52;0;132;0
WireConnection;52;1;135;0
WireConnection;131;0;52;2
WireConnection;82;0;80;0
WireConnection;133;0;131;0
WireConnection;133;1;134;0
WireConnection;76;0;82;0
WireConnection;68;0;133;0
WireConnection;85;0;77;0
WireConnection;120;0;119;0
WireConnection;120;1;121;0
WireConnection;86;0;85;0
WireConnection;86;1;87;0
WireConnection;91;0;90;0
WireConnection;98;0;96;0
WireConnection;98;1;97;0
WireConnection;92;0;86;0
WireConnection;92;1;91;0
WireConnection;93;0;91;0
WireConnection;124;0;120;0
WireConnection;124;1;125;0
WireConnection;103;0;102;0
WireConnection;103;1;98;0
WireConnection;126;0;124;0
WireConnection;126;1;127;0
WireConnection;107;0;108;0
WireConnection;107;1;106;0
WireConnection;107;2;105;0
WireConnection;94;0;92;0
WireConnection;94;1;93;0
WireConnection;99;0;103;0
WireConnection;88;0;94;0
WireConnection;113;0;112;0
WireConnection;109;0;107;0
WireConnection;122;0;126;0
WireConnection;11;0;111;0
WireConnection;11;1;115;0
WireConnection;11;2;72;0
WireConnection;11;3;116;0
WireConnection;11;4;117;0
WireConnection;11;10;89;0
WireConnection;11;11;123;0
ASEEND*/
//CHKSM=02EB990C980C21A24A22FFEDB50DA079A0CD82CA