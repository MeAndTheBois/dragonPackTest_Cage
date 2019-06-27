/***************************************************
File:           LPK_VolumeIndicator.cs
Authors:        Christopher Onorati
Last Updated:   12/17/18
Last Version:   2.17

Description: 
  This component is an indicator to display the current
  volume level of a sound type on a UI screen.  This should
  ideally be used on a canvas represting the options
  screen.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   /*Text */

/**
* CLASS NAME  : LPK_VolumeIndicator
* DESCRIPTION : Indicator to show what level the volume is at.
**/
[RequireComponent(typeof(Text))]
public class LPK_VolumeIndicator : LPK_LogicBase
{
    /************************************************************************************/

    public enum LPK_AudioDisplayType
    {
        SFX,
        MUSIC,
        VOICE,
        MASTER,
    };

    /************************************************************************************/

    [Header("Component Properties")]

    [Tooltip("Type of audio this gameobject will emit.  Note that each game object should only emit one audio file.")]
    [Rename("Audio Type")]
    public LPK_AudioDisplayType m_eAudioType;

    /************************************************************************************/

    Text m_cText;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Connects to event listening and initial display.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart ()
    {
        m_cText = gameObject.GetComponent<Text>();

        SetText();

        LPK_EventList audioLevelsList = new LPK_EventList();
        audioLevelsList.m_OptionManagerEventTrigger = new LPK_EventList.LPK_OPTION_MANAGER_EVENTS[] { LPK_EventList.LPK_OPTION_MANAGER_EVENTS.LPK_AudioLevelsAdjusted };

        InitializeEvent(audioLevelsList, OnAudioLevelsChange, false);
    }

    /**
    * FUNCTION NAME: OnAudioLevelsChange
    * DESCRIPTION  : Call the Set Text function.
    * INPUTS       : data - Event registration data (unused).
    * OUTPUTS      : None
    **/
    void OnAudioLevelsChange(LPK_EventManager.LPK_EventData data)
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
        if (m_eAudioType == LPK_AudioDisplayType.MUSIC)
            m_cText.text = Mathf.RoundToInt(LPK_VolumeManager.m_flMusicLevel * 10).ToString();
        else if (m_eAudioType == LPK_AudioDisplayType.SFX)
            m_cText.text = Mathf.RoundToInt(LPK_VolumeManager.m_flSFXLevel * 10).ToString();
        else if (m_eAudioType == LPK_AudioDisplayType.VOICE)
            m_cText.text = Mathf.RoundToInt(LPK_VolumeManager.m_flVoiceLevel * 10).ToString();
        else if (m_eAudioType == LPK_AudioDisplayType.MASTER)
            m_cText.text = Mathf.RoundToInt(LPK_VolumeManager.m_flMasterLevel * 10).ToString();
    }

    /**
    * FUNCTION NAME: OnDestroyed
    * DESCRIPTION  : Remove event listening.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    protected override void OnDestroyed()
    {
        DetachFunction(OnAudioLevelsChange);
    }
}
