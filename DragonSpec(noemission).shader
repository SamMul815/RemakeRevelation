Shader "Custom/DragonSpec(noemission)" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Rimpow("Rimpower",Range(0,10)) = 2
		_specpow("specpow",float) = 3
		_Bumpmap("Normal",2D) = "bump" {}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Cu fullforwardshadows

			#pragma target 3.0

			sampler2D _MainTex , _Bumpmap, _MainTex2;
			float _Rimpow, _Emipower, _specpow;

			struct Input {
				float2 uv_MainTex;
				float2 uv_Bumpmap;
				float2 uv_MainTex2;
			};

			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Normal = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap));
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			float4 LightingCu(SurfaceOutput i, float3 lightDir, float3 viewDir, float atten)
			{
				//디퓨즈연산
				float NdotL = saturate(dot(i.Normal, lightDir));
				float3 diColor;
				diColor = NdotL * _LightColor0.rgb * atten;
				diColor *= i.Albedo;

				//림라이트
				float3 rimcolor;
				float rim = saturate(dot(i.Normal, viewDir));
				rim = pow(1 - rim, _Rimpow);
				rimcolor = (rim * i.Albedo * _Color) *atten;

				// 스펙큘러 연산
				float3 spec;
				float3 h = normalize(lightDir + viewDir);
				float NdotH = saturate(dot(h, i.Normal));
				NdotH = pow(NdotH, 100);
				spec = NdotH * (_LightColor0.rgb * _specpow);

				//최종

				float4 finalColor;
				finalColor.rgb = diColor +rimcolor + spec;
					//finalColor.rgb = diColor +rimcolor + spec;
				finalColor.a = i.Alpha;

				return finalColor;

			}
			ENDCG
	}
		FallBack "Diffuse"
}