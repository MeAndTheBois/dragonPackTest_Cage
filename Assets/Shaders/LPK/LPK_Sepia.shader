/***************************************************
File:           LPK_Sepia.shader
Authors:        Christopher Onorati
Last Updated:   12/9/2018
Last Version:   2.17

Description:
Shader to create a sepia-toned scene.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "LPK/LPK_Sepia"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
                float3 lum = float3(0.299, 0.587, 0.114);
                float bwCol = dot(col.rgb, lum);

                col = float4(float3(bwCol * float3(1.0, 0.7, 0.4)), col.a);
				return col;
			}
			ENDCG
		}
	}
}
