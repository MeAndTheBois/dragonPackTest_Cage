/***************************************************
File:           LPK_AdditiveSprite.shader
Authors:        Christopher Onorati
Last Updated:   1/8/2019
Last Version:   2.17

Description:
Shader that can be added to sprites to make additive blending.
Can work with a black background sprite, or a sprite with a
transparent background.

This script is a basic and generic implementation of its
functionality. It is designed for educational purposes and
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

Shader "Vertex Color Lit" 
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
    }

    SubShader
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

        // inside Pass
        Blend SrcAlpha One

        Pass
        {
            SetTexture[_MainTex]
            {
                combine texture * previous
            }
        }
    }
}