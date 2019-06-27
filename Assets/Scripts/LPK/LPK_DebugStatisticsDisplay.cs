/***************************************************
File:           LPK_DebugStatisticsDisplay
Authors:        Christopher Onorati
Last Updated:   2/9/2019
Last Version:   2018.3.4

Description:
  This component can be used to debug performance problems
  by getting statistics on LPK component and object usage.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : LPK_DebugStatisticsDisplay
* DESCRIPTION : Debugger component to return data on LPK usage statistics.
**/
public class LPK_DebugStatisticsDisplay : MonoBehaviour
{
    /************************************************************************************/

    [Header("Object Statistics")]

    [Tooltip("Total amount of LPK objects in the game.")]
    [PreventEditing]
    public uint m_LPKObjectCount;

    [Tooltip("Total amount of LPK objects that use Update functions in the game.")]
    [PreventEditing]
    public uint m_LPKUpdatedObjectsCount;

    [Header("Component Statistics")]

    [Tooltip("Total amount of LPK components in the game.")]
    [PreventEditing]
    public uint m_LPKComponentCount;

    [Tooltip("Total amount of LPK components that use Update in the game.")]
    [PreventEditing]
    public uint m_LPKUpdatedComponentCount;

    /**
    * FUNCTION NAME: Start
    * DESCRIPTION  : Connects to event listening.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void Start()
    {
        LPK_DebugStatistics.OnDebugStatisticsUpdated += UpdateDebugStatistics;
    }

    /**
    * FUNCTION NAME: UpdateDebugStatistics
    * DESCRIPTION  : Updates debug statistics when changes are made to objects and components in the game.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void UpdateDebugStatistics()
    {
        m_LPKObjectCount = LPK_DebugStatistics.GetTotalObjectCount();
        m_LPKUpdatedObjectsCount = LPK_DebugStatistics.GetTotalUpdatedObjectCount();
        m_LPKComponentCount = LPK_DebugStatistics.GetTotalComponentCount();
        m_LPKUpdatedComponentCount = LPK_DebugStatistics.GetTotalUpdatedComponentCount();
    }

    /**
    * FUNCTION NAME: OnDestroy
    * DESCRIPTION  : Detaches from event listening.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnDestroy()
    {
        LPK_DebugStatistics.OnDebugStatisticsUpdated -= UpdateDebugStatistics;
    }
}
