Shader "ErbGameArt/Skybox Pro" {
    Properties {
        _Skycubemap ("Sky cubemap", Cube) = "_Skybox" {}
        [MaterialToggle] _Useskycubemap ("Use sky cubemap?", Float ) = 0
        _Skycubemap2 ("Sky cubemap2", Cube) = "_Skybox" {}
        [MaterialToggle] _Usesecondcubemap ("Use second cubemap?", Float ) = 0
        _SkyColor ("Sky Color", Color) = (1,1,1,1)
        _HorizonColor ("Horizon Color", Color) = (1,1,1,1)
        _Nightskyintensity ("Night sky intensity", Range(-10, 0)) = -0.2
        _Sunsetcolor ("Sunset color", Color) = (1,0.328,0,1)
        _SunRadiusB ("Sun Radius B", Range(0, 0.2)) = 0
        _SunRadiusA ("Sun Radius A", Range(0, 0.2)) = 0.05
        _SunIntensity ("Sun Intensity", Float ) = 2
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Background"
            "RenderType"="Opaque"
            "PreviewType"="Skybox"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            uniform float4 _LightColor0;
            uniform float _SunRadiusB;
            uniform float _SunRadiusA;
            uniform float _SunIntensity;
            uniform samplerCUBE _Skycubemap;
            uniform float4 _Sunsetcolor;
            uniform samplerCUBE _Skycubemap2;
            uniform fixed _Usesecondcubemap;
            uniform float _Nightskyintensity;
            uniform float4 _SkyColor;
            uniform float4 _HorizonColor;
            uniform fixed _Useskycubemap;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float4 _Skycubemap_var = texCUBE(_Skycubemap,viewReflectDirection);
                float ldir = distance(lightDirection,float3(0,-1,0));
                float LConst = (ldir*0.4545454+0.09090909);
                float minsun = min(_SunRadiusA,_SunRadiusB);
                float dif = (1.0 - (minsun*minsun));
                float sun = max(_SunRadiusA,_SunRadiusB);
                float3 emissive = (saturate((lerp( 1.0, saturate(lerp( _Skycubemap_var.rgb, lerp(texCUBE(_Skycubemap2,viewReflectDirection).rgb,_Skycubemap_var.rgb,LConst), _Usesecondcubemap )), _Useskycubemap )*(0.0 + ( (ldir - _Nightskyintensity) * (1.0 - 0.0) ) / (2.0 - _Nightskyintensity))*lerp(_SkyColor.rgb,_HorizonColor.rgb,pow((1.0 - max(0,dot(viewDirection,float3(0,-1,0)))),8.0))))+(_LightColor0.rgb*pow(saturate((1.0 + ( (max(0,dot((-1*lightDirection),viewDirection)) - dif) * (0.0 - 1.0) ) / ((1.0 - (sun*sun)) - dif))),5.0)*_SunIntensity*(_Sunsetcolor.rgb*LConst)));
                return fixed4(emissive,1);
            }
            ENDCG
        }
    }
}
