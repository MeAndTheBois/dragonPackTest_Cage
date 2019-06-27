/***************************************************
File:           LPK_DispatchOnLineOfSight.cs
Authors:        Christopher Onorati
Last Updated:   1/29/2019
Last Version:   2.17

Description:
  This component checks the LOS between two gameobjects.
  Note both gameobjects need to have colliders to be detected.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_DispatchOnLineOfSight
* DESCRIPTION : Check the line of sight between two objects with a raycast.
**/
public class LPK_DispatchOnLineOfSight : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Whether this field should start active or not.")]
    [Rename("Active")]
    public bool m_bActive = true;

    [Tooltip("How to change active state when events are received.")]
    [Rename("Toggle Type")]
    public LPK_ToggleType m_eToggleType;

    [Tooltip("Object that is trying to look for others.  Defaults to self if left unset.  This is useful for prefab setup.")]
    [Rename("Source Object")]
    public GameObject m_pSource;

    [Tooltip("Objects the source is trying to find.")]
    public GameObject[] m_Targets;

    [Tooltip("How far to look for an object.  By default, look forever.")]
    [Rename("Search Distance.")]
    public float m_flDistance = Mathf.Infinity;

    [Tooltip("Layer mask to use for filtering.  Leave empty to collider with everything.")]
    [Rename("Layer Mask")]
    public LayerMask m_layerMask;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    [Header("Event Sending Info")]

    [Tooltip("Receiver Game Objects for when line of sight is established.")]
    public LPK_EventReceivers m_LineOfSightEstablishedReceivers;

    [Tooltip("Receiver Game Objects for when line of sight is maintained.")]
    public LPK_EventReceivers m_LineOfSightMaintainedReceivers;

    [Tooltip("Receiver Game Objects for when line of sight is lost.")]
    public LPK_EventReceivers m_LineOfSightLostReceivers;

    /************************************************************************************/

    //Stores currently found game objects.
    List<GameObject> m_pFoundObjects = new List<GameObject>();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up event detection for toggling active state.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);

        if (m_pSource == null)
            m_pSource = gameObject;
    }

    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Sends out raycasts to the objects we are trying to establish a line of sight for.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        if (!m_bActive)
            return;

        for (int i = 0; i < m_Targets.Length; i++)
            CheckLOS(m_pSource, m_Targets[i]);
    }

    /**
    * FUNCTION NAME: CheckLOS
    * DESCRIPTION  : Tests LOS for each object we are tracking.
    * INPUTS       : source - The object doing the looking.
    *                target - The object the source is looking for.
    * OUTPUTS      : None
    **/
    void CheckLOS(GameObject source, GameObject target)
    {
        bool bCanSee = false;
        
        //TODO: It would be good to store the source transform position so we dont have to grab it so many times per frame...
        Vector3 dir = target.transform.position - source.transform.position;
        dir.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(source.transform.position, dir, m_flDistance, m_layerMask);

        if (!hit.collider)
            return;

        //Epsilon of 0.05f
        if (hit.collider.gameObject == target)
            bCanSee = true;

        //Target has been found
        if (bCanSee && !m_pFoundObjects.Find(obj => obj.name == target.name))
            DispatchFoundEvent(target);

        else if (bCanSee && m_pFoundObjects.Find(obj => obj.name == target.name))
            DispatchMaintainEvent(target);

        //Target has been lost.
        else if (!bCanSee && m_pFoundObjects.Find(obj => obj.name == target.name))
            DispatchLostEvent(target);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Sets the active state of the line of sight checking.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        if (m_eToggleType == LPK_ToggleType.ON)
            m_bActive = true;
        else if (m_eToggleType == LPK_ToggleType.OFF)
            m_bActive = false;
        else if (m_eToggleType == LPK_ToggleType.TOGGLE)
            m_bActive = !m_bActive;
    }

    /**
    * FUNCTION NAME: DispatchFoundEvent
    * DESCRIPTION  : Dispatches the established LOS event.
    * INPUTS       : target - Game object to add to the found objects list.
    * OUTPUTS      : None
    **/
    void DispatchFoundEvent(GameObject target)
    {
        //This object has been found.
        m_pFoundObjects.Add(target);

        LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(gameObject, m_LineOfSightEstablishedReceivers);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_AIEventTrigger = new LPK_EventList.LPK_AI_EVENTS[] { LPK_EventList.LPK_AI_EVENTS.LPK_LineOfSightEstablished };

        LPK_EventManager.InvokeEvent(sendEvent, data);

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Line of sight established between " + m_pSource.name + " and " + target);
    }

    /**
    * FUNCTION NAME: DispatchMaintainEvent
    * DESCRIPTION  : Dispatches the maintained LOS event.
    * INPUTS       : target - Object that was seen.  Only used for debug logging here.
    * OUTPUTS      : None
    **/
    void DispatchMaintainEvent(GameObject target)
    {
        LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(gameObject, m_LineOfSightMaintainedReceivers);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_AIEventTrigger = new LPK_EventList.LPK_AI_EVENTS[] { LPK_EventList.LPK_AI_EVENTS.LPK_LineOfSightMaintained };

        LPK_EventManager.InvokeEvent(sendEvent, data);

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Line of sight maintained between " + m_pSource.name + " and " + target);
    }

    /**
    * FUNCTION NAME: DispatchFoundEvent
    * DESCRIPTION  : Dispatches the lost LOS event.
    * INPUTS       : target - Game object to remove from the found objects list.
    * OUTPUTS      : None
    **/
    void DispatchLostEvent(GameObject target)
    {
        //This object has been lost.
        m_pFoundObjects.Remove(target);

        LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(gameObject, m_LineOfSightLostReceivers);

        LPK_EventList sendEvent = new LPK_EventList();
        sendEvent.m_AIEventTrigger = new LPK_EventList.LPK_AI_EVENTS[] { LPK_EventList.LPK_AI_EVENTS.LPK_LineOfSightLost };

        LPK_EventManager.InvokeEvent(sendEvent, data);

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Line of sight lost between " + m_pSource.name + " and " + target);
    }
}
