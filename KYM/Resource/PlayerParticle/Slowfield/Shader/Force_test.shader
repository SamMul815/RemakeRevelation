Shader "Custom/BarrierShader" {
   Properties {
      _MainTex ("Albedo (RGB)", 2D) = "white" {}
      _MaskTex ("MaskTex",2D) = "black"{}
	  _Bumpmap ("Normal", 2D) = "bump" {}
	  _Noise("Noise", 2D) = "black" {}
      [HDR]_Color ("Color",Color) = (0,0,0,0)
   }
   SubShader {

      Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
      LOD 200
      cull back
      CGPROGRAM
      #pragma surface surf Barrier noshadow noambient alpha:fade
      #pragma target 3.0
      
      sampler2D _MainTex, _MaskTex, _Bumpmap , _Noise;
      float4 _Color;
	  
      struct Input {
         float2 uv_MainTex, uv_MaskTex , uv_Bumpmap , uv_Noise;      

      };
         
      void surf (Input IN, inout SurfaceOutput o) {

		 float4 d = tex2D(_Noise, float2(IN.uv_Noise.x + _Time.y * 0.05 , IN.uv_Noise.y + _Time.y * 0.2));
         fixed4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x - _Time.y * 0.05, IN.uv_MainTex.y) + d.xy);
		 o.Normal = UnpackNormal(tex2D(_Bumpmap,float2(IN.uv_Bumpmap.x , IN.uv_Bumpmap.y + _Time.y*0.2)));
         float4 m = tex2D(_MaskTex, float2(IN.uv_MaskTex.x, IN.uv_MaskTex.y + _Time.y*0.2) + d.xy);
         o.Albedo = c;
         o.Gloss = m.r;
         o.Alpha = c.a;
      }

      float4 LightingBarrier(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
      {
         float rim = saturate(dot(s.Normal, viewDir));
         float4 final;
         final.rgb = _Color;
         final.a = pow(1 - rim,5) + (s.Albedo*0.2) *s.Gloss;
         return final;
      }

      ENDCG
      Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
      LOD 200
      cull front
      CGPROGRAM
      #pragma surface surf Barrier noshadow noambient alpha:fade
      #pragma target 3.0
      
      sampler2D _MainTex, _MaskTex,_Bumpmap ,_Noise;
      float4 _Color;
      struct Input {
         float2 uv_MainTex, uv_MaskTex,uv_Bumpmap, uv_Noise;      

      };
      void surf (Input IN, inout SurfaceOutput o) {
         float4 d = tex2D(_Noise, float2(IN.uv_Noise.x + _Time.y * 0.05 , IN.uv_Noise.y + _Time.y * 0.2));
		 fixed4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x - _Time.y * 0.05, IN.uv_MainTex.y) + d.xy);
         float4 m = tex2D(_MaskTex, float2(IN.uv_MaskTex.x , IN.uv_MaskTex.y + _Time.y*0.2) + d.xy);
		 o.Normal = UnpackNormal(tex2D(_Bumpmap,float2(IN.uv_Bumpmap.x , IN.uv_Bumpmap.y + _Time.y*0.2)));
         o.Albedo = c;
         o.Gloss = m.r;
         o.Alpha = c.a;
      }

      float4 LightingBarrier(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
      {   
         float4 final;
         final.rgb = _Color;
         final.a = 0.05 + (s.Albedo*0.2) *s.Gloss ;
         return final;
      }
      ENDCG
   }
   FallBack "Transparant"
}