Shader "Custom/Black" {
	Properties{
	}

		SubShader{

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appData {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};


			v2f vert(appData v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				return fixed4(0,0,0,1);
			}
			ENDCG
		}

	}
	FallBack "Diffuse"
}
