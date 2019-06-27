/***************************************************
File:           LPK_SupportLauncher.cs
Authors:        Christopher Onorati
Last Updated:   3/18/2019
Last Version:   2018.3.4

Description:
  Quick command to launch a browser to the support
  Discord server.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

/**
* CLASS NAME  : LPK_SupportLauncher
* DESCRIPTION : Launches the support page for the LPK on a hotkey.
**/
public class LPK_SupportLauncher : EditorWindow
{
    /**
    * FUNCTION NAME: LaunchSupportPage
    * DESCRIPTION  : Launch the discord support server.
    * INPUTS       : None
    * OUTPUTS      : None
    **/  
    [MenuItem("LPK/Help %h")]
    public static void LaunchSupportPage()
    {
        Application.OpenURL("https://discord.gg/cEZ5jYn");
    }
}

#endif
