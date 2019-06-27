/***************************************************
File:           LPK_VelocityTowardsGameObject
Authors:        Christopher Onorati
Last Updated:   2/25/2019
Last Version:   2018.3.4

Description:
  This component can be added to any object with a
  RigidBody to cause it to apply a velocity force towards
  another object.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_VelocityTowardsGameObject
* DESCRIPTION : This component can be added to any game object with a RigidBody to cause it to move towards a game object.
**/
[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
public class LPK_VelocityTowardsGameObject : LPK_LogicBase
{
    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Whether the velocity should ba applied every frame. Will only be applied once otherwise.")]
    [Rename("Only Once")]
    public bool m_bOnlyOnce = false;

    [Tooltip("Game object to move towards.  If set to null, try to find the first object with the specified tag.")]
    [Rename("Target Game Object")]
    public GameObject m_pTargetGameObject;

    [Tooltip("Tags to move the object towards.  Will start at the top trying to find the first object of the tag.")]
    [TagDropdown]
    public string[] m_TargetTags;

    [Tooltip("Max distance used to search for game objects.  If set to 0, detect objects anywhere.")]
    [Rename("Detect Radius")]
    public float m_flRadius = 10.0f;

    [Tooltip("Force to be applied.")]
    [Rename("Force")]
    public float m_flSpeed = 5;

    /************************************************************************************/

    bool m_bHasAppliedVelocity;

    /************************************************************************************/

    Rigidbody2D m_cRigidBody;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Sets rigidbody component.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        m_cRigidBody = GetComponent<Rigidbody2D>();
    }

    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Applies ongoing velocity if appropriate.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        if(m_bOnlyOnce && m_bHasAppliedVelocity)
            return;
        
        ApplyVelocity();
    }

    /**
     * FUNCTION NAME: ApplyVelocity
     * DESCRIPTION  : Applies velocity to object.
     * INPUTS       : None
     * OUTPUTS      : None
     **/
    void ApplyVelocity()
    {
        if (m_pTargetGameObject == null)
            if (!FindGameObject())
                return;

        //Velocity application.
        m_cRigidBody.velocity = (m_pTargetGameObject.transform.position - transform.position).normalized * m_flSpeed;

        m_bHasAppliedVelocity = true;
    }

    /**
     * FUNCTION NAME: FindGameObject
     * DESCRIPTION  : Applies ongoing velocity if appropriate.
     * INPUTS       : None
     * OUTPUTS      : bool - True/false of if a game object was found and set.
     **/
    bool FindGameObject()
    {
        for (int i = 0; i < m_TargetTags.Length; i++)
        {
            List<GameObject> objects = new List<GameObject>();
            GetGameObjectsInRadius(objects, m_flRadius, 1, m_TargetTags[i]);

            if(objects[0] != null)
            {
                m_pTargetGameObject = objects[0];
                return true;
            }
        }

        return false;
    }
}
