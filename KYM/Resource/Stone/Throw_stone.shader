Shader "StoneCustom/Throw_stone" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_RimCol("RimCol",Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[HDR]_MainTex2("Emission", 2D) = "white" {}
		_Emipower("EmissionHDR",Range(1,4)) = 1
		_Rimpow("Rimpower",Range(0,10)) = 2
		_specpow("specpow",float) = 3
		_Bumpmap ("Normal",2D) = "bump" {}
		[Toggle]_Rimonoff ("onoff",float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Cu fullforwardshadows noforwardadd

		#pragma target 3.0

		sampler2D _MainTex , _Bumpmap, _MainTex2;
		float _Rimpow, _Emipower, _specpow,_Rimonoff;
		float4 _RimCol;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bumpmap;
			float2 uv_MainTex2;
			float3 viewDir;
			float3 worldNormal;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			float4 d = tex2D(_MainTex2, IN.uv_MainTex2);
			o.Normal = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap));
			o.Albedo = c.rgb;
			o.Emission = (d.rgb * _Emipower) * (sin(_Time.y*4)*0.5+0.5);
			o.Alpha = c.a;
		}
		float4 LightingCu(SurfaceOutput i, float3 lightDir, float3 viewDir, float atten)
		{
			//디퓨즈연산
			float NdotL = (dot(i.Normal, lightDir));
			float3 diColor;
			diColor = NdotL * _LightColor0.rgb * atten;
			diColor *= i.Albedo;

			//림라이트
			float3 rimcolor ;
			float rim = abs(dot(normalize(i.Normal), normalize(viewDir)));
			rim = pow(1 - rim, _Rimpow);
			rimcolor = (rim * _RimCol) * _Rimonoff;
			
			// 스펙큘러 연산
			float3 spec;
			float3 h = normalize(lightDir + viewDir);
			float NdotH = saturate(dot(h, i.Normal));
			NdotH = pow(NdotH, 100);
			spec = NdotH * (_LightColor0.rgb * _specpow);

			//최종
			
			float4 finalColor;
			finalColor.rgb = diColor + rimcolor + spec;
			finalColor.a = i.Alpha;

			return finalColor;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
