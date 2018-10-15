Shader "Custom/Distortion_effect" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Ref("Refpower",Range(0,0.2)) =0.05

	}
		SubShader{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
			LOD 200
	
		zwrite off
		GrabPass {}

		CGPROGRAM

		#pragma surface surf nolight noambient alpha:fade

		#pragma target 3.0

		sampler2D _MainTex ,_GrabTexture;
		float _Ref;

		struct Input {
			float4 color:COLOR;
			float2 uv_MainTex;
			float4 screenPos;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 ref = tex2D (_MainTex, IN.uv_MainTex);
			float3 screenUV = IN.screenPos.rgb / IN.screenPos.a;
			float4 r = tex2D(_GrabTexture, (screenUV.xy + ref.x * _Ref));
			o.Emission = r.rgb * IN.color.rgb;
			o.Alpha = ref.a* IN.color.a;
		}
		float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten) {
			return float4(0, 0, 0, 1);
		}
		ENDCG
	}
	FallBack "Transparent"
}
