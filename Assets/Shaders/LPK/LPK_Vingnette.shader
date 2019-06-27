/***************************************************
File:           LPK_Vingnette.shader
Authors:        Christopher Onorati
Last Updated:   12/9/2018
Last Version:   2.17

Description:
Shader to create a basic vingnette effect.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "LPK/LPK_Vingnette"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _CenterOfScreen("Vingette Center", float) = 0.5
        _Intensity("Intensity", float) = 16.0
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
            float _CenterOfScreen;  //Center of the screen.
            float _Intensity;       //Intensity of the vingette.

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.uv);

                //Set 0 to the middle instead of lower left corner of the screen.
                float2 uv = i.uv - 0.5;

                col *= _CenterOfScreen + _CenterOfScreen * _Intensity * i.uv.x * i.uv.y * (1.0 - i.uv.x) * (1.0 - i.uv.y);

                return col;
			}
			ENDCG
		}
	}
}
