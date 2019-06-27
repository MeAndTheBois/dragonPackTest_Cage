/***************************************************
File:           LPK_SelectAllObjectsBySpecifiers.cs
Authors:        Christopher Onorati
Last Updated:   3/19/2019
Last Version:   2018.3.4

Description:
  Wizard tool to select all objects in active scene via a
  tag.

This script is a basic and generic implementation of its 
functionality. It is designed for educational purposes and 
aimed at helping beginners.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using System.Collections;
using System.Collections.Generic;   /* Lists.        */
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

/**
* CLASS NAME  : LPK_SelectObjectsBySpecifiers
* DESCRIPTION : Select all game objects in the scene via specifiers.
**/
public class LPK_SelectObjectsBySpecifiers : ScriptableWizard
{
    //NOTENOTE:  Not using attributes in this due to not functioning.  May be a Unity limitation.
    public string m_SearchTag;

    //NOTENOTE:  Not using attributes in this due to not functioning.  May be a Unity limitation.
    public int m_SearchLayer = -1;

    /**
    * FUNCTION NAME: SelectAllObjectsByTag
    * DESCRIPTION  : Creates wizard to enable object selection via tag.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    [MenuItem("Utilities/Objects/Select By Specifiers #s")]
    static void SelectAllObjectsByTag()
    {
        ScriptableWizard.DisplayWizard<LPK_SelectObjectsBySpecifiers> ("Select All Objects By Specifiers", "Confirm Selection");
    }

    /**
    * FUNCTION NAME: OnWizardCreate
    * DESCRIPTION  : Finds all game objects via the selected tag upon startup.
    * INPUTS       : None
    * OUTPUTS      : None
    **/    
    void OnWizardCreate()
    {
        GameObject[] foundObjects = new GameObject[] {};

        //Tags were set to search for.
        if(!string.IsNullOrEmpty(m_SearchTag))
        {
            foundObjects = GameObject.FindGameObjectsWithTag(m_SearchTag);

            if (m_SearchLayer >= 0)
                foundObjects = FindGameObjectsInLayer(foundObjects);
        }

        //Only layers were set.
        else if (m_SearchLayer >= 0)
            foundObjects = FindGameObjectsInLayer();

        Selection.objects = foundObjects;
    }

    /**
    * FUNCTION NAME: FindGameObjectsInLayer
    * DESCRIPTION  : Helper function to find all game objects that are in a layer.
    * INPUTS       : searchObjects - Objects to parse through for layer matching.
    * OUTPUTS      : None
    **/    
    GameObject[] FindGameObjectsInLayer(GameObject[] searchObjects = null)
    {
        if(searchObjects == null)
            searchObjects = FindObjectsOfType<GameObject>();

        List<GameObject> layerObjects = new List<GameObject>();

        for (int i = 0; i < searchObjects.Length; i++)
        {
            if(searchObjects[i].layer == m_SearchLayer)
                layerObjects.Add(searchObjects[i]);
        }
        
        return layerObjects.ToArray();    
    }
}

#endif  //UNITY_EDITOR
