Shader "Custom/NewSurfaceShader" {
	Properties {
		[HDR]_EColor ("Burning Color", Color) = (1,1,1,1) //타들어가는부분 밝기/색
		[HDR]_Color ("Default Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {} //기본텍스쳐
		_BumpMap ("Normal", 2D) = "Bump" {}
		_NoiseTex ("NoiseTex", 2D) = "white" {} //타들어가는 모양 결정하는텍스쳐
		_Cut("Alpha Cut", Range(0,1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"= "Opaque"} 
		LOD 200
		cull off
		zwrite on
		CGPROGRAM
	
		#pragma surface surf Standard fullfowardshadows alpha:fade

		#pragma target 3.0

		sampler2D _MainTex , _CameraDepthTexture;
		sampler2D _NoiseTex;
		sampler2D _BumpMap;
		float _Cut;
		float4 _EColor;
		float4 _Color;

		struct Input {
			float2 uv_BumpMap;
			float2 uv_MainTex;
			float2 uv_NoiseTex;
			float4 color:Color;
			float4 screenPos;
		};

	

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 sPos = float2(IN.screenPos.x, IN.screenPos.y) / IN.screenPos.w;
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			float4 Depth = tex2D(_CameraDepthTexture, sPos);
			fixed4 noise = tex2D (_NoiseTex, IN.uv_NoiseTex);
			o.Albedo = (c.rgb*_Color*IN.color.rgb) * Depth.r;
			o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uv_BumpMap));
		

			float alpha;
			if (noise.r>= _Cut) //알파값은  변수보다 크거나 같을때만 보인다
			alpha = 1;
			else
			alpha = 0;

			float outline; //아웃라인 색은 가장자리, 기존 텍스쳐보다 넓은 범위에서 보여야하기 때문에 쩜n배키워줌
			if (noise.r >= _Cut * 1.15)
			outline =0;
			else
			outline =1;

			o.Emission = outline*_EColor; // 0 or1 * 색
			o.Alpha = alpha*c.a*IN.color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
