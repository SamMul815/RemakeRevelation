// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:3,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4954,x:32719,y:32712,varname:node_4954,prsc:2|custl-7761-OUT,clip-5537-OUT;n:type:ShaderForge.SFN_Get,id:5537,x:32502,y:32997,varname:node_5537,prsc:2|IN-4255-OUT;n:type:ShaderForge.SFN_Get,id:7761,x:32502,y:32942,varname:node_7761,prsc:2|IN-3129-OUT;n:type:ShaderForge.SFN_Set,id:3751,x:31834,y:32421,varname:AmbientOcclusion,prsc:2|IN-1074-OUT;n:type:ShaderForge.SFN_Clamp01,id:1074,x:31666,y:32421,varname:node_1074,prsc:2|IN-4330-OUT;n:type:ShaderForge.SFN_Add,id:4330,x:31508,y:32421,varname:node_4330,prsc:2|A-1301-R,B-2320-OUT;n:type:ShaderForge.SFN_Tex2d,id:1301,x:31220,y:32335,ptovrint:False,ptlb:A.O,ptin:_AO,varname:node_1301,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_OneMinus,id:2320,x:31220,y:32504,varname:node_2320,prsc:2|IN-5980-OUT;n:type:ShaderForge.SFN_Slider,id:5980,x:30892,y:32504,ptovrint:False,ptlb:A.O_strength,ptin:_AO_strength,varname:node_5980,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Set,id:3129,x:31840,y:32806,varname:mainColor,prsc:2|IN-8105-OUT;n:type:ShaderForge.SFN_Lerp,id:8105,x:31686,y:32806,varname:node_8105,prsc:2|A-1352-OUT,B-6959-OUT,T-870-OUT;n:type:ShaderForge.SFN_Vector1,id:1352,x:31516,y:32771,varname:node_1352,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:6959,x:31503,y:32826,varname:node_6959,prsc:2|A-6076-OUT,B-7783-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:870,x:31503,y:32957,varname:node_870,prsc:2;n:type:ShaderForge.SFN_Abs,id:7783,x:31310,y:32898,varname:node_7783,prsc:2|IN-4597-OUT;n:type:ShaderForge.SFN_Multiply,id:4597,x:31153,y:32898,varname:node_4597,prsc:2|A-6076-OUT,B-7963-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7963,x:30948,y:32981,ptovrint:False,ptlb:Emission strength,ptin:_Emissionstrength,varname:node_7963,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:6076,x:30948,y:32832,varname:node_6076,prsc:2|A-2096-RGB,B-8287-RGB,C-8674-OUT;n:type:ShaderForge.SFN_Tex2d,id:2096,x:30706,y:32709,ptovrint:False,ptlb:Main_Texture,ptin:_Main_Texture,varname:node_2096,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:8287,x:30706,y:32895,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8287,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.3,c3:0.7,c4:1;n:type:ShaderForge.SFN_Get,id:8674,x:30685,y:33046,varname:node_8674,prsc:2|IN-3751-OUT;n:type:ShaderForge.SFN_Set,id:1562,x:31849,y:33269,varname:scanLine,prsc:2|IN-206-OUT;n:type:ShaderForge.SFN_Lerp,id:206,x:31687,y:33269,varname:node_206,prsc:2|A-4642-OUT,B-259-OUT,T-8091-OUT;n:type:ShaderForge.SFN_Vector1,id:4642,x:31478,y:33237,varname:node_4642,prsc:2,v1:1;n:type:ShaderForge.SFN_Sin,id:259,x:31478,y:33291,varname:node_259,prsc:2|IN-6506-OUT;n:type:ShaderForge.SFN_Slider,id:8091,x:31321,y:33435,ptovrint:False,ptlb:ScanLine strength,ptin:_ScanLinestrength,varname:node_8091,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6506,x:31308,y:33291,varname:node_6506,prsc:2|A-5734-OUT,B-3909-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5734,x:31097,y:33269,ptovrint:False,ptlb:Scanline scale,ptin:_Scanlinescale,varname:node_5734,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:128;n:type:ShaderForge.SFN_Add,id:3909,x:31097,y:33339,varname:node_3909,prsc:2|A-7377-Y,B-9123-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7377,x:30862,y:33269,varname:node_7377,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9123,x:30873,y:33395,varname:node_9123,prsc:2|A-5228-TSL,B-5818-OUT;n:type:ShaderForge.SFN_Time,id:5228,x:30642,y:33314,varname:node_5228,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:5818,x:30629,y:33490,ptovrint:False,ptlb:animation Stength,ptin:_animationStength,varname:node_5818,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Set,id:4255,x:31853,y:33728,varname:opacityclip,prsc:2|IN-24-OUT;n:type:ShaderForge.SFN_Lerp,id:24,x:31696,y:33728,varname:node_24,prsc:2|A-1982-OUT,B-5021-OUT,T-5759-OUT;n:type:ShaderForge.SFN_Vector1,id:1982,x:31500,y:33694,varname:node_1982,prsc:2,v1:0.7;n:type:ShaderForge.SFN_Multiply,id:5021,x:31500,y:33751,varname:node_5021,prsc:2|A-6071-OUT,B-8202-OUT,C-4107-OUT,D-3914-OUT;n:type:ShaderForge.SFN_Clamp01,id:5759,x:31500,y:33883,varname:node_5759,prsc:2|IN-9854-OUT;n:type:ShaderForge.SFN_Slider,id:6071,x:31114,y:33680,ptovrint:False,ptlb:Opacity Strength,ptin:_OpacityStrength,varname:node_6071,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_OneMinus,id:8202,x:31271,y:33751,varname:node_8202,prsc:2|IN-2212-OUT;n:type:ShaderForge.SFN_Get,id:4107,x:31250,y:33872,varname:node_4107,prsc:2|IN-3751-OUT;n:type:ShaderForge.SFN_Get,id:3914,x:31250,y:33919,varname:node_3914,prsc:2|IN-1562-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:9854,x:31271,y:33972,varname:node_9854,prsc:2,chbt:0|M-8238-RGB,R-7319-R,G-7319-G,B-7319-B;n:type:ShaderForge.SFN_Color,id:8238,x:31019,y:33919,ptovrint:False,ptlb:Light Color Mask,ptin:_LightColorMask,varname:node_8238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2212,x:31019,y:33751,varname:node_2212,prsc:2|EXP-1908-OUT;n:type:ShaderForge.SFN_Slider,id:1908,x:30699,y:33778,ptovrint:False,ptlb:Fresnel Power,ptin:_FresnelPower,varname:node_1908,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.709402,max:20;n:type:ShaderForge.SFN_LightColor,id:7319,x:31007,y:34077,varname:node_7319,prsc:2;proporder:1301-5980-8091-5734-5818-6071-8238-1908-7963-2096-8287;pass:END;sub:END;*/

