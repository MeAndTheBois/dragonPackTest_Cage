/***************************************************
File:           LPK_VelocityEffect
Authors:        Christopher Onorati
Last Updated:   12/3/2018
Last Version:   2.17

Description:
  This component can be added to any object with a
  RigidBody to cause it to apply a custom velocity.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_VelocityEffect
* DESCRIPTION : This component can be added to any object with a RigidBody to cause it to apply a custom velocity.
**/
[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
public class LPK_VelocityEffect : LPK_LogicBase
{
    /************************************************************************************/

    public enum LPK_VelocityApplyDirection
    {
        WORLD,
        LOCAL,
    };

    public enum LPK_VelocityApplyMode
    {
        SET,
        ADD,
    };

    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Whether the velocity effect should start active or not.")]
    [Rename("Start Active")]
    public bool m_bActive = true;

    [Tooltip("Whether the velocity should ba applied every frame. Will only be applied on initialize otherwise.")]
    [Rename("Every Frame")]
    public bool m_bEveryFrame = false;

    [Tooltip("Whether the velocity should be applied Locally or Globally")]
    [Rename("Direction")]
    public LPK_VelocityApplyDirection m_eDirection = LPK_VelocityApplyDirection.WORLD;

    [Tooltip("Whether velocity should be Added or Set")]
    [Rename("Mode")]
    public LPK_VelocityApplyMode m_eMode = LPK_VelocityApplyMode.SET;

    [Tooltip("Direction to apply the velocity in.")]
    [Rename("Forward")]
    public Vector3 m_vecForward = new Vector3(0, 1, 0);

    [Tooltip("Magnitude of the speed to be applied.")]
    [Rename("Speed")]
    public float m_flSpeed = 5;

    [Header("Event Receiving Info")]

    [Tooltip("Which event will trigger this component to be active.")]
    public LPK_EventList m_EventTrigger = new LPK_EventList();

    /************************************************************************************/
    private Transform m_cTransform;
    private Rigidbody2D m_cRigidBody;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Applies initial velocity if appropriate and sets rigidbody component.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart()
    {
        InitializeEvent(m_EventTrigger, OnEvent);

        m_cTransform = GetComponent<Transform>();
        m_cRigidBody = GetComponent<Rigidbody2D>();

        if (!m_bEveryFrame && m_bActive)
            ApplyVelocity();
    }

    /**
    * FUNCTION NAME: OnUpdate
    * DESCRIPTION  : Applies ongoing velocity if appropriate.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnUpdate()
    {
        if (m_bEveryFrame && m_bActive)
            ApplyVelocity();
	}

    /**
    * FUNCTION NAME: OnEvent
    * DESCRIPTION  : Sets the velocity effect active.
    * INPUTS       : data - Event data to parse for validation.
    * OUTPUTS      : None
    **/
    override protected void OnEvent(LPK_EventManager.LPK_EventData data)
    {
        //Incorrect object.
        if (!ShouldRespondToEvent(data))
            return;

        m_bActive = true;

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Velocity Effect Avtive");

        if (!m_bEveryFrame)
            ApplyVelocity();
    }

   /**
   * FUNCTION NAME: ApplyVelocity
   * DESCRIPTION  : Manages velocity change on object with component.
   * INPUTS       : None
   * OUTPUTS      : None
   **/
    void ApplyVelocity()
    {
        if (m_eDirection == LPK_VelocityApplyDirection.LOCAL)
        {
            if (m_eMode == LPK_VelocityApplyMode.SET)
                m_cRigidBody.velocity = m_cTransform.InverseTransformDirection(m_vecForward) * m_flSpeed;
            else
                m_cRigidBody.velocity += (Vector2)m_cTransform.InverseTransformDirection(m_vecForward) * m_flSpeed;
        }
        else
        {
            if (m_eMode == LPK_VelocityApplyMode.SET)
                m_cRigidBody.velocity = m_vecForward * m_flSpeed;
            else
                m_cRigidBody.velocity += (Vector2)m_vecForward * m_flSpeed;
        }

        if (m_bPrintDebug)
            LPK_PrintDebug(this, "Velocity Applied");
    }
}
