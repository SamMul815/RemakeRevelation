// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1280277,fgcg:0.1953466,fgcb:0.2352941,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2790,x:32952,y:32738,varname:node_2790,prsc:2|diff-8453-RGB,diffpow-1856-OUT,spec-3275-OUT,gloss-1856-OUT,normal-3012-OUT,emission-7285-OUT,alpha-2138-OUT,refract-9005-OUT;n:type:ShaderForge.SFN_Color,id:8453,x:32504,y:32316,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8453,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5441177,c2:0.6415821,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:3438,x:32124,y:32892,varname:node_3438,prsc:2|EXP-4747-OUT;n:type:ShaderForge.SFN_Vector1,id:4747,x:31926,y:32947,varname:node_4747,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:9830,x:32333,y:32892,varname:node_9830,prsc:2|A-3438-OUT,B-500-OUT;n:type:ShaderForge.SFN_ValueProperty,id:500,x:32124,y:33031,ptovrint:False,ptlb:Fresnel stranch,ptin:_Fresnelstranch,varname:node_500,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:9755,x:32504,y:32892,varname:node_9755,prsc:2|A-9830-OUT,B-2745-RGB,C-7949-RGB;n:type:ShaderForge.SFN_Cubemap,id:2745,x:32333,y:33031,ptovrint:False,ptlb:Cubemap,ptin:_Cubemap,varname:node_2745,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:57c3bf79e54755e4490d64a9624aef37,pvfc:0;n:type:ShaderForge.SFN_Tex2d,id:7949,x:32333,y:33199,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_7949,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cad1dcc3f26219247aa1d733207a2667,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:2138,x:32546,y:33071,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_2138,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.25,max:1;n:type:ShaderForge.SFN_Tex2d,id:5673,x:32124,y:32722,ptovrint:False,ptlb:Normal map,ptin:_Normalmap,varname:node_5673,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ba2447429bf76b745a2e74e5a7ec7c91,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:2707,x:32321,y:32722,varname:node_2707,prsc:2|A-1264-OUT,B-5673-RGB;n:type:ShaderForge.SFN_Slider,id:1264,x:32035,y:32636,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:node_1264,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0.5,max:2;n:type:ShaderForge.SFN_ComponentMask,id:9005,x:32504,y:32722,varname:node_9005,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2707-OUT;n:type:ShaderForge.SFN_Lerp,id:3012,x:32504,y:32582,varname:node_3012,prsc:2|A-8402-OUT,B-2707-OUT,T-2957-OUT;n:type:ShaderForge.SFN_Vector1,id:2957,x:32321,y:32663,varname:node_2957,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Vector3,id:8402,x:32310,y:32506,varname:node_8402,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Vector1,id:1856,x:32504,y:32465,varname:node_1856,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3275,x:32504,y:32524,varname:node_3275,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Clamp01,id:7285,x:32685,y:32892,varname:node_7285,prsc:2|IN-9755-OUT;proporder:8453-500-2745-7949-2138-5673-1264;pass:END;sub:END;*/

Shader "Custom/testcrystal" {
    Properties {
        _Color ("Color", Color) = (0.5441177,0.6415821,1,1)
        _Fresnelstranch ("Fresnel stranch", Float ) = 2
        _Cubemap ("Cubemap", Cube) = "_Skybox" {}
        _Texture ("Texture", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.25
        _Normalmap ("Normal map", 2D) = "bump" {}
        _Distortion ("Distortion", Range(-2, 2)) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Fresnelstranch;
            uniform samplerCUBE _Cubemap;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Opacity;
            uniform sampler2D _Normalmap; uniform float4 _Normalmap_ST;
            uniform float _Distortion;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normalmap_var = UnpackNormal(tex2D(_Normalmap,TRANSFORM_TEX(i.uv0, _Normalmap)));
                float3 node_2707 = (_Distortion*_Normalmap_var.rgb);
                float3 normalLocal = lerp(float3(0,0,1),node_2707,0.1);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + node_2707.rg;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_1856 = 1.0;
                float gloss = node_1856;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float node_3275 = 0.2;
                float3 specularColor = float3(node_3275,node_3275,node_3275);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = pow(max( 0.0, NdotL), node_1856) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 emissive = saturate(((pow(1.0-max(0,dot(normalDirection, viewDirection)),0.0)*_Fresnelstranch)*texCUBE(_Cubemap,viewReflectDirection).rgb*_Texture_var.rgb));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Fresnelstranch;
            uniform samplerCUBE _Cubemap;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Opacity;
            uniform sampler2D _Normalmap; uniform float4 _Normalmap_ST;
            uniform float _Distortion;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normalmap_var = UnpackNormal(tex2D(_Normalmap,TRANSFORM_TEX(i.uv0, _Normalmap)));
                float3 node_2707 = (_Distortion*_Normalmap_var.rgb);
                float3 normalLocal = lerp(float3(0,0,1),node_2707,0.1);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + node_2707.rg;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_1856 = 1.0;
                float gloss = node_1856;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float node_3275 = 0.2;
                float3 specularColor = float3(node_3275,node_3275,node_3275);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = pow(max( 0.0, NdotL), node_1856) * attenColor;
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _Opacity,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
