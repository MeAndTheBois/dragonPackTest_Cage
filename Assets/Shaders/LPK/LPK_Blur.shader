/***************************************************
File:           LPK_Blur.shader
Authors:        Christopher Onorati
Last Updated:   12/9/2018
Last Version:   2.17

Description:
Basic blur shader for use on a main camera.  Performs a
box blur.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "LPK/LPK_Blur"
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
            float4 _MainTex_TexelSize;

            /**
            * FUNCTION NAME: BoxBlur
            * DESCRIPTION  : Blurs a pixel with the 9 around it.
            * INPUTS       : tex  - Main rendered view.
                             uv   - UV coordinates to blur.
                             size - How intense the blur should be.
            * OUTPUTS      : None
            **/
            float4 BoxBlur(sampler2D tex, float2 uv, float4 size)
            {
                float4 col = tex2D(tex, uv + float2(-size.x, size.y)) + tex2D(tex, uv + float2(0, size.y)) + tex2D(tex, uv + float2(size.x, size.y)) +
                             tex2D(tex, uv + float2(-size.x, 0)) + tex2D(tex, uv + float2(0, 0)) + tex2D(tex, uv + float2(size.x, 0)) +
                             tex2D(tex, uv + float2(-size.x, -size.y)) + tex2D(tex, uv + float2(0, -size.y)) + tex2D(tex, uv + float2(size.x, -size.y));

                //Blurred color.
                return col / 9;
            }

			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = BoxBlur(_MainTex, i.uv, _MainTex_TexelSize);
				return col;
			}
			ENDCG
		}
	}
}
