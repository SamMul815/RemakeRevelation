Shader "Custom/trailHDR" {
	Properties {
		[HDR]_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200

		blend SrcAlpha One

		CGPROGRAM
		#pragma surface surf Standard noambient alpha:fade


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 color:Color;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Emission = c.rgb * IN.color.rgb;
			o.Alpha = c.a * IN.color.a;
		}
		ENDCG
	}
	FallBack "Transparent"
}
