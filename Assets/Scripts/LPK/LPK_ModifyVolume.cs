/***************************************************
File:           LPK_ModifyVolume.cs
Authors:        Christopher Onorati
Last Updated:   12/17/2018
Last Version:   2.17

Description:
  Implementation of a volume manager to adjust Audio Source
  volumes based on the type of FX they play at runtime.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_ModifyVolume
* DESCRIPTION : Component to modify audio volumes at run time.
**/
[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
public class LPK_ModifyVolume : LPK_LogicBase
{
    [Header("Component Properties")]

    [Tooltip("Type of audio this gameobject will emit.  Note that each game object should only emit one audio file.")]
    [Rename("Audio Type")]
    public LPK_VolumeManager.LPK_AudioType m_eAudioType;

    /************************************************************************************/

    AudioSource m_cAudioSource;

    /**
    * FUNCTION NAME: OnStart
    * DESCRIPTION  : Connects to event listening.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    override protected void OnStart ()
    {
        m_cAudioSource = GetComponent<AudioSource>();

        //Set initial audio levels.
        SetAudioLevel();

        LPK_EventList audioLevelsList = new LPK_EventList();
        audioLevelsList.m_OptionManagerEventTrigger = new LPK_EventList.LPK_OPTION_MANAGER_EVENTS[] { LPK_EventList.LPK_OPTION_MANAGER_EVENTS.LPK_AudioLevelsAdjusted };

        InitializeEvent(audioLevelsList, OnAudioLevelsChange, false);
    }

    /**
    * FUNCTION NAME: OnAudioLevelsChange
    * DESCRIPTION  : Call the Audio Source's volume.
    * INPUTS       : data - Event registration data (unused).
    * OUTPUTS      : None
    **/
    void OnAudioLevelsChange(LPK_EventManager.LPK_EventData data)
    {
        SetAudioLevel();
    }

    /**
    * FUNCTION NAME: SetAudioLevel
    * DESCRIPTION  : Adjusts the Audio Source's volume.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void SetAudioLevel()
    {
        if (m_eAudioType == LPK_VolumeManager.LPK_AudioType.MUSIC)
            m_cAudioSource.volume = LPK_VolumeManager.m_flMusicLevel * LPK_VolumeManager.m_flMasterLevel;
        else if (m_eAudioType == LPK_VolumeManager.LPK_AudioType.SFX)
            m_cAudioSource.volume = LPK_VolumeManager.m_flSFXLevel * LPK_VolumeManager.m_flMasterLevel;
        else if (m_eAudioType == LPK_VolumeManager.LPK_AudioType.VOICE)
            m_cAudioSource.volume = LPK_VolumeManager.m_flVoiceLevel * LPK_VolumeManager.m_flMasterLevel;
    }
}
