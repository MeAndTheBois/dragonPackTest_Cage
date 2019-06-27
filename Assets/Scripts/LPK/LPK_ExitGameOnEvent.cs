/***************************************************
File:           LPK_ExitGameOnEvent.cs
Authors:        Christoper Onorati
Last Updated:   12/1/2018
Last Version:   2.17

Description:
  This component can be added to any object to cause it
  to exit the application upon receiving an event.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_ExitGameOnEvent
* DESCRIPTION : Closes the game on parsing user-specified event.
**/
public class LPK_ExitGameOnEvent : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up what event to listen to for game ending.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Exists out of the game.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        Quit();
    }

    /**
    * FUNCTION NAME: Quit
    * DESCRIPTION  : Calls quit command.  Seperate function so this is exposed to the unity
    *                UI system (buttons).
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public void Quit()
    {
        Application.Quit();
    }
}
