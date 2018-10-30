Shader "Custom/MachineGun_Noise" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("noisde",2D) = "white" {}

	}
	SubShader {
		Tags { "RenderType"="Transparant" } cull off
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float4 color:Color;
		};	
		float4 color:Color;
		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutputStandard o) {
			float4 s = tex2D(_MainTex2, float2(IN.uv_MainTex2.x - _Time.y *0.3, IN.uv_MainTex2.y - _Time.x * 2));
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex + s.xy* 0.08) * _Color;
			o.Emission =c.rgb * IN.color.rgb;
			o.Alpha = c.a * IN.color.a;
		}
		ENDCG
	}
	FallBack "Transparant"
}
