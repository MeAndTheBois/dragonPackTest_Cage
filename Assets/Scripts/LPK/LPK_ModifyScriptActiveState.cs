/***************************************************
File:           LPK_ModifyScriptActiveState.cs
Authors:        Christopher Onorati
Last Updated:   12/26/2018
Last Version:   2.17

Description:
  This component can be used to enable/disable the active
  state of other scripts.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_ModifyScriptActiveState
* DESCRIPTION : Component used to enable/disable other components.
**/
public class LPK_ModifyScriptActiveState : LPK_LogicBase
{
    /************************************************************************************/
    [Header("Component Properties")]

    [Tooltip("How to change the active state of the declared script(s).")]
    [Rename("Toggle Type")]
    public LPK_ToggleType m_eToggleType;

    [Tooltip("Script(s) to change the enabled state of.")]
    [Rename("Script")]
    public MonoBehaviour[] m_ModifyScript;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: Start
    * DESCRIPTION  : Sets up what event to listen to for sprite and color modification.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Sets the actibe state of the script(s).
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        for(int i = 0; i < m_ModifyScript.Length; i++)
        {
            if (m_ModifyScript[i] == null)
                continue;

            if (m_eToggleType == LPK_ToggleType.ON)
                m_ModifyScript[i].enabled = true;
            else if (m_eToggleType == LPK_ToggleType.OFF)
                m_ModifyScript[i].enabled = false;
            else if (m_eToggleType == LPK_ToggleType.TOGGLE)
            {
                if (!m_ModifyScript[i].enabled)
                    m_ModifyScript[i].enabled = true;
                else if (m_ModifyScript[i].enabled)
                    m_ModifyScript[i].enabled = false;
            }

            //Debug info.
            if(m_bPrintDebug && m_ModifyScript[i].enabled)
                LPK_PrintDebug(this, "Changing active state of " + m_ModifyScript[i] + " to ON.");
            else if (m_bPrintDebug && !m_ModifyScript[i].enabled)
                LPK_PrintDebug(this, "Changing active state of " + m_ModifyScript[i] + " to OFF.");
        }
    }
}
