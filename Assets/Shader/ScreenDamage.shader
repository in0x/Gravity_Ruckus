Shader "Screen/Damage" 
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Intensity("Intensity", Range(0, 1)) = 0
		_ScreenWidth("Viewport Width", int) = 0
		_ScreenHeight("Viewport Height", int) = 0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _Intensity;
			uniform int _ScreenWidth;
			uniform int _ScreenHeight;

			float4 frag(v2f_img i) : COLOR
			{
				// Get texel color from render texture
				float4 result = tex2D(_MainTex, i.uv);

				// Calculate strength in x direction
				float x = (i.uv.x * _ScreenWidth) - (_ScreenWidth / 2.0);
				x = x * sign(x);
				float factor = x / (_ScreenWidth / 2.0);

				// Calculate strength in y direction
				float y = (i.uv.y * _ScreenHeight) - (_ScreenHeight / 2.0);
				y = y * sign(y);
				factor += y / (_ScreenHeight / 2.0);
				
				// Adjust red value based on average
				factor /= 2;
				factor *= 1.0 - _Intensity;

				float4 red = { 0.2, 0.0, 0.0, 1.0 };
				result += (red * factor);

				return result;
			}
			ENDCG
		}
	}
}