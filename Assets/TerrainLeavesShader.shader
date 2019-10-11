Shader "Terrain Leaves Shader" {
    Properties {
        _MainTex ("Base", 2D) = "white" {}
        _NightPower ("Night power", Range(0, 1)) = 1
        _Brightness ("_Brightness", Range(0.8, 1)) = 1
    }

    SubShader {
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite On
 
            CGPROGRAM
                #include "UnityCG.cginc"
                #pragma vertex vert
                #pragma fragment frag

                struct appdata {
                    float4 vertex : POSITION;
                    float4 texcoord : TEXCOORD0;
                    fixed4 texcoord1 : TEXCOORD1;
                    fixed4 color : COLOR;
                };
 
                struct v2f
                {
                    fixed4 color : COLOR;
                    fixed4 pos : SV_POSITION;
                    fixed2 pack0 : TEXCOORD0;
 
                };
                sampler2D _MainTex;
                float _NightPower;
                float _Brightness;
                fixed4 _MainTex_ST;
               
                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);

                    o.color = v.texcoord1;
                    
                    o.color.r = o.color.r + _NightPower;
                    if (o.color.r < o.color.g) {
                        o.color.r = v.color.a - o.color.r / _Brightness;
                    } else {
                        o.color.r = v.color.a - o.color.g / _Brightness;
                    }
                    o.color.g = o.color.r;
                    o.color.b = o.color.r;

                    v.color.a = 1;

                    o.color *= v.color;

                    return o;
                }

                fixed4 frag(v2f i) : COLOR {
                    fixed4 res = tex2D(_MainTex, i.pack0) * i.color;
                    if (res.a < 0.9) {
                        discard;
                    }
                    return res;         
                }
 
            ENDCG
        }
    }
}