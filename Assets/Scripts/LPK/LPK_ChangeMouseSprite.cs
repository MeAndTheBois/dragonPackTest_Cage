/***************************************************
File:           LPK_ChangeMouseSprite.cs
Authors:        Christopher Onorati
Last Updated:   12/2/2018
Last Version:   2.17

Description:
  This component changes the visual display of the mouse.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;
using System.Collections;

/**
* CLASS NAME  : CreateMouseCursor
* DESCRIPTION : Add to a levelsettings object to change the mouse cursor.
**/
public class LPK_ChangeMouseSprite : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Texture to use for the mouse cursor.")]
    [Rename("Mouse Sprite")]
    public Texture2D m_CursorImage;

    [Tooltip("Mouse input location.")]
    [Rename("Mouse location.")]
    public Vector2 m_vecHotSpot;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets the mouse cursor's new image upon loading the component.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        Cursor.SetCursor(m_CursorImage, m_vecHotSpot, CursorMode.Auto);
    }

    /**
    * FUNCTION NAME: SetCursor
    * DESCRIPTION  : Allows for cursor image modificaiton during playtime.
    * INPUTS       : Texture2D : cursorImage - 2D image to use for the mouse cursor.
                     Vector2   : hotSpot     - Location for the game to listen to mouse input. 
    * OUTPUTS      : None
    **/
    void SetCursor(Texture2D cursorImage, Vector2 hotSpot)
    {
        Cursor.SetCursor(cursorImage, hotSpot, CursorMode.Auto);

        m_CursorImage = cursorImage;
        m_vecHotSpot = hotSpot;
    }
}
