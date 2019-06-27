/***************************************************
File:           LPK_DifficultyIndicator.cs
Authors:        Christopher Onorati
Last Updated:   12/18/18
Last Version:   2.17

Description: 
  This component is an indicator to display the current
  diffuclty level of the game.  This should ideally be
  used on a canvas represting the options screen.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   /* Text */

/**
* CLASS NAME  : LPK_DifficultyIndicator
* DESCRIPTION : Indicator to show what level the diffiuclty is at.
**/
[RequireComponent(typeof(Text))]
public class LPK_DifficultyIndicator : LPK_LogicBase
{
    /************************************************************************************/

    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Display name to show for difficulty level 1.")]
    [Rename("Level 1 Name")]
    public string m_sDifficultyOneName;

    [Tooltip("Display name to show for difficulty level 2.")]
    [Rename("Level 1 Name")]
    public string m_sDifficultyTwoName;

    [Tooltip("Display name to show for difficulty level 3.")]
    [Rename("Level 1 Name")]
    public string m_sDifficultyThreeName;

    /************************************************************************************/

    Text m_cText;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Connects to event listening and initial display.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    protected override void OnStart()
    {
        m_cText = GetComponent<Text>();

        SetText();

        LPK_EventList difficultyList = new LPK_EventList();
        difficultyList.m_OptionManagerEventTrigger = new LPK_EventList.LPK_OPTION_MANAGER_EVENTS[] { LPK_EventList.LPK_OPTION_MANAGER_EVENTS.LPK_DifficultyLevelAdjusted };

        InitializeEvent(difficultyList, OnDifficultyLevelChange, false);
    }

    /**
    * FUNCTION NAME: OnDifficultyLevelChange
    * DESCRIPTION  : Call the Set Text function.
    * INPUTS       : data - Event registration data (unused).
    * OUTPUTS      : None
    **/
    void OnDifficultyLevelChange(LPK_EventManager.LPK_EventData data)
    {
        SetText();
    }

    /**
    * FUNCTION NAME: SetText
    * DESCRIPTION  : Set the text to display on screen.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void SetText()
    {
        //Easy
        if (LPK_DifficultyManager.m_eDifficultyLevel == LPK_DifficultyManager.LPK_DifficultyLevel.EASY)
            m_cText.text = m_sDifficultyOneName;

        //Medium
        else if (LPK_DifficultyManager.m_eDifficultyLevel == LPK_DifficultyManager.LPK_DifficultyLevel.MEDIUM)
            m_cText.text = m_sDifficultyTwoName;

        //Hard
        else if (LPK_DifficultyManager.m_eDifficultyLevel == LPK_DifficultyManager.LPK_DifficultyLevel.HARD)
            m_cText.text = m_sDifficultyThreeName;
    }

    /**
    * FUNCTION NAME: OnDestroyed
    * DESCRIPTION  : Remove event listening.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    protected override void OnDestroyed()
    {
        DetachFunction(OnDifficultyLevelChange);
    }
}
