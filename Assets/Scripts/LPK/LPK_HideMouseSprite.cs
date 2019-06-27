/***************************************************
File:           LPK_HideMouseSprite.cs
Authors:        Christopher Onorati
Last Updated:   12/2/2018
Last Version:   2.17

Description:
  This component hides the mouse from the screen.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;
using System.Collections;

/**
* CLASS NAME  : HideMouse
* DESCRIPTION : Hides the mouse from the screen.
**/
public class LPK_HideMouseSprite : LPK_LogicBase
{
    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Hides the cursor automatically.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        Cursor.visible = false;
    }
}
