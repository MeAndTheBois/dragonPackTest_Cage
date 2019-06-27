/***************************************************
File:           LPK_Pixelation.shader
Authors:        Christopher Onorati
https://www.youtube.com/watch?v=9bTFVaKGIIQ
Last Updated:   12/10/2018
Last Version:   2.17

Description:
Basic pixelation shader to de-res screen view.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "LPK/LPK_Pixelation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _PixelWidth("Pixel Width", float) = 32.0
        _PixelHeight("Pixel Height", float) = 32.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off
        ZWrite Off
        ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
            float _PixelWidth;
            float _PixelHeight;

			fixed4 frag (v2f i) : SV_Target
			{
                float2 uv = i.uv;

                //Multiply by dimension.
                uv.x *= _PixelWidth;
                uv.y *=_PixelHeight;
                
                //Round to remove fraction values.
                uv.x = round(uv.x);
                uv.y = round(uv.y);

                //Average the pixel.
                uv.x /= _PixelWidth;
                uv.y /= _PixelHeight;

				fixed4 col = tex2D(_MainTex, uv);
				return col;
			}
			ENDCG
		}
	}
}
