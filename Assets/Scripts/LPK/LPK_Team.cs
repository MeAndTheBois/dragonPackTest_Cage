/***************************************************
File:           LPK_Team.cs
Authors:        Christopher Onorati
Last Updated:   3/1/2019
Last Version:   2018.3.4

Description:
  This component can be added to any object to denote 
  what team it belongs to. Objects on different teams
  are considered Enemies and objects belonging to the 
  same team are considered Allies. Custom Enemy and 
  Ally custom collision events will be sent.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_Team
* DESCRIPTION : This component can be added to any object to enable team based collision.
**/
public class LPK_Team : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Which team does this object belong to.")]
    [Rename("Team Number")]
    public int m_iTeam = 3;

    [Header("Event Sending Info")]

    [Tooltip("Receiver Game Objects for Ally and Enemy collisions.")]
    public LPK_EventReceivers m_TeamCollisionReceivers;

    /************************************************************************************/
    Collider m_cCollider;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Initializes components.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        m_cCollider = GetComponent<Collider>();

        if(m_cCollider == null)
        {
            if (m_bPrintDebug)
                LPK_PrintWarning(this, "No collider on Game Object!");
        }
    }

    /**
    * FUNCTION NAME: LPK_OnCollisionEnter2D
    * DESCRIPTION  : Manages custom collision reaction to objects on teams.
    * INPUTS       : col - Collision data.
    * OUTPUTS      : None
    **/
    override protected void LPK_OnCollisionEnter2D(Collision2D col)
    {
        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Collision Occurred");

        //If the otherObject doesnt have a team, do nothing
        if (col.gameObject.GetComponent<LPK_Team>() == null)
            return;

        RespondToCollision(col.gameObject.GetComponent<LPK_Team>().m_iTeam);
    }

    /**
    * FUNCTION NAME: LPK_OnTriggerEnter2D
    * DESCRIPTION  : Manages custom collision reaction to objects on teams.
    * INPUTS       : col - Collision data.
    * OUTPUTS      : None
    **/
    override protected void LPK_OnTriggerEnter2D(Collider2D col)
    {
        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Collision Occurred");

        //If the otherObject doesnt have a team, do nothing
        if (col.gameObject.GetComponent<LPK_Team>() == null)
            return;

        RespondToCollision(col.gameObject.GetComponent<LPK_Team>().m_iTeam);
    }

    void RespondToCollision(int otherTeamID)
    {
        //If the otherObject belongs to the same team, send LPK_AllyCollision
        if (otherTeamID == m_iTeam)
        {
            LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(gameObject, m_TeamCollisionReceivers);

            LPK_EventList sendEvent = new LPK_EventList();
            sendEvent.m_CharacterEventTrigger = new LPK_EventList.LPK_CHARACTER_EVENTS[] { LPK_EventList.LPK_CHARACTER_EVENTS.LPK_AllyCollision };

            LPK_EventManager.InvokeEvent(sendEvent, data);

            if (m_bPrintDebug)
                LPK_PrintDebug(this, "Ally Collision Event Dispatched");
        }

        //If the otherObject belongs to a different team, send LPK_EnemyCollision
        if (otherTeamID != m_iTeam)
        {
            LPK_EventManager.LPK_EventData data = new LPK_EventManager.LPK_EventData(gameObject, m_TeamCollisionReceivers);

            LPK_EventList sendEvent = new LPK_EventList();
            sendEvent.m_CharacterEventTrigger = new LPK_EventList.LPK_CHARACTER_EVENTS[] { LPK_EventList.LPK_CHARACTER_EVENTS.LPK_EnemyCollision };

            LPK_EventManager.InvokeEvent(sendEvent, data);

            if (m_bPrintDebug)
                LPK_PrintDebug(this, "Enemy Collision Event Dispatched");
        }
    }
}
