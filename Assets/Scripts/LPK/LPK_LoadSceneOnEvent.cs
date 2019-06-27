/***************************************************
File:           LPK_LoadLevelOnEvent.cs
Authors:        Christopher Onorati
Last Updated:   2/8/2019
Last Version:   2018.3.4

Description:
  This component can be added to any object to cause it to 
  load a given level upon receiving a specific event.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; /* Access to scene datatype and scenemanager */

/**
* CLASS NAME  : LPK_LoadSceneOnEvent
* DESCRIPTION : Loads a new scene on parsing of an event.
**/
public class LPK_LoadSceneOnEvent : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Scene to load when triggered.")]
    [SceneDropdown]
    public string m_LevelToLoad;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up what event to listen to for level/scene switching.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Launches a new level.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        LoadScene();
    }

    /**
    * FUNCTION NAME: LoadScene
    * DESCRIPTION  : Launches a new level.  Seperated from OnEvent so it is exposed
    *                and accessible by Unity UI system (buttons).
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(m_LevelToLoad))
        {
            if (m_bPrintDebug)
                LPK_PrintDebug(this, "Loading new level.");

            SceneManager.LoadScene(m_LevelToLoad);
        }
        else
        {
            if (m_bPrintDebug)
                LPK_PrintDebug(this, "No Level Specified");
        }
    }
}
