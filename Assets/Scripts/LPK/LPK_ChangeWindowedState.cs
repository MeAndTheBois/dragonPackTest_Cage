/***************************************************
File:           LPK_ChangeWindowedState.cs
Authors:        Christopher Onorati
Last Updated:   12/15/2018
Last Version:   2.17

Description:
  This component can be used to change the fullscreen mode
  of the game window during runtime.  This component is
  ideally used on an options menu.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_ChangeWindowedState
* DESCRIPTION : Component used to change windowed state of game.
**/
public class LPK_ChangeWindowedState : LPK_LogicBase
{
    /************************************************************************************/

    public enum LPK_WindowToggleType
    {
        CHANGE_FULLSCREEN,
        CHANGE_WINDOWED,
        CHANGE_TOGGLE,
    };

    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("How to change the windowed state when activated via event.")]
    [Rename("Toggle Type")]
    public LPK_WindowToggleType m_eWindowToggleType;

    /**
    * FUNCTION NAME: SetWindowType
    * DESCRIPTION  : Changes windowed state.  Moved to public so UI buttons can interact with this.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    public void SetWindowType()
    {
        if (m_eWindowToggleType == LPK_WindowToggleType.CHANGE_FULLSCREEN)
            Screen.fullScreen = true;
        else if (m_eWindowToggleType == LPK_WindowToggleType.CHANGE_WINDOWED)
            Screen.fullScreen = false;
        else
            Screen.fullScreen = !Screen.fullScreen;
    }
}
