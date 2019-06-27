/***************************************************
File:           LPK_TranslationBounds.cs
Authors:        Christopher Onorati
Last Updated:   11/28/2018
Last Version:   2.17

Description:
  This component keeps its owner within the specified
  bounds on Translation. It depends on initialization
  order to be properly compatible with other components,
  such as LPK_FollowObject.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_TranslationBounds
* DESCRIPTION : Locks an object to a specified bounds.
**/
[RequireComponent(typeof(Transform))]
public class LPK_TranslationBounds : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("The minimum value that the translation can reach")]
    [Rename("Min Bounds")]
    public Vector3 m_vecMin = new Vector3( -10, -10, -10 );

    [Tooltip("The maximum value that the translation can reach")]
    [Rename("Max Bounds")]
    public Vector3 m_vecMax = new Vector3(10, 10, 10);

    [Tooltip("Whether this component should constrain local position or world position.")]
    [Rename("Local")]
    public bool m_bLocal = false;

    /************************************************************************************/
    private Transform m_cTransform;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets up the component dependencies.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        m_cTransform = GetComponent<Transform>();
    }

    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Manages clamping of translations.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        Vector3 vecModifiedTransform;

        if (m_bLocal)
            vecModifiedTransform = m_cTransform.localPosition;
        else
            vecModifiedTransform = m_cTransform.position;

        vecModifiedTransform.x = Mathf.Clamp(vecModifiedTransform.x, m_vecMin.x, m_vecMax.x);
        vecModifiedTransform.y = Mathf.Clamp(vecModifiedTransform.y, m_vecMin.y, m_vecMax.y);
        vecModifiedTransform.z = Mathf.Clamp(vecModifiedTransform.z, m_vecMin.z, m_vecMax.z);

        if (m_bLocal)
            m_cTransform.localPosition = vecModifiedTransform;
        else
            m_cTransform.position = vecModifiedTransform;
    }
}
