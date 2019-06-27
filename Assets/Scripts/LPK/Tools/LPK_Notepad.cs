/***************************************************
File:           LPK_Notepad.cs
Authors:        Christopher Onorati (https://docs.unity3d.com/ScriptReference/EditorPrefs.SetString.html)
Last Updated:   3/18/2019
Last Version:   2018.3.4

Description:
  Create a notepad to remember current tasks being
  worked on locally for a project.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

/**
* CLASS NAME  : LPK_Notepad
* DESCRIPTION : Internal editor notepad.
**/
public class LPK_Notepad : EditorWindow
{
    string note = "Notes:\n->\n->";

    /**
    * FUNCTION NAME: InitializeNotepad
    * DESCRIPTION  : Create an instance of the notepad.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    [MenuItem("Utilities/Notepad %n")]
    static void InitializeNotepad()
    {
        LPK_Notepad window = (LPK_Notepad)EditorWindow.GetWindow(typeof(LPK_Notepad));
        window.Show();
    }

    /**
    * FUNCTION NAME: OnGUI
    * DESCRIPTION  : Draw notepad and contents.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnGUI()
    {
        note = EditorGUILayout.TextArea(note, GUILayout.Width(position.width - 5), GUILayout.Height(position.height - 30));
    }

    /**
    * FUNCTION NAME: OnFocus(
    * DESCRIPTION  : Restore notepad contents on refocused.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnFocus()
    {
        if (EditorPrefs.HasKey("LPK_Notes"))
            note = EditorPrefs.GetString("LPK_Notes");
    }

    /**
    * FUNCTION NAME: OnLostFocus
    * DESCRIPTION  : Save notes written to internal registry.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnLostFocus()
    {
        EditorPrefs.SetString("LPK_Notes", note);
    }

    /**
    * FUNCTION NAME: OnDestroy
    * DESCRIPTION  : Save notes written to internal registry.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnDestroy()
    {
        EditorPrefs.SetString("LPK_Notes", note);
    }
}

#endif  //UNITY_EDITOR
