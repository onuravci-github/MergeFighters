Shader "Custom/Toon 1" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _ColorTex ("ColorTex", 2D) = "white" {}
        _DarkColorTex ("DarkColorTex", 2D) = "white" {}
        _DotColorTex ("DotColorTex", 2D) = "white" {}
        _DotTex ("DotTex", 2D) = "white" {}
        _DotSize ("DotSize", Float ) = 4
        _DotRotation ("DotRotation", Range(0, 360)) = 0
        _FresnelWidth ("FresnelWidth", Range(0, 10)) = 0
        _OutlineWidth ("OutlineWidth", Range(0, 2)) = 0
        _OutlineColor ("OutlineColor", Color) = (0,0,0,1)
        [MaterialToggle] _LayerMainTex ("LayerMainTex", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 xboxone ps4 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _OutlineColor;
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlineWidth,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 xboxone ps4 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DotTex; uniform float4 _DotTex_ST;
            uniform float _DotSize;
            uniform float _FresnelWidth;
            uniform fixed _LayerMainTex;
            uniform float _DotRotation;
            uniform sampler2D _ColorTex; uniform float4 _ColorTex_ST;
            uniform sampler2D _DarkColorTex; uniform float4 _DarkColorTex_ST;
            uniform sampler2D _DotColorTex; uniform float4 _DotColorTex_ST;
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
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _ColorTex_var = tex2D(_ColorTex,TRANSFORM_TEX(i.uv0, _ColorTex));
                float4 _DotColorTex_var = tex2D(_DotColorTex,TRANSFORM_TEX(i.uv0, _DotColorTex));
                float4 _DarkColorTex_var = tex2D(_DarkColorTex,TRANSFORM_TEX(i.uv0, _DarkColorTex));
                float node_7432_ang = ((3.141592654/180.0)*_DotRotation);
                float node_7432_spd = 1.0;
                float node_7432_cos = cos(node_7432_spd*node_7432_ang);
                float node_7432_sin = sin(node_7432_spd*node_7432_ang);
                float2 node_7432_piv = float2(0.5,0.5);
                float2 node_7432 = (mul(((float2((sceneUVs * 2 - 1).r,((sceneUVs * 2 - 1).g*(_ScreenParams.g/_ScreenParams.r)))*(distance(objPos.rgb,_WorldSpaceCameraPos)/_DotSize))*0.5+0.5)-node_7432_piv,float2x2( node_7432_cos, -node_7432_sin, node_7432_sin, node_7432_cos))+node_7432_piv); // Rotate dots based on view
                float4 _DotTex_var = tex2D(_DotTex,TRANSFORM_TEX(node_7432, _DotTex));
                float node_3954 = 2.0;
                float node_9485 = 2.0;
                float3 node_6623 = lerp(lerp(_ColorTex_var.rgb,lerp(_DotColorTex_var.rgb,_DarkColorTex_var.rgb,_DotTex_var.rgb),(1.0 - floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),(1.0 / _FresnelWidth)) * node_3954) / (node_3954 - 1))),_ColorTex_var.rgb,floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation) * node_9485) / (node_9485 - 1)); // Combine colors based on lighting
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex)); // An optional overlay texture e.g. for eyes
                float3 finalColor = lerp( node_6623, lerp(_MainTex_var.rgb,node_6623,(1.0 - _MainTex_var.a)), _LayerMainTex );
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 xboxone ps4 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DotTex; uniform float4 _DotTex_ST;
            uniform float _DotSize;
            uniform float _FresnelWidth;
            uniform fixed _LayerMainTex;
            uniform float _DotRotation;
            uniform sampler2D _ColorTex; uniform float4 _ColorTex_ST;
            uniform sampler2D _DarkColorTex; uniform float4 _DarkColorTex_ST;
            uniform sampler2D _DotColorTex; uniform float4 _DotColorTex_ST;
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
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _ColorTex_var = tex2D(_ColorTex,TRANSFORM_TEX(i.uv0, _ColorTex));
                float4 _DotColorTex_var = tex2D(_DotColorTex,TRANSFORM_TEX(i.uv0, _DotColorTex));
                float4 _DarkColorTex_var = tex2D(_DarkColorTex,TRANSFORM_TEX(i.uv0, _DarkColorTex));
                float node_7432_ang = ((3.141592654/180.0)*_DotRotation);
                float node_7432_spd = 1.0;
                float node_7432_cos = cos(node_7432_spd*node_7432_ang);
                float node_7432_sin = sin(node_7432_spd*node_7432_ang);
                float2 node_7432_piv = float2(0.5,0.5);
                float2 node_7432 = (mul(((float2((sceneUVs * 2 - 1).r,((sceneUVs * 2 - 1).g*(_ScreenParams.g/_ScreenParams.r)))*(distance(objPos.rgb,_WorldSpaceCameraPos)/_DotSize))*0.5+0.5)-node_7432_piv,float2x2( node_7432_cos, -node_7432_sin, node_7432_sin, node_7432_cos))+node_7432_piv); // Rotate dots based on view
                float4 _DotTex_var = tex2D(_DotTex,TRANSFORM_TEX(node_7432, _DotTex));
                float node_3954 = 2.0;
                float node_9485 = 2.0;
                float3 node_6623 = lerp(lerp(_ColorTex_var.rgb,lerp(_DotColorTex_var.rgb,_DarkColorTex_var.rgb,_DotTex_var.rgb),(1.0 - floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),(1.0 / _FresnelWidth)) * node_3954) / (node_3954 - 1))),_ColorTex_var.rgb,floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation) * node_9485) / (node_9485 - 1)); // Combine colors based on lighting
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex)); // An optional overlay texture e.g. for eyes
                float3 finalColor = lerp( node_6623, lerp(_MainTex_var.rgb,node_6623,(1.0 - _MainTex_var.a)), _LayerMainTex );
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
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 xboxone ps4 n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}