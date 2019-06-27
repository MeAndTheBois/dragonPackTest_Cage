/***************************************************
File:           LPK_ImageOverlay.shader
Authors:        Christopher Onorati
Last Updated:   12/9/2018
Last Version:   2.17

Description:
Shader to replace the screen view with a texture.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "LPK/LPK_ImageOverlay"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _OverlayTex("Overlay Texture", 2D) = "white" {}
        _MoveX("Move X Speed", float) = 0
        _MoveY("Move Y Speed", float) = 0
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
            sampler2D _OverlayTex;
            float _MoveX;
            float _MoveY;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_OverlayTex, float2(i.uv.x + _Time.x * _MoveX, i.uv.y + _Time.x * _MoveY));
				return col;
			}
			ENDCG
		}
	}
}
