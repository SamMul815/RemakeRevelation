Shader "Custom/fefldragon" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_color("color" ,color) = (1,1,1,1)
		_pow("power",float) = 1
		_Bumpmap ("Normal",2D) = "bump"{}
		_Cube("Cubemap",cube) = ""{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard 


		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bumpmap;
		samplerCUBE _Cube;
		float4 _color;
		float _pow;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bumpmap;
			float3 worldRefl;
			float3 viewDir;
			float3 lightDir;
			INTERNAL_DATA
		};

		half _Glossiness;
		half _Metallic;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap));
			//프레넬
			float fr = saturate(dot(o.Normal, IN.viewDir));
			//림라이트
			float rim = pow(1 - fr, 4);
			//스카이박스 반사
			//float3 refltex = texCUBE(_Cube,WorldReflectionVector(IN, o.Normal)).rgb;
			//최종연산
			//o.Emission = lerp(refltex, fr, (c.rgb * _pow));
			o.Emission = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal)).rgb * _pow *rim  * c.rgb;
			//o.Emission = rim;
			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
