/***************************************************
File:           LPK_KillOnEvent.cs
Authors:        Christopher Onorati
Last Updated:   12/1/2018
Last Version:   2.17

Description:
  This component can be added to any object to cause it to 
  dispatch a LPK_Death event to a specified object upon 
  receiving an event.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_KillOnEvent
* DESCRIPTION : Sends a kill event upon receiving another event.
**/
public class LPK_KillOnEvent : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    [Header("Event Sending Info")]

    [Tooltip("Receiver Game Objects for character death.")]
    public LPK_EventReceivers m_DeathEventReceivers;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up what event to listen to for object killing.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Sends an object killed event.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;


        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Event Received");

        //Send out event.
        LPK_EventManager.LPK_EventData sendData = new LPK_EventManager.LPK_EventData(gameObject, m_DeathEventReceivers);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_CharacterEventTrigger = new LPK_EventList.LPK_CHARACTER_EVENTS[] { LPK_EventList.LPK_CHARACTER_EVENTS.LPK_Death };

        LPK_EventManager.InvokeEvent(sendEvent, sendData);
    }
}
