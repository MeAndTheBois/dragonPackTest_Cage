/***************************************************
File:           LPK_AnalyticsRecordKeyboardInput.cs
Authors:        Christopher Onorati
Last Updated:   3/1/2019
Last Version:   2018.3.4

Description:
  Records keyboard input to an analytics file when input
  is receieved.

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
* CLASS NAME  : LPK_AnalyticsRecordKeyboardInput
* DESCRIPTION : Analytics script to record keyboard input.
**/
public class LPK_AnalyticsRecordKeyboardInput : LPK_AnalyticsBase
{
    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Record user input.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        //Check input
        if (Input.anyKeyDown)
            PrintLogMessage(Input.inputString);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(LPK_AnalyticsRecordKeyboardInput))]
public class LPK_AnalyticsRecordKeyboardInputEditor : Editor
{
    /**
    * FUNCTION NAME: OnInspectorGUI
    * DESCRIPTION  : Override GUI for inspector.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public override void OnInspectorGUI()
    {
        LPK_AnalyticsRecordKeyboardInput owner = (LPK_AnalyticsRecordKeyboardInput)target;

        LPK_AnalyticsRecordKeyboardInput editorOwner = owner.GetComponent<LPK_AnalyticsRecordKeyboardInput>();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Script");
        editorOwner = (LPK_AnalyticsRecordKeyboardInput)EditorGUILayout.ObjectField(editorOwner, typeof(LPK_AnalyticsRecordKeyboardInput), false);
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