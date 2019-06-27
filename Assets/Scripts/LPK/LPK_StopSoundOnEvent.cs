/***************************************************
File:           LPK_StopSoundOnEvent
Authors:        Christopher Onorati
Last Updated:   2/9/2019
Last Version:   2018.3.4

Description:
  This component can be added to any object to stop a 
  sound upon receving an event notice.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_StopSoundOnEvent
* DESCRIPTION : Stops a sound when an event is parsed.
**/
public class LPK_StopSoundOnEvent : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Object(s) whose emitter should be stopped. Set to owner by default if both this and the tagged objects array are left empty.")]
    public GameObject[] m_TargetObjects;

    [Tooltip("Tagged object(s) whose emitter will be used to play sound. Set to owner by default if both this and the game object array are left empty.")]
    public string[] m_TargetTags;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up what event to listen to for sound stopping.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);

        if (m_TargetObjects.Length == 0 && m_TargetTags.Length == 0)
        {
            if (GetComponent<AudioSource>() != null)
                m_TargetObjects = new GameObject[] { gameObject };
        }
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Plays a sound effect.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Event received.");

        for (int i = 0; i < m_TargetObjects.Length; i++)
        {
            if (m_TargetObjects[i].GetComponent<AudioSource>() != null)
                m_TargetObjects[i].GetComponent<AudioSource>().Stop();
        }

        for (int i = 0; i < m_TargetTags.Length; i++)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(m_TargetTags[i]);

            for (int j = 0; j < taggedObjects.Length; j++)
            {
                if (taggedObjects[j].GetComponent<AudioSource>() != null)
                    taggedObjects[j].GetComponent<AudioSource>().Stop();
            }
        }
    }
}
