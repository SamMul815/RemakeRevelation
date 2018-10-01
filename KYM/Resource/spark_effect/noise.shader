Shader "Custom/noise" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("noisde",2D) = "white" {}
		_Slide("Noisepower",Range(0,2)) = 0.2
		_uspeed("u.scroll",Range(0,1)) = 0.1
		_vspeed("v.scroll",Range(0,1)) = 0.1

	}
	SubShader {
		Tags { "RenderType"="Transparant"} cull off
		LOD 200

		CGPROGRAM

		#pragma surface surf Lambert alpha:fade

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		float _Slide, _uspeed , _vspeed;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float4 color:Color;
		};	
		float4 color:Color;
		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutput o) {
			float4 n = tex2D(_MainTex2, float2(IN.uv_MainTex2.x - _Time.y * _uspeed, IN.uv_MainTex2.y - _Time.y * _vspeed));
			float4 c = tex2D (_MainTex, IN.uv_MainTex + n.xy * _Slide) * _Color;
			o.Emission =c.rgb * IN.color.rgb;
			o.Alpha = (c.a * IN.color.a) * 0.5;
		}
		ENDCG
	}
	FallBack "Transparent"
}
