/***************************************************
File:           LPK_FaceVelocity.cs
Authors:        Christopher Onorati
Last Updated:   2/15/2019
Last Version:   2018.3.4

Description:
  This component causes an object to face its velocity
  either on start or every frame.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_FaceVelocity
* DESCRIPTION : Basic facing component.
**/
[RequireComponent(typeof(Rigidbody2D))]
public class LPK_FaceVelocity : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Whether to update the object to face velocity on start or every frame.")]
    [Rename("Every Frame")]
    public bool m_bEveryFrame = true;

    /************************************************************************************/

    Rigidbody2D m_cRigidBody;

    /**
     * FUNCTION NAME: OnStart
     * DESCRIPTION  : Checks to ensure proper components are on the object for facing.
     * INPUTS       : None
     * OUTPUTS      : None
     **/
    override protected void OnStart()
    {
        m_cRigidBody = GetComponent<Rigidbody2D>();

        if (!m_bEveryFrame)
        {
            Vector2 dir = m_cRigidBody.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Manages facing direction if user specified every frame updating.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        if (!m_bEveryFrame)
            return;

        Vector2 dir = m_cRigidBody.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
