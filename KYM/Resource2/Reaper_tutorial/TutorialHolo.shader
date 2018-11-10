Shader "Tutorial/TutorialHolo" {
	Properties {
		_Color ("RimColor", Color) = (1,1,1,1)
		_Bumpmap ("Normal map", 2D) = "bump" {}
		_Rimpow("Rimpower", Range(0,5)) = 1
		_NoiseTex("NoiseTex",2D) = "wihte" {}
		_Cut("Alpha Cut", Range(0,1)) =0
		[HDR]_OutColor("OutColor",Color) =(1,1,1,1)
		_OutThinkness ("OutThinkness",Range(1,1.5)) = 1.15
	}
		SubShader{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
			LOD 200
			zwrite on
			ColorMask 0
			CGPROGRAM
			#pragma surface surf nolight noambient noshadow
			#pragma target 3.0

			sampler2D _MainTex, _Bumpmap , _NoiseTex;

			struct Input {
				float4 color:COLOR;
			};

			void surf(Input IN, inout SurfaceOutput o) {

			}
			float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten) {
				return float4(0, 0, 0, 0);
				}

			ENDCG


				// 2pass 
				zwrite off
				CGPROGRAM
			#pragma surface surf Lambert noambient alpha:fade
			#pragma target 3.0

				sampler2D _MainTex, _Bumpmap,_NoiseTex;

			struct Input {
				float2 uv_MainTex, uv_Bumpmap , uv_NoiseTex;
				float3 viewDir;
			};

			float4 _Color;
			float _Rimpow;
			float4 _OutColor;
			float _Cut;
			float _OutThinkness;

			void surf(Input IN, inout SurfaceOutput o) {
				o.Normal = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap));
				float4 noise = tex2D(_NoiseTex, IN.uv_NoiseTex);
				float rim = dot(o.Normal, IN.viewDir);
				float al;
				if (noise.r >= _Cut)
					al = 1;
				else
					al = 0;


				float outline;
				if (noise.r >= _Cut * _OutThinkness)
					outline = 0;
				else
					outline = 1;

				o.Emission = (outline * _OutColor.rgb) + _Color;
				o.Alpha = al * pow(1 - rim, _Rimpow);
			}
			ENDCG
		}
	FallBack "Diffuse"
}
