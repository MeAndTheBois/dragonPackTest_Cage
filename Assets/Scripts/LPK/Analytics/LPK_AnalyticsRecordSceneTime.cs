/***************************************************
File:           LPK_AnalyticsRecordSceneTime.cs
Authors:        Christopher Onorati
Last Updated:   3/1/2019
Last Version:   2018.3.4

Description:
  Records time that a scene was active in seconds.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/**
* CLASS NAME  : LPK_AnalyticsRecordSceneTime
* DESCRIPTION : Analytics script to record length a scene is active.
**/
public class LPK_AnalyticsRecordSceneTime : LPK_AnalyticsBase
{
    /************************************************************************************/

    float m_flStartTime;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Get start time and hook up to scene changing event.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        base.OnStart();

        SceneManager.activeSceneChanged += SceneChanged;
        m_flStartTime = Time.time;
    }

    /**
    * FUNCTION NAME: SceneChanged
    * DESCRIPTION  : Print scene changed message.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void SceneChanged(Scene current, Scene next)
    {
        PrintSceneChangeMessage();
    }

    /**
    * FUNCTION NAME: OnDestroyed
    * DESCRIPTION  : Print scene changed message.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    protected override void OnDestroyed()
    {
        PrintSceneChangeMessage();
    }

    /**
    * FUNCTION NAME: OnApplicationQuit
    * DESCRIPTION  : Print scene changed message.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnApplicationQuit()
    {
        PrintSceneChangeMessage();
    }

    /**
    * FUNCTION NAME: OnApplicationQuit
    * DESCRIPTION  : Print end of session information.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void PrintSceneChangeMessage()
    {
        SceneManager.activeSceneChanged -= SceneChanged;

        float totalTime = Time.time - m_flStartTime;
        PrintLogMessage("Scene " + SceneManager.GetActiveScene().name + " lasted for " + totalTime + " seconds.");
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(LPK_AnalyticsRecordSceneTime))]
public class LPK_AnalyticsRecordSceneTimeEditor : Editor
{
    /**
    * FUNCTION NAME: OnInspectorGUI
    * DESCRIPTION  : Override GUI for inspector.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public override void OnInspectorGUI()
    {
        LPK_AnalyticsRecordSceneTime owner = (LPK_AnalyticsRecordSceneTime)target;

        LPK_AnalyticsRecordSceneTime editorOwner = owner.GetComponent<LPK_AnalyticsRecordSceneTime>();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Script");
        editorOwner = (LPK_AnalyticsRecordSceneTime)EditorGUILayout.ObjectField(editorOwner, typeof(LPK_AnalyticsRecordSceneTime), false);
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