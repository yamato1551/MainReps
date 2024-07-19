﻿
Shader "Custom/Toon" {
    Properties {
		_Color("Color",Color)=(1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _Outline ("_Outline", Range(0,0.1)) = 0
        _OutlineColor ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Pass {
            Tags { "RenderType"="Opaque" }
            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
            };

            float _Outline;
            float4 _OutlineColor;

            float4 vert(appdata_base v) : SV_POSITION {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 normal = mul((float3x3) UNITY_MATRIX_MV, v.normal);
                normal.x *= UNITY_MATRIX_P[0][0];
                normal.y *= UNITY_MATRIX_P[1][1];
                o.pos.xy += normal.xy * _Outline;
                return o.pos;
            }

            half4 frag(v2f i) : COLOR {
                return _OutlineColor;
            }

            ENDCG
        }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
		  struct Input {
            float2 uv_MainTex;
        };

       
		
		fixed4 _Color;
      
        fixed4 LightingToonRamp (SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            half d = dot(s.Normal, lightDir)*0.5 + 0.5;
            fixed3 ramp = tex2D(_MainTex, fixed2(d, 0.5)).rgb;
            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * ramp;
            c.a = 0;
            return c;
        }

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
			 fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
		}

        ENDCG
    }
    FallBack "Diffuse"
}