/***************************************************
File:           LPK_AnalyticsPrintMessageOnEvent.cs
Authors:        Christopher Onorati
Last Updated:   3/1/2019
Last Version:   2018.3.4

Description:
  Prints messages to an analytics file when events are
  receieved.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_AnalyticsPrintMessageOnEvent
* DESCRIPTION : Analytics script to print messages to a file when events are received.
**/
public class LPK_AnalyticsPrintMessageOnEvent : LPK_AnalyticsBase
{
    [Header("Component Properties")]

    [Tooltip("Message to print to the log file.")]
    [Rename("Message")]
    public string m_sMessage;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Initializes event detection.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        base.OnStart();

        InitializeEvent(m_EventTrigger, OnEvent);
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Logs messages to a file when user specified events occur.
    * INPUTS       : data - Data to parse for event validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        if (!ShouldRespondToEvent(data))
            return;

        PrintLogMessage(m_sMessage);
    }
}
