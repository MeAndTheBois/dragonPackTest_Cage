/***************************************************
File:           LPK_DebugDrawForwardVector.cs
Authors:        Christopher Onorati
Last Updated:   12/15/2018
Last Version:   2.17

Description:
  Draws a forward pointing vector for each object specified,
  with a set length and color by the user.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_DebugDrawForwardVector
* DESCRIPTION : Draws a forward pointing vector for each object in the class.
**/
[ExecuteInEditMode]
public class LPK_DebugDrawForwardVector : LPK_DebugBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Draw a debug line. This will only show up in the scene view of the editor.")]
    [Rename("Draw Debug Line")]
    public bool m_bDrawDebugLine = true;

    [Tooltip("Draw a line renderer.  This will show up in all views but is much more expenseive.")]
    [Rename("Draw Line Renderer")]
    public bool m_bDrawLineRenderer = false;

    [Tooltip("Length of the line to draw for forward vector.")]
    [Rename("Length")]
    public float m_flLength = 2.0f;

    [Tooltip("Color of the line to draw for forward vector.")]
    [Rename("Color")]
    public Color m_vecColor = Color.red;

    /************************************************************************************/

    //Debug line to draw.
    LPK_DebugLineDrawer m_line;

    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Used to clean up any leftover data when deactive.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        if (m_line != null && !m_bDrawLineRenderer)
            DestroyImmediate(m_line.m_pGameObject);
    }

    /**
    * FUNCTION NAME: Draw
    * DESCRIPTION  : Draw debug info for the gameobject.
    * INPUTS       : obj - Game object to draw debug info for.
    * OUTPUTS      : None
    **/
    override protected void Draw(GameObject obj)
    {
        //Create line if appropriate
        if (m_bDrawLineRenderer && ( m_line == null || m_line.m_pGameObject == null) &&
            ((m_bDrawInEditor && Application.isEditor) || (m_bDrawInGame && !Application.isEditor)))
            m_line = new LPK_DebugLineDrawer(m_vecColor, gameObject);

        if (Application.isEditor && m_bDrawDebugLine)
            Debug.DrawRay(transform.position, transform.up * m_flLength, m_vecColor, 0.01f, true);
        if(m_bDrawLineRenderer && Application.isEditor && m_line != null)
            m_line.DrawLineInGameView(transform.position, transform.position + (transform.up * m_flLength));
        if (m_bDrawLineRenderer && !Application.isEditor && m_line != null)
            m_line.DrawLineInGameView(transform.position, transform.position + (transform.up * m_flLength));
    }

    /**
    * FUNCTION NAME: Draw
    * DESCRIPTION  : Draw debug info for the gameobject.
    * INPUTS       : obj - Game object to draw debug info for.
    * OUTPUTS      : None
    **/
    override protected void Undraw(GameObject obj)
    {
        if (m_line != null)
            DestroyImmediate(m_line.m_pGameObject);
    }
}
