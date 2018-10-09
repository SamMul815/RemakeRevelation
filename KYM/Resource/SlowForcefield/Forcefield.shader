// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:9201,x:32719,y:32712,varname:node_9201,prsc:2|emission-4294-OUT;n:type:ShaderForge.SFN_Multiply,id:4294,x:32500,y:32811,varname:node_4294,prsc:2|A-6223-OUT,B-9738-OUT;n:type:ShaderForge.SFN_Step,id:5529,x:31860,y:32980,varname:node_5529,prsc:2|A-4516-RGB,B-2373-OUT;n:type:ShaderForge.SFN_Step,id:1865,x:31860,y:33132,varname:node_1865,prsc:2|A-6810-OUT,B-4516-RGB;n:type:ShaderForge.SFN_Lerp,id:6223,x:31641,y:32799,varname:node_6223,prsc:2|A-7277-OUT,B-5855-OUT,T-2855-VFACE;n:type:ShaderForge.SFN_FaceSign,id:2855,x:31447,y:32980,varname:node_2855,prsc:2,fstp:0;n:type:ShaderForge.SFN_Lerp,id:7277,x:31447,y:32799,varname:node_7277,prsc:2|A-1077-OUT,B-5913-OUT,T-8205-OUT;n:type:ShaderForge.SFN_Multiply,id:1077,x:31236,y:32799,varname:node_1077,prsc:2|A-9246-RGB,B-8123-RGB,C-1297-OUT;n:type:ShaderForge.SFN_Multiply,id:5913,x:31236,y:32980,varname:node_5913,prsc:2|A-5750-RGB,B-1167-OUT;n:type:ShaderForge.SFN_DepthBlend,id:8205,x:31236,y:33154,varname:node_8205,prsc:2|DIST-497-OUT;n:type:ShaderForge.SFN_Lerp,id:5855,x:31447,y:32483,varname:node_5855,prsc:2|A-2164-OUT,B-313-OUT,T-8205-OUT;n:type:ShaderForge.SFN_Multiply,id:313,x:31236,y:32637,varname:node_313,prsc:2|A-5750-RGB,B-7317-OUT;n:type:ShaderForge.SFN_Multiply,id:2164,x:31236,y:32483,varname:node_2164,prsc:2|A-9246-RGB,B-8123-RGB,C-1297-OUT;n:type:ShaderForge.SFN_Tex2d,id:9246,x:30951,y:32409,ptovrint:False,ptlb:Depth texture,ptin:_Depthtexture,varname:node_9246,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fd002e28c3082e647998b5d647f22430,ntxv:0,isnm:False|UVIN-6039-OUT;n:type:ShaderForge.SFN_Color,id:8123,x:30951,y:32633,ptovrint:False,ptlb:Depth Color,ptin:_DepthColor,varname:node_8123,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1532223,c2:0.5600541,c3:0.8014706,c4:1;n:type:ShaderForge.SFN_Clamp01,id:7317,x:30951,y:32806,varname:node_7317,prsc:2|IN-3371-OUT;n:type:ShaderForge.SFN_Color,id:5750,x:30951,y:33035,ptovrint:False,ptlb:Color,ptin:_Color,varname:_DepthColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1532223,c2:0.8014706,c3:0.5332299,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:1297,x:30951,y:32956,ptovrint:False,ptlb:Depth color power,ptin:_Depthcolorpower,varname:node_1297,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.7;n:type:ShaderForge.SFN_ValueProperty,id:497,x:30946,y:33429,ptovrint:False,ptlb:Depth dist,ptin:_Depthdist,varname:_Depthcolorpower_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Add,id:6039,x:30752,y:32409,varname:node_6039,prsc:2|A-8917-OUT,B-3182-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8917,x:30541,y:32409,varname:node_8917,prsc:2|A-3938-OUT,B-6507-T;n:type:ShaderForge.SFN_TexCoord,id:3182,x:30541,y:32556,varname:node_3182,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Append,id:3938,x:30333,y:32409,varname:node_3938,prsc:2|A-2847-OUT,B-2238-OUT;n:type:ShaderForge.SFN_Time,id:6507,x:30333,y:32566,varname:node_6507,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2847,x:30136,y:32395,ptovrint:False,ptlb:U speed,ptin:_Uspeed,varname:node_2847,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:2238,x:30136,y:32524,ptovrint:False,ptlb:V speed,ptin:_Vspeed,varname:_node_2847_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.4;n:type:ShaderForge.SFN_Add,id:1167,x:30946,y:33280,varname:node_1167,prsc:2|A-28-RGB,B-6447-OUT;n:type:ShaderForge.SFN_Fresnel,id:3331,x:30333,y:32807,varname:node_3331,prsc:2|EXP-4696-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9134,x:30333,y:32975,ptovrint:False,ptlb:Fresnel power,ptin:_Fresnelpower,varname:node_9134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Slider,id:4696,x:30002,y:32820,ptovrint:False,ptlb:Fresnel stren,ptin:_Fresnelstren,varname:node_4696,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.5,max:3;n:type:ShaderForge.SFN_Tex2d,id:28,x:30333,y:33056,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_28,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fb459beb777e9f3489753f3c78859089,ntxv:0,isnm:False|UVIN-2869-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6447,x:30333,y:33257,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_6447,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Add,id:2869,x:30106,y:33073,varname:node_2869,prsc:2|A-7921-OUT,B-6370-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7921,x:29768,y:33064,varname:node_7921,prsc:2|A-2022-OUT,B-7025-T;n:type:ShaderForge.SFN_Append,id:2022,x:29560,y:33064,varname:node_2022,prsc:2|A-697-OUT,B-4120-OUT;n:type:ShaderForge.SFN_Time,id:7025,x:29560,y:33239,varname:node_7025,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:697,x:29341,y:33064,ptovrint:False,ptlb:Utex speed,ptin:_Utexspeed,varname:node_697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4120,x:29341,y:33162,ptovrint:False,ptlb:Vtex speed,ptin:_Vtexspeed,varname:_node_697_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.1;n:type:ShaderForge.SFN_Multiply,id:6711,x:32079,y:33049,varname:node_6711,prsc:2|A-5529-OUT,B-1865-OUT;n:type:ShaderForge.SFN_Slider,id:2373,x:31290,y:33329,ptovrint:False,ptlb:Step up,ptin:_Stepup,varname:_Stepup_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:6810,x:31276,y:33447,ptovrint:False,ptlb:Step down,ptin:_Stepdown,varname:_Stepdown_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Clamp01,id:9738,x:32277,y:33049,varname:node_9738,prsc:2|IN-6711-OUT;n:type:ShaderForge.SFN_Tex2d,id:4516,x:31627,y:32980,ptovrint:False,ptlb:Noisefade texture,ptin:_Noisefadetexture,varname:_node_5624_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fd002e28c3082e647998b5d647f22430,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:4868,x:30946,y:33210,ptovrint:False,ptlb:Tex color power,ptin:_Texcolorpower,varname:node_4868,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:4602,x:28914,y:33370,ptovrint:False,ptlb:noise tex,ptin:_noisetex,varname:node_4602,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fd002e28c3082e647998b5d647f22430,ntxv:0,isnm:False|UVIN-1490-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3371,x:30583,y:32838,varname:node_3371,prsc:2|A-3331-OUT,B-9134-OUT;n:type:ShaderForge.SFN_TexCoord,id:4786,x:28355,y:33480,varname:node_4786,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:4310,x:28537,y:33480,varname:node_4310,prsc:2|A-4786-UVOUT,B-6366-OUT;n:type:ShaderForge.SFN_Multiply,id:6366,x:28355,y:33624,varname:node_6366,prsc:2|A-4136-OUT,B-2434-T;n:type:ShaderForge.SFN_Append,id:4136,x:28164,y:33624,varname:node_4136,prsc:2|A-6839-OUT,B-7232-OUT;n:type:ShaderForge.SFN_Time,id:2434,x:28164,y:33766,varname:node_2434,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:6839,x:27930,y:33624,ptovrint:False,ptlb:node_6839,ptin:_node_6839,varname:node_6839,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:7232,x:27930,y:33704,ptovrint:False,ptlb:node_6839_copy,ptin:_node_6839_copy,varname:_node_6839_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_Tex2d,id:9546,x:28914,y:33593,ptovrint:False,ptlb:noise tex_2,ptin:_noisetex_2,varname:_noisetex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-8516-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2271,x:29355,y:33386,varname:node_2271,prsc:2|A-5822-OUT,B-5822-OUT;n:type:ShaderForge.SFN_TexCoord,id:1038,x:29355,y:33571,varname:node_1038,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:456,x:29563,y:33386,varname:node_456,prsc:2|A-2271-OUT,B-1038-UVOUT;n:type:ShaderForge.SFN_Panner,id:6370,x:29753,y:33386,varname:node_6370,prsc:2,spu:0,spv:0|UVIN-456-OUT;n:type:ShaderForge.SFN_Add,id:5822,x:29119,y:33468,varname:node_5822,prsc:2|A-4602-R,B-9546-G;n:type:ShaderForge.SFN_Panner,id:1490,x:28727,y:33393,varname:node_1490,prsc:2,spu:-0.02,spv:0.1|UVIN-4310-OUT;n:type:ShaderForge.SFN_Panner,id:8516,x:28727,y:33590,varname:node_8516,prsc:2,spu:0.15,spv:0.05|UVIN-4310-OUT;proporder:9246-8123-5750-1297-497-2847-2238-9134-4696-28-6447-697-4120-2373-6810-4516-4868-4602-6839-7232-9546;pass:END;sub:END;*/

