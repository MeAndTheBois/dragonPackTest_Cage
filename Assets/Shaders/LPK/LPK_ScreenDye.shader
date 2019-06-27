/***************************************************
File:           LPK_ScreenDye.shader
Authors:        Christopher Onorati
                https://www.youtube.com/watch?v=kpBnIAPtsj8
Last Updated:   12/8/2018
Last Version:   2.17

Description:
Shader to dye the screen a certain color.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "VPK/VPK_ScreenDye"
{
	Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
        _RenderColor ("Render Color", Vector) = (1, 1, 1, 1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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
			
            sampler2D _MainTex;  //Camera view.
			Vector _RenderColor; //Multiplier to color.

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _RenderColor;
				return col;
			}
			ENDCG
		}
	}
}