Shader "Tutorial/TutorialHologram_noise" {
    Properties {
        _AO ("A.O", 2D) = "bump" {}
        _AO_strength ("A.O_strength", Range(0, 1)) = 0
        _ScanLinestrength ("ScanLine strength", Range(0, 1)) = 0
        _Scanlinescale ("Scanline scale", Float ) = 128
        _animationStength ("animation Stength", Float ) = 0
        _OpacityStrength ("Opacity Strength", Range(0, 1)) = 0.8
        _LightColorMask ("Light Color Mask", Color) = (1,1,1,1)
        _FresnelPower ("Fresnel Power", Range(0, 20)) = 1.709402
        _Emissionstrength ("Emission strength", Float ) = 0
        _Main_Texture ("Main_Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0,0.3,0.7,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 4x4 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither4x4( float value, float2 sceneUVs ) {
                float4x4 mtx = float4x4(
                    float4( 1,  9,  3, 11 )/17.0,
                    float4( 13, 5, 15,  7 )/17.0,
                    float4( 4, 12,  2, 10 )/17.0,
                    float4( 16, 8, 14,  6 )/17.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,4);
                int ySmp = fmod(px.y,4);
                float4 xVec = 1-saturate(abs(float4(0,1,2,3) - xSmp));
                float4 yVec = 1-saturate(abs(float4(0,1,2,3) - ySmp));
                float4 pxMult = float4( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec), dot(mtx[3],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform sampler2D _AO; uniform float4 _AO_ST;
            uniform float _AO_strength;
            uniform float _Emissionstrength;
            uniform sampler2D _Main_Texture; uniform float4 _Main_Texture_ST;
            uniform float4 _Color;
            uniform float _ScanLinestrength;
            uniform float _Scanlinescale;
            uniform float _animationStength;
            uniform float _OpacityStrength;
            uniform float4 _LightColorMask;
            uniform float _FresnelPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 _AO_var = UnpackNormal(tex2D(_AO,TRANSFORM_TEX(i.uv0, _AO)));
                float AmbientOcclusion = saturate((_AO_var.r+(1.0 - _AO_strength)));
                float4 node_5228 = _Time;
                float scanLine = lerp(1.0,sin((_Scanlinescale*(i.posWorld.g+(node_5228.r*_animationStength)))),_ScanLinestrength);
                float opacityclip = lerp(0.7,(_OpacityStrength*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower))*AmbientOcclusion*scanLine),saturate((_LightColorMask.rgb.r*_LightColor0.r + _LightColorMask.rgb.g*_LightColor0.g + _LightColorMask.rgb.b*_LightColor0.b)));
                clip( BinaryDither4x4(opacityclip - 1.5, sceneUVs) );
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_1352 = 0.0;
                float4 _Main_Texture_var = tex2D(_Main_Texture,TRANSFORM_TEX(i.uv0, _Main_Texture));
                float3 node_6076 = (_Main_Texture_var.rgb*_Color.rgb*AmbientOcclusion);
                float3 mainColor = lerp(float3(node_1352,node_1352,node_1352),(node_6076+abs((node_6076*_Emissionstrength))),attenuation);
                float3 finalColor = mainColor;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 4x4 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither4x4( float value, float2 sceneUVs ) {
                float4x4 mtx = float4x4(
                    float4( 1,  9,  3, 11 )/17.0,
                    float4( 13, 5, 15,  7 )/17.0,
                    float4( 4, 12,  2, 10 )/17.0,
                    float4( 16, 8, 14,  6 )/17.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,4);
                int ySmp = fmod(px.y,4);
                float4 xVec = 1-saturate(abs(float4(0,1,2,3) - xSmp));
                float4 yVec = 1-saturate(abs(float4(0,1,2,3) - ySmp));
                float4 pxMult = float4( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec), dot(mtx[3],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform sampler2D _AO; uniform float4 _AO_ST;
            uniform float _AO_strength;
            uniform float _Emissionstrength;
            uniform sampler2D _Main_Texture; uniform float4 _Main_Texture_ST;
            uniform float4 _Color;
            uniform float _ScanLinestrength;
            uniform float _Scanlinescale;
            uniform float _animationStength;
            uniform float _OpacityStrength;
            uniform float4 _LightColorMask;
            uniform float _FresnelPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 _AO_var = UnpackNormal(tex2D(_AO,TRANSFORM_TEX(i.uv0, _AO)));
                float AmbientOcclusion = saturate((_AO_var.r+(1.0 - _AO_strength)));
                float4 node_5228 = _Time;
                float scanLine = lerp(1.0,sin((_Scanlinescale*(i.posWorld.g+(node_5228.r*_animationStength)))),_ScanLinestrength);
                float opacityclip = lerp(0.7,(_OpacityStrength*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower))*AmbientOcclusion*scanLine),saturate((_LightColorMask.rgb.r*_LightColor0.r + _LightColorMask.rgb.g*_LightColor0.g + _LightColorMask.rgb.b*_LightColor0.b)));
                clip( BinaryDither4x4(opacityclip - 1.5, sceneUVs) );
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_1352 = 0.0;
                float4 _Main_Texture_var = tex2D(_Main_Texture,TRANSFORM_TEX(i.uv0, _Main_Texture));
                float3 node_6076 = (_Main_Texture_var.rgb*_Color.rgb*AmbientOcclusion);
                float3 mainColor = lerp(float3(node_1352,node_1352,node_1352),(node_6076+abs((node_6076*_Emissionstrength))),attenuation);
                float3 finalColor = mainColor;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 4x4 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither4x4( float value, float2 sceneUVs ) {
                float4x4 mtx = float4x4(
                    float4( 1,  9,  3, 11 )/17.0,
                    float4( 13, 5, 15,  7 )/17.0,
                    float4( 4, 12,  2, 10 )/17.0,
                    float4( 16, 8, 14,  6 )/17.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,4);
                int ySmp = fmod(px.y,4);
                float4 xVec = 1-saturate(abs(float4(0,1,2,3) - xSmp));
                float4 yVec = 1-saturate(abs(float4(0,1,2,3) - ySmp));
                float4 pxMult = float4( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec), dot(mtx[3],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform sampler2D _AO; uniform float4 _AO_ST;
            uniform float _AO_strength;
            uniform float _ScanLinestrength;
            uniform float _Scanlinescale;
            uniform float _animationStength;
            uniform float _OpacityStrength;
            uniform float4 _LightColorMask;
            uniform float _FresnelPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 projPos : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 _AO_var = UnpackNormal(tex2D(_AO,TRANSFORM_TEX(i.uv0, _AO)));
                float AmbientOcclusion = saturate((_AO_var.r+(1.0 - _AO_strength)));
                float4 node_5228 = _Time;
                float scanLine = lerp(1.0,sin((_Scanlinescale*(i.posWorld.g+(node_5228.r*_animationStength)))),_ScanLinestrength);
                float opacityclip = lerp(0.7,(_OpacityStrength*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelPower))*AmbientOcclusion*scanLine),saturate((_LightColorMask.rgb.r*_LightColor0.r + _LightColorMask.rgb.g*_LightColor0.g + _LightColorMask.rgb.b*_LightColor0.b)));
                clip( BinaryDither4x4(opacityclip - 1.5, sceneUVs) );
                float3 lightColor = _LightColor0.rgb;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
