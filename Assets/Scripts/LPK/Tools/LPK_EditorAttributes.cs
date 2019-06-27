/***************************************************
File:           LPK_EditorTools.cs
Authors:        Hellium - https://answers.unity.com/questions/1487864/change-a-variable-name-only-on-the-inspector.html
                Christopher Onorati
Last Updated:   3/19/2019
Last Version:   2018.3.4

Description:
  Contains helper attributes for editor tools.

***************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* CLASS NAME  : RenameAttribute
* DESCRIPTION : Overrides the default label of a property to assign a new name.
**/
public class RenameAttribute : PropertyAttribute
{
    public string m_sNewName { get; private set; }
    public RenameAttribute(string name)
    {
        m_sNewName = name;
    }
}

/**
* CLASS NAME  : PreventEditing
* DESCRIPTION : Prevents a public value from being edited in the inspector.
**/
public class PreventEditing : PropertyAttribute
{
}

/**
* CLASS NAME  : TagDropdown
* DESCRIPTION : Create a dropdown showing all tags instead of using strings.
**/
public class TagDropdown : PropertyAttribute
{
}

/**
* CLASS NAME  : SceneDropdown
* DESCRIPTION : Create a dropdown showing all scenes currently in the build instead of using strings.
**/
public class SceneDropdown : PropertyAttribute
{
}

/**
* CLASS NAME  : LayerDropdown
* DESCRIPTION : Create a dropdown showing all layers currently in the project instead of using strings.
**/
public class LayerDropDown : PropertyAttribute
{
}
