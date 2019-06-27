/***************************************************
File:           LPK_CameraShaderEnabler.cs
Authors:        Christopher Onorati
Last Updated:   12/9/2018
Last Version:   2.17

Description:
  Allows the user to apply a effect on a camera renderer.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_CameraShaderEnabler
* DESCRIPTION : Component to enable post processing effects from
*               a single camera.
**/
[RequireComponent(typeof(Camera))]
public class LPK_CameraShaderEnabler : LPK_LogicBase
{
    /************************************************************************************/

    public enum ToggleType
    {
        ON,
        OFF,
        TOGGLE,
    };

    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Start the camera with this shader effect active.")]
    [Rename("Start Active")]
    public bool m_bActive = true;

    [Tooltip("How this shader manager handles detecting input.")]
    [Rename("Toggle Type")]
    public ToggleType m_eToggleType = ToggleType.ON;

    [Tooltip("Shader to apply to the camera.  For multiple effects just add more of this component.")]
    [Rename("Shader Material")]
    public Material m_ShaderMat;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up what event to listen to for shader toggling.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);
    }

    /**
    * FUNCTION NAME: OnRenderImage
    * DESCRIPTION  : Sets up which shaders to apply to a rendering camera.
    * INPUTS       : src - Source image to modify before rendering.
    *                dst - Destination of the render (usually the screen).
    * OUTPUTS      : None
    **/
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        //Apply shader desired.
        if (m_bActive && m_ShaderMat != null)
            Graphics.Blit(src, dst, m_ShaderMat);
        else
            Graphics.Blit(src, dst);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Manages active state of effect.
    * INPUTS       : data - Event data to parse to determine triggering.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Shader effect enabled.");

        if (m_eToggleType == ToggleType.ON)
            m_bActive = true;
        else if (m_eToggleType == ToggleType.OFF)
            m_bActive = false;
        else if (m_eToggleType == ToggleType.TOGGLE)
            m_bActive = !m_bActive;

    }
}
