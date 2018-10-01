Shader "Custom/Dragon_Reflection" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal", 2D) = "Bump"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_rimpower ("Rim Power",Range(0,100))=0
		_power ("Power",Range(0,500))=0
		//_refractionpower("Powerefraction_powerr",Range(0,10))=0
		_cube("CUBE",cube)= ""{}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _rimpower;
		float _power;
		float _refractionpower;
		samplerCUBE _cube;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
			float3 lightDir;
			float3 worldRefl;
			INTERNAL_DATA
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutputStandard o) {

		  
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D (_BumpMap, IN.uv_BumpMap));

			float rim = saturate(dot(o.Normal,IN.viewDir));
			rim = pow(1-rim,_rimpower);
			//float4 refraction = pow(texCUBE(_cube, WorldReflectionVector(IN,o.Normal)),_refractionpower);

			o.Albedo = c.rgb;
			o.Emission = texCUBE(_cube, WorldReflectionVector(IN,o.Normal))*_power*rim * _Color ;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
