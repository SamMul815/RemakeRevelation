Shader "Custom/Dragon" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent"} cull off
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows alpha:fade

		#pragma target 3.0

		sampler2D _MainTex, _BumpMap;

		struct Input {
			float2 uv_MainTex, uv_BumpMap;
		};

		half _Glossiness;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Albedo = c.rgb;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