Shader "Custom/Forcefield" {
    Properties {
        _Depthtexture ("Depth texture", 2D) = "white" {}
        _DepthColor ("Depth Color", Color) = (0.1532223,0.5600541,0.8014706,1)
        _Color ("Color", Color) = (0.1532223,0.8014706,0.5332299,1)
        _Depthcolorpower ("Depth color power", Float ) = 0.7
        _Depthdist ("Depth dist", Float ) = 0.15
        _Uspeed ("U speed", Float ) = 0
        _Vspeed ("V speed", Float ) = -0.4
        _Fresnelpower ("Fresnel power", Float ) = 0.5
        _Fresnelstren ("Fresnel stren", Range(0, 3)) = 1.5
        _MainTex ("MainTex", 2D) = "white" {}
        _Emission ("Emission", Float ) = 0.15
        _Utexspeed ("Utex speed", Float ) = 0
        _Vtexspeed ("Vtex speed", Float ) = -0.1
        _Stepup ("Step up", Range(-1, 1)) = 1
        _Stepdown ("Step down", Range(0, 1)) = 0
        _Noisefadetexture ("Noisefade texture", 2D) = "white" {}
        _Texcolorpower ("Tex color power", Float ) = 1
        _noisetex ("noise tex", 2D) = "white" {}
        _node_6839 ("node_6839", Float ) = 0.2
        _node_6839_copy ("node_6839_copy", Float ) = 0.3
        _noisetex_2 ("noise tex_2", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
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
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _Depthtexture; uniform float4 _Depthtexture_ST;
            uniform float4 _DepthColor;
            uniform float4 _Color;
            uniform float _Depthcolorpower;
            uniform float _Depthdist;
            uniform float _Uspeed;
            uniform float _Vspeed;
            uniform float _Fresnelpower;
            uniform float _Fresnelstren;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Emission;
            uniform float _Utexspeed;
            uniform float _Vtexspeed;
            uniform float _Stepup;
            uniform float _Stepdown;
            uniform sampler2D _Noisefadetexture; uniform float4 _Noisefadetexture_ST;
            uniform sampler2D _noisetex; uniform float4 _noisetex_ST;
            uniform float _node_6839;
            uniform float _node_6839_copy;
            uniform sampler2D _noisetex_2; uniform float4 _noisetex_2_ST;
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
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 node_6507 = _Time;
                float2 node_6039 = ((float2(_Uspeed,_Vspeed)*node_6507.g)+i.uv0);
                float4 _Depthtexture_var = tex2D(_Depthtexture,TRANSFORM_TEX(node_6039, _Depthtexture));
                float4 node_7025 = _Time;
                float4 node_4422 = _Time;
                float4 node_2434 = _Time;
                float2 node_4310 = (i.uv0+(float2(_node_6839,_node_6839_copy)*node_2434.g));
                float2 node_1490 = (node_4310+node_4422.g*float2(-0.02,0.1));
                float4 _noisetex_var = tex2D(_noisetex,TRANSFORM_TEX(node_1490, _noisetex));
                float2 node_8516 = (node_4310+node_4422.g*float2(0.15,0.05));
                float4 _noisetex_2_var = tex2D(_noisetex_2,TRANSFORM_TEX(node_8516, _noisetex_2));
                float node_5822 = (_noisetex_var.r+_noisetex_2_var.g);
                float2 node_2869 = ((float2(_Utexspeed,_Vtexspeed)*node_7025.g)+(((node_5822*node_5822)+i.uv0)+node_4422.g*float2(0,0)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2869, _MainTex));
                float node_8205 = saturate((sceneZ-partZ)/_Depthdist);
                float4 _Noisefadetexture_var = tex2D(_Noisefadetexture,TRANSFORM_TEX(i.uv0, _Noisefadetexture));
                float3 emissive = (lerp(lerp((_Depthtexture_var.rgb*_DepthColor.rgb*_Depthcolorpower),(_Color.rgb*(_MainTex_var.rgb+_Emission)),node_8205),lerp((_Depthtexture_var.rgb*_DepthColor.rgb*_Depthcolorpower),(_Color.rgb*saturate((pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnelstren)*_Fresnelpower))),node_8205),isFrontFace)*saturate((step(_Noisefadetexture_var.rgb,_Stepup)*step(_Stepdown,_Noisefadetexture_var.rgb))));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
