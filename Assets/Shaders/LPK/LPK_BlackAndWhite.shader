/***************************************************
File:           LPK_BlackAndWhite.shader
Authors:        Christopher Onorati
Last Updated:   12/9/2018
Last Version:   2.17

Description:
Shader to create a black and white scene.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "LPK/LPK_BlackAndWhite"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _DivideCount("Saturation", float) = 3.0
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
            float _DivideCount;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 bwColor = float4 ( col.r + col.g + col.b / _DivideCount,
                                          col.r + col.g + col.b / _DivideCount,
                                          col.r + col.g + col.b / _DivideCount, col.a );

				return bwColor;
			}
			ENDCG
		}
	}
}
