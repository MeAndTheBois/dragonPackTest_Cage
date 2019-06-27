/***************************************************
File:           LPK_FeedbackManager.cs
Authors:        Christopher Onorati
Last Updated:   2/28/2019
Last Version:   2018.3.4

Description:
  This component can be used to create feedback loops for
  game events.  This component should be duplicated as needed,
  with student code (such as creating objects, modify colors, etc.)
  going in the lines commented below.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_FeedbackManager
* DESCRIPTION : Component used to track feedback loops on game events.
**/
[RequireComponent(typeof(TextMesh))]
public class LPK_FeedbackManager : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Set to start the feedback loop instantly when this component is activated/spawned.")]
    [Rename("Start Feedback Instantly")]
    public bool m_bStartFeedbackInstantly = true;

    [Tooltip("Duration for the Signal portion of the feedback loop.")]
    [Rename("Signal Duration")]
    public float m_flSignalTime;

    [Tooltip("Duration for the Update portion of the feedback loop.")]
    [Rename("Update Duration")]
    public float m_flUpdateTime;

    [Tooltip("Duration for the Resolve portion of the feedback loop.")]
    [Rename("Resolve Duration")]
    public float m_flResolveTime;

    [Tooltip("Set to display the debug text on the object.  This is different than the print debug info flag.")]
    [Rename("Display Debug Text")]
    public bool m_bDisplayDebugText = true;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component's action")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /************************************************************************************/

    TextMesh m_cTextMesh;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up the text mesh and the feedback process.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        m_cTextMesh = GetComponent<TextMesh>();

        InitializeEvent(m_EventTrigger, OnEvent);

        if (m_bStartFeedbackInstantly)
            StartSignal();
    }

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Activation of the feedback loop.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Early out.
        if (!ShouldRespondToEvent(data))
            return;

        StartSignal();

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Feedback loop started via event trigger.");
    }

    /**
    * FUNCTION NAME: StartSignal
    * DESCRIPTION  : Starts the signal portion of the feedback loop.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void StartSignal()
    {
        //NOTENOTE:  Could add a custom event here to allow students to hook into, but for educational purposes it may be more valuable
        //           for the student to write the calls themselves.  This also makes it easier for the instructot to track what is going on at each
        //           stage in the feedback loop.

        //STUDENT:  ADD SIGNAL FEEDBACK HERE (VISUAL, SFX, PARITCLES, ETC)

        if (m_bDisplayDebugText)
            m_cTextMesh.text = "S";

       StartCoroutine(DelayUpdate());
    }

    /**
    * FUNCTION NAME: StartUpdate
    * DESCRIPTION  : Starts the update portion of the feedback loop.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void StartUpdate()
    {
        //NOTENOTE:  Could add a custom event here to allow students to hook into, but for educational purposes it may be more valuable
        //           for the student to write the calls themselves.  This also makes it easier for the instructot to track what is going on at each
        //           stage in the feedback loop.

        //STUDENT:  ADD UPDATE FEEDBACK HERE (VISUAL, SFX, PARITCLES, ETC)

        if (m_bDisplayDebugText)
            m_cTextMesh.text = "U";

        StartCoroutine(DelayResolve());
    }

    /**
     * FUNCTION NAME: StartResolve
     * DESCRIPTION  : Starts the resolve portion of the feedback loop.
     * INPUTS       : None
     * OUTPUTS      : None
     **/
    void StartResolve()
    {
        //NOTENOTE:  Could add a custom event here to allow students to hook into, but for educational purposes it may be more valuable
        //           for the student to write the calls themselves.  This also makes it easier for the instructot to track what is going on at each
        //           stage in the feedback loop.

        //STUDENT:  ADD RESOLVE FEEDBACK HERE (VISUAL, SFX, PARITCLES, ETC)

        if (m_bDisplayDebugText)
            m_cTextMesh.text = "R";

        StartCoroutine(StopResolve());
    }

    /**
    * FUNCTION NAME: EndFeedbackLoop
    * DESCRIPTION  : Ends the feedback loop.  Any last calls can be done here.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void EndFeedbackLoop()
    {
        //NOTENOTE:  Could add a custom event here to allow students to hook into, but for educational purposes it may be more valuable
        //           for the student to write the calls themselves.  This also makes it easier for the instructot to track what is going on at each
        //           stage in the feedback loop.

        //STUDENT:  CLEAN UP ANY DANGLING FEEDBACK HERE.

        if (m_bDisplayDebugText)
            m_cTextMesh.text = "";
    }

    /**
    * FUNCTION NAME: DelayUpdate
    * DESCRIPTION  : Forces delay before starting the Update segment of the feedback loop.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    IEnumerator DelayUpdate()
    {
        yield return new WaitForSeconds(m_flSignalTime);
        StartUpdate();
    }

    /**
    * FUNCTION NAME: DelayUpdate
    * DESCRIPTION  : Forces delay before starting the Resolve segment of the feedback loop.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    IEnumerator DelayResolve()
    {
        yield return new WaitForSeconds(m_flUpdateTime);
        StartResolve();
    }

    /**
    * FUNCTION NAME: StopResolve
    * DESCRIPTION  : Forces delay before stopping the Resolve segment of the feedback loop.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    IEnumerator StopResolve()
    {
        yield return new WaitForSeconds(m_flResolveTime);
        EndFeedbackLoop();
    }
}
