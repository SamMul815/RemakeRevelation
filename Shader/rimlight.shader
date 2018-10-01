Shader "Custom/rimlight" {
	Properties {
		_rimcol ("rimcolor",Color) = (1,1,1,1)
		_rimpow ("rimpower",float) = 1
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Bumpmap("Normal",2D) = "bump"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		float4 _rimcol;
		float _rimpow;
		sampler2D _Bumpmap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bumpmap;
			float3 viewDir;
			float3 lightDir;
		};

		half _Glossiness;
		half _Metallic;


		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap));
			float rim =saturate(dot(o.Normal, IN.viewDir));
			float rim1 = pow(1 - rim, _rimpow);
			o.Emission = (rim1 * _rimcol) / 3;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
