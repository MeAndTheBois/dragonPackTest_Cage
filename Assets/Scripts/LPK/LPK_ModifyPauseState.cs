/***************************************************
File:           LPK_ModifyPauseState.cs
Authors:        Christopher Onorati
Last Updated:   12/17/2018
Last Version:   2.17

Description:
  This component manages the pause state of a scene.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_ModifyPauseState
* DESCRIPTION : Component to manage the pause state of the scene.
**/
public class LPK_ModifyPauseState : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("What is the rate of gameplay when puased.  0 is competly standstill.  0.5 of some other value can be used if you want the pause screen to slow gameplay down but not freeze it entirely.")]
    [Rename("Pause Rate")]
    [Range(0.0f, 1.0f)]
    public float m_flPauseRate = 0.0f;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_PauseEventTrigger = new LPK_EventList();

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_UnapuseEventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Initializes pausing functions.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_PauseEventTrigger, PauseOnEvent);
        InitializeEvent(m_UnapuseEventTrigger, UnpauseOnEvent);
    }

    /**
    * FUNCTION NAME: PauseOnEvent
    * DESCRIPTION  : Calls the pause function if coniditons are met.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    void PauseOnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        StartCoroutine(FrameDelay());
    }

    /**
    * FUNCTION NAME: UnpauseOnEvent
    * DESCRIPTION  : Calls the unpause function if coniditons are met.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    void UnpauseOnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        Unpause();
    }

     /**
     * FUNCTION NAME: FrameDelay
     * DESCRIPTION  : Delays pause by 1 frame so objects can settle and spawn (like the pause menu).
     * INPUTS       : None
     * OUTPUTS      : None
     **/
    IEnumerator FrameDelay()
    {
        //Assume 60 FPS
        yield return true;
        Pause();
    }

    /**
    * FUNCTION NAME: Pause
    * DESCRIPTION  : Pauses the scene.  Set to public so UI buttons can interact with this.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public void Pause()
    {
        LPK_PauseManager.Pause(m_flPauseRate);

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Pausing scene.");

    }

    /**
    * FUNCTION NAME: Unpause
    * DESCRIPTION  : Unpauses the scene.  Set to public so UI buttons can interact with this.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public void Unpause()
    {
        LPK_PauseManager.Unpause();

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Unpausing scene.");
    }

    /**
    * FUNCTION NAME: OnDestroyed
    * DESCRIPTION  : Detach pause detection functions as they are not named OnEvent (since there are two).
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnDestroyed()
    {
        DetachFunction(PauseOnEvent);
        DetachFunction(UnpauseOnEvent);
    }
}

/**
* CLASS NAME  : LPK_PauseManager
* DESCRIPTION : Global manager to adjust pause state of gameplay.
**/
public static class LPK_PauseManager
{
    /**
    * FUNCTION NAME: Pause
    * DESCRIPTION  : Pauses the scene and the actions of any LPK component.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public static void Pause(float newTimeScale)
    {
        Time.timeScale = newTimeScale;

        LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(null, null);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_PauseEventTrigger = new LPK_EventList.LPK_PAUSE_EVENTS[] { LPK_EventList.LPK_PAUSE_EVENTS.LPK_GamePaused };

        LPK_EventManager.InvokeEvent(sendEvent, data);
    }

    /**
    * FUNCTION NAME: Unpause
    * DESCRIPTION  : Resumes the scene and the actions of any LPK component.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public static void Unpause()
    {
        Time.timeScale = 1.0f;

        LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(null, null);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_PauseEventTrigger = new LPK_EventList.LPK_PAUSE_EVENTS[] { LPK_EventList.LPK_PAUSE_EVENTS.LPK_GameUnpaused };

        LPK_EventManager.InvokeEvent(sendEvent, data);
    }
}
