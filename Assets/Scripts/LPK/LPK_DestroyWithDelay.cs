/***************************************************
File:           LPK_DestroyWithDelay.cs
Authors:        Christopher Onorati
Last Updated:   12/8/2018
Last Version:   2.17

Description:
  This component can be added to an object to cause it
  to be destroyed with a delay.  Useful for projectiles.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_DestroyWithDelay
* DESCRIPTION : Destroys the object this component belongs to with a delay.
**/
public class LPK_DestroyWithDelay : LPK_LogicBase
{
    /************************************************************************************/

    [Tooltip("How long to wait after object is spawned to destroy it (in seconds)")]
    [Rename("Destroy Delay")]
    public float m_flDestroyDelay = 2.0f;

    [Header("Event Sending Info")]

    [Tooltip("Receiver Game Objects for object destruction.")]
    public LPK_EventReceivers ObjectDeletedReceivers;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Initializes the delay for object destruction.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart ()
    {
        StartCoroutine(DelayTimer());
    }

    /**
    * FUNCTION NAME: DelayTimer
    * DESCRIPTION  : Forces initial delay before object destruction.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(m_flDestroyDelay);
        DestroyOwner();
        Object.Destroy(gameObject);
    }

    /**
    * FUNCTION NAME: DestroyOwner
    * DESCRIPTION  : Destroy this object's owner and send out the event.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void DestroyOwner()
    {
        LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(gameObject, ObjectDeletedReceivers);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_GameplayEventTrigger = new LPK_EventList.LPK_GAMEPLAY_EVENTS[] { LPK_EventList.LPK_GAMEPLAY_EVENTS.LPK_GameObjectDestroy };

        LPK_EventManager.InvokeEvent(sendEvent, data);

        Object.Destroy(gameObject);

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Object Destroyed");
    }
}
