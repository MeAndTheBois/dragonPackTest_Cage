/***************************************************
File:           LPK_AnalyticsRecordGameSessionTime.cs
Authors:        Christopher Onorati
Last Updated:   3/1/2019
Last Version:   2018.3.4

Description:
  Records time that a gamesession lasted in seconds.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
* CLASS NAME  : LPK_AnalyticsRecordGameSessionTime
* DESCRIPTION : Analytics script to record length a play session.
**/
public class LPK_AnalyticsRecordGameSessionTime : LPK_AnalyticsBase
{
    /************************************************************************************/

    float m_flStartTime;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Get start time and mark object to not be destroyed on scene loading.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        base.OnStart();

        m_flStartTime = Time.time;
        Object.DontDestroyOnLoad(gameObject);

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Marking " + gameObject.name + " as persistent to accuratly track play session length.  This object will never be destroyed!");
    }

    /**
    * FUNCTION NAME: OnDestroyed
    * DESCRIPTION  : Print end of session information.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    protected override void OnDestroyed()
    {
        PrintGameEndMessage();
    }

    /**
    * FUNCTION NAME: OnApplicationQuit
    * DESCRIPTION  : Print end of session information.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnApplicationQuit()
    {
        PrintGameEndMessage();
    }

    /**
    * FUNCTION NAME: PrintGameEndMessage
    * DESCRIPTION  : Print end of session information.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void PrintGameEndMessage()
    {
        float totalTime = Time.time - m_flStartTime;
        PrintLogMessage("Game lasted for " + totalTime + " seconds.");
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(LPK_AnalyticsRecordGameSessionTime))]
public class LPK_AnalyticsRecordGameSessionTimeEditor : Editor
{
    /**
    * FUNCTION NAME: OnInspectorGUI
    * DESCRIPTION  : Override GUI for inspector.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public override void OnInspectorGUI()
    {
        LPK_AnalyticsRecordGameSessionTime owner = (LPK_AnalyticsRecordGameSessionTime)target;

        LPK_AnalyticsRecordGameSessionTime editorOwner = owner.GetComponent<LPK_AnalyticsRecordGameSessionTime>();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Script");
        editorOwner = (LPK_AnalyticsRecordGameSessionTime)EditorGUILayout.ObjectField(editorOwner, typeof(LPK_AnalyticsRecordGameSessionTime), false);
        GUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Base Properties", EditorStyles.boldLabel);

        owner.m_bPrintDebug = EditorGUILayout.Toggle("Print Debug Info", owner.m_bPrintDebug);

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Base Analytics Properties", EditorStyles.boldLabel);

        owner.m_sFileName = EditorGUILayout.TextField("File Name", owner.m_sFileName);
        owner.m_sDirectoryPath = EditorGUILayout.TextField("Path Name", owner.m_sDirectoryPath);
        owner.m_bPrintFileMessages = EditorGUILayout.Toggle("Print File Messages", owner.m_bPrintFileMessages);
    }
}

#endif