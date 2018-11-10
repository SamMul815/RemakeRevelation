// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1280277,fgcg:0.1953466,fgcb:0.2352941,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9571,x:32719,y:32712,varname:node_9571,prsc:2|normal-7177-OUT,alpha-4401-OUT,refract-757-OUT;n:type:ShaderForge.SFN_Tex2d,id:6557,x:31354,y:32645,ptovrint:False,ptlb:Refraction_tex,ptin:_Refraction_tex,varname:node_6557,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:61b59b0922966a14ca467f6e6ff7f341,ntxv:3,isnm:True|UVIN-577-UVOUT;n:type:ShaderForge.SFN_Rotator,id:577,x:31182,y:32645,varname:node_577,prsc:2|UVIN-8725-UVOUT,SPD-9041-OUT;n:type:ShaderForge.SFN_TexCoord,id:8725,x:30948,y:32645,varname:node_8725,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:9041,x:30791,y:32833,ptovrint:False,ptlb:Rotation Speed,ptin:_RotationSpeed,varname:node_9041,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:6756,x:31197,y:32861,ptovrint:False,ptlb:Normal intensicy,ptin:_Normalintensicy,varname:node_6756,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Vector3,id:658,x:31354,y:32515,varname:node_658,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:7177,x:31604,y:32645,varname:node_7177,prsc:2|A-658-OUT,B-6557-RGB,T-6756-OUT;n:type:ShaderForge.SFN_Slider,id:5945,x:31197,y:32998,ptovrint:False,ptlb:Rafraction Value,ptin:_RafractionValue,varname:node_5945,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.5,cur:0,max:0.5;n:type:ShaderForge.SFN_Multiply,id:5131,x:31614,y:32966,varname:node_5131,prsc:2|A-6756-OUT,B-5945-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6461,x:31614,y:32786,varname:node_6461,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6557-RGB;n:type:ShaderForge.SFN_Multiply,id:3580,x:31813,y:32836,varname:node_3580,prsc:2|A-6461-OUT,B-5131-OUT;n:type:ShaderForge.SFN_Tex2d,id:3218,x:31813,y:33045,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_3218,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3a5a96df060a5cf4a9cc0c59e13486b7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:4401,x:32067,y:32836,varname:node_4401,prsc:2|A-2629-OUT,B-3218-R;n:type:ShaderForge.SFN_ValueProperty,id:2629,x:31844,y:32763,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_2629,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ComponentMask,id:2053,x:31990,y:33064,varname:node_2053,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3218-R;n:type:ShaderForge.SFN_Multiply,id:2470,x:32168,y:33010,varname:node_2470,prsc:2|A-3580-OUT,B-2053-OUT;n:type:ShaderForge.SFN_VertexColor,id:7156,x:31990,y:33218,varname:node_7156,prsc:2;n:type:ShaderForge.SFN_Color,id:7071,x:31990,y:33366,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7071,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9217,x:32162,y:33274,varname:node_9217,prsc:2|A-7156-A,B-7071-A;n:type:ShaderForge.SFN_Multiply,id:757,x:32484,y:33053,varname:node_757,prsc:2|A-2470-OUT,B-9217-OUT;proporder:6557-9041-6756-3218-2629-5945-7071;pass:END;sub:END;*/

Shader "Custom/Distortion_effect" {
    Properties {
        _Refraction_tex ("Refraction_tex", 2D) = "bump" {}
        _RotationSpeed ("Rotation Speed", Range(0, 5)) = 0
        _Normalintensicy ("Normal intensicy", Range(-5, 5)) = 0
        _Opacity ("Opacity", 2D) = "white" {}
        _Value ("Value", Float ) = 0
        _RafractionValue ("Rafraction Value", Range(-0.5, 0.5)) = 0
        _Color ("Color", Color) = (1,1,1,1)
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Refraction_tex; uniform float4 _Refraction_tex_ST;
            uniform float _RotationSpeed;
            uniform float _Normalintensicy;
            uniform float _RafractionValue;
            uniform sampler2D _Opacity; uniform float4 _Opacity_ST;
            uniform float _Value;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float4 node_3455 = _Time;
                float node_577_ang = node_3455.g;
                float node_577_spd = _RotationSpeed;
                float node_577_cos = cos(node_577_spd*node_577_ang);
                float node_577_sin = sin(node_577_spd*node_577_ang);
                float2 node_577_piv = float2(0.5,0.5);
                float2 node_577 = (mul(i.uv0-node_577_piv,float2x2( node_577_cos, -node_577_sin, node_577_sin, node_577_cos))+node_577_piv);
                float3 _Refraction_tex_var = UnpackNormal(tex2D(_Refraction_tex,TRANSFORM_TEX(node_577, _Refraction_tex)));
                float3 normalLocal = lerp(float3(0,0,1),_Refraction_tex_var.rgb,_Normalintensicy);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(i.uv0, _Opacity));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (((_Refraction_tex_var.rgb.rg*(_Normalintensicy*_RafractionValue))*_Opacity_var.r.r)*(i.vertexColor.a*_Color.a));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(_Value*_Opacity_var.r)),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
