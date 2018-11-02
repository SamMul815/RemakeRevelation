// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Teleport"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Tiling("Tiling", Vector) = (5,5,0,0)
		_Speed("Speed", Float) = 1
		_warf("warf", Range( -10 , 10)) = 1
		[HDR]_GlowColor("Glow Color", Color) = (0.673125,0.6926227,1.077,0)
		_AO("AO", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "bump" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		[Toggle]_Reverse("Reverse", Float) = 1
		_VertOffsetStrength("VertOffset Strength", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
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

		uniform float _warf;
		uniform float _Reverse;
		uniform float _VertOffsetStrength;
		uniform float2 _Tiling;
		uniform float _Speed;
		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _AO;
		uniform float4 _AO_ST;
		uniform float4 _GlowColor;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _Cutoff = 0.5;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform22 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float Y_gradient20 = saturate( ( ( transform22.y + _warf ) / lerp(-10.0,10.0,_Reverse) ) );
			float mulTime7 = _Time.y * _Speed;
			float2 panner6 = ( mulTime7 * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord1 = v.texcoord.xy * _Tiling + panner6;
			float simplePerlin2D2 = snoise( uv_TexCoord1 );
			float Noise11 = ( simplePerlin2D2 + 1.0 );
			float3 VertOffset61 = ( ( ( ase_vertex3Pos * Y_gradient20 ) * _VertOffsetStrength ) * Noise11 );
			v.vertex.xyz += VertOffset61;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float3 Normalmap53 = UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) );
			o.Normal = Normalmap53;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_AO = i.uv_texcoord * _AO_ST.xy + _AO_ST.zw;
			float4 Albedo50 = ( tex2D( _Albedo, uv_Albedo ) * tex2D( _AO, uv_AO ) * float4(1,1,1,0) );
			o.Albedo = Albedo50.rgb;
			float mulTime7 = _Time.y * _Speed;
			float2 panner6 = ( mulTime7 * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord1 = i.uv_texcoord * _Tiling + panner6;
			float simplePerlin2D2 = snoise( uv_TexCoord1 );
			float Noise11 = ( simplePerlin2D2 + 1.0 );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 transform22 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float Y_gradient20 = saturate( ( ( transform22.y + _warf ) / lerp(-10.0,10.0,_Reverse) ) );
			float4 Emission41 = ( _GlowColor * ( Noise11 * Y_gradient20 ) );
			o.Emission = Emission41.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
			float temp_output_33_0 = ( Y_gradient20 * 1.0 );
			float OpacityMask30 = ( ( ( ( 1.0 - Y_gradient20 ) * Noise11 ) - temp_output_33_0 ) + ( 1.0 - temp_output_33_0 ) );
			clip( OpacityMask30 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
110;34;1679;806;4134.04;252.893;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;15;-4012.262,-712.2704;Float;False;1542.767;453.578;Comment;9;8;7;6;5;1;2;10;9;11;Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;26;-4019.301,-177.5524;Float;False;1564.191;846.7394;Comment;10;68;24;20;25;23;18;22;19;16;69;Y_Gradient;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-3962.262,-451.2715;Float;False;Property;_Speed;Speed;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;16;-3969.301,-127.5525;Float;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-3808.034,212.5186;Float;True;Constant;_NagativeNumber;Nagative Number;4;0;Create;True;0;0;False;0;-10;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-3807.163,439.4366;Float;True;Constant;_PositiveNumber;Positive Number;11;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;22;-3736.541,-115.6456;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;7;-3809.261,-446.2715;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-3806.313,74.65698;Float;False;Property;_warf;warf;3;0;Create;True;0;0;False;0;1;-9.1;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;6;-3641.26,-487.2715;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ToggleSwitchNode;69;-3561.218,348.0287;Float;False;Property;_Reverse;Reverse;10;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-3625.26,-630.2705;Float;False;Property;_Tiling;Tiling;1;0;Create;True;0;0;False;0;5,5;100,100;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-3475.485,-29.47149;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;23;-3223.044,11.7541;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-3440.255,-632.2705;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-3153.546,-420.6936;Float;False;Constant;_Booster;Booster;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-3184.256,-662.2705;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;37;-4058.822,824.1607;Float;False;1720.805;732.8799;Comment;10;21;29;32;27;28;33;34;35;36;30;Opacity Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;25;-2985.141,10.45415;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;21;-3964.174,876.2358;Float;True;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-2962.448,-511.6937;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;20;-2709.802,-4.876222;Float;False;Y_gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;67;-4060.905,1621.468;Float;False;1711.8;612.0396;Comment;8;58;59;64;60;63;66;65;61;Vert Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;-3749.142,1096.521;Float;True;11;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;45;-2430.322,-765.6913;Float;False;1135.759;579.3199;Comment;6;38;39;40;43;44;41;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;59;-4000.01,1857.5;Float;True;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;32;-4008.821,1300.267;Float;True;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;58;-4010.904,1671.468;Float;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;27;-3730.481,874.1605;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-2712.489,-593.3747;Float;True;Noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-3449.289,915.8143;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;57;-1223.062,-853.5778;Float;False;1598.899;657.4584;Comment;7;47;48;46;49;50;52;53;Base;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-3704.717,1975.507;Float;True;Property;_VertOffsetStrength;VertOffset Strength;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;39;-2377.722,-416.3712;Float;True;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-3754.819,1304.039;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-3690.021,1747.503;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;38;-2380.322,-623.0719;Float;True;11;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;46;-1120.447,-403.1196;Float;False;Constant;_BaseColor;Base Color;6;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;49;-1173.062,-803.5778;Float;True;Property;_Albedo;Albedo;6;0;Create;True;0;0;False;0;None;a4807ff47f6478148b18250ea03d4397;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;47;-1171.6,-596.0405;Float;True;Property;_AO;AO;5;0;Create;True;0;0;False;0;None;3b231e36bbbaffd41a0da24f5b89e456;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-2134.55,-517.317;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;-3294.609,1942.014;Float;True;11;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;34;-3154.879,1045.093;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;43;-2086.23,-715.6913;Float;False;Property;_GlowColor;Glow Color;4;1;[HDR];Create;True;0;0;False;0;0.673125,0.6926227,1.077,0;0.673125,0.6926227,1.077,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;35;-3137.268,1299.645;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-3300.742,1729.253;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-2875.171,1165.237;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-1754.788,-543.78;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-2929.311,1726.213;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;52;-269.5598,-649.9967;Float;True;Property;_NormalMap;NormalMap;7;0;Create;True;0;0;False;0;None;64743079716905840ab4f649af98f7f3;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-797.4492,-666.1943;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;-556.2971,-666.194;Float;True;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;41;-1537.563,-554.0372;Float;True;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;54;-288.2978,11.28404;Float;False;53;0;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;62;-310.7906,414.946;Float;False;61;0;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;53;132.8373,-659.3666;Float;True;Normalmap;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;51;-278.3434,-74.20528;Float;False;50;0;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;61;-2592.104,1722.618;Float;True;VertOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-2581.014,1162.003;Float;True;OpacityMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;14;-276.5865,95.25265;Float;False;41;0;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-435.1451,176.1587;Float;False;Property;_Metallic;Metallic;9;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;-281.4243,323.2659;Float;False;30;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-427.1451,251.1587;Float;False;Property;_Smoothness;Smoothness;8;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/Teleport;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;22;0;16;0
WireConnection;7;0;8;0
WireConnection;6;1;7;0
WireConnection;69;0;24;0
WireConnection;69;1;68;0
WireConnection;18;0;22;2
WireConnection;18;1;19;0
WireConnection;23;0;18;0
WireConnection;23;1;69;0
WireConnection;1;0;5;0
WireConnection;1;1;6;0
WireConnection;2;0;1;0
WireConnection;25;0;23;0
WireConnection;9;0;2;0
WireConnection;9;1;10;0
WireConnection;20;0;25;0
WireConnection;27;0;21;0
WireConnection;11;0;9;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;33;0;32;0
WireConnection;60;0;58;0
WireConnection;60;1;59;0
WireConnection;40;0;38;0
WireConnection;40;1;39;0
WireConnection;34;0;28;0
WireConnection;34;1;33;0
WireConnection;35;0;33;0
WireConnection;63;0;60;0
WireConnection;63;1;64;0
WireConnection;36;0;34;0
WireConnection;36;1;35;0
WireConnection;44;0;43;0
WireConnection;44;1;40;0
WireConnection;65;0;63;0
WireConnection;65;1;66;0
WireConnection;48;0;49;0
WireConnection;48;1;47;0
WireConnection;48;2;46;0
WireConnection;50;0;48;0
WireConnection;41;0;44;0
WireConnection;53;0;52;0
WireConnection;61;0;65;0
WireConnection;30;0;36;0
WireConnection;0;0;51;0
WireConnection;0;1;54;0
WireConnection;0;2;14;0
WireConnection;0;3;55;0
WireConnection;0;4;56;0
WireConnection;0;10;31;0
WireConnection;0;11;62;0
ASEEND*/
//CHKSM=39C899A22C7C04D01DBD6F80E8A7D3A6B8A91955