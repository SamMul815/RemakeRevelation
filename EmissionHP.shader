Shader "Custom/EmissionHP" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[HDR]_Color2("Emission Color",Color) = (1,1,1,1)
		Epower("Emission power",Range(0,1)) = 0
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[HDR]_MainTex2("Emission",2D) = "white" {}
		_Bumpmap("Normalmap",2D) = "bump" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex, _MainTex2 , _Bumpmap;

		struct Input {
			float2 uv_MainTex, uv_MainTex2, uv_Bumpmap;
		};

		fixed4 _Color;
		float4 _Color2;
		float Epower;


		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float4 e = tex2D (_MainTex2,IN.uv_MainTex2) *_Color2;
			float3 customColor = e.rgb;
			customColor.r = customColor.r * Epower;
			o.Normal = UnpackNormal(tex2D(_Bumpmap,IN.uv_Bumpmap));
			o.Albedo = c.rgb;			
			o.Emission = customColor.rgb * Epower;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
