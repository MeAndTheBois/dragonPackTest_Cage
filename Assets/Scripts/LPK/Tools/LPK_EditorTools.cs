/***************************************************
File:           LPK_EditorTools.cs
Authors:        Hellium - https://answers.unity.com/questions/1487864/change-a-variable-name-only-on-the-inspector.html
                Christopher Onorati
Last Updated:   3/19/2019
Last Version:   2018.3.4

Description:
  Contains helper classes for editor tools.

***************************************************/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;   /* Lists.        */
using UnityEngine.SceneManagement;  /* SceneDropdown */
using System.IO;                    /* SceneDropdown */

#if UNITY_EDITOR

/**
* CLASS NAME  : RenameEditor
* DESCRIPTION : Overrides the GUI of the editor view.
**/
[CustomPropertyDrawer(typeof(RenameAttribute))]
public class RenameEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, new GUIContent((attribute as RenameAttribute).m_sNewName));
    }
}

/**
* CLASS NAME  : RenameEditor
* DESCRIPTION : Prevents editing a value in the inspector.
**/
[CustomPropertyDrawer(typeof(PreventEditing))]
public class DisplayWithoutEditDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.AnimationCurve:
                break;
            case SerializedPropertyType.ArraySize:
                break;
            case SerializedPropertyType.Boolean:
                EditorGUI.LabelField(position, label, new GUIContent(property.boolValue.ToString()));
                break;
            case SerializedPropertyType.Bounds:
                break;
            case SerializedPropertyType.Character:
                break;
            case SerializedPropertyType.Color:
                break;
            case SerializedPropertyType.Enum:
                EditorGUI.LabelField(position, label, new GUIContent(property.enumDisplayNames[property.enumValueIndex]));
                break;
            case SerializedPropertyType.Float:
                EditorGUI.LabelField(position, label, new GUIContent(property.floatValue.ToString()));
                break;
            case SerializedPropertyType.Generic:
                break;
            case SerializedPropertyType.Gradient:
                break;
            case SerializedPropertyType.Integer:
                EditorGUI.LabelField(position, label, new GUIContent(property.intValue.ToString()));
                break;
            case SerializedPropertyType.LayerMask:
                break;
            case SerializedPropertyType.ObjectReference:
                break;
            case SerializedPropertyType.Quaternion:
                break;
            case SerializedPropertyType.Rect:
                break;
            case SerializedPropertyType.String:
                EditorGUI.LabelField(position, label, new GUIContent(property.stringValue));
                break;
            case SerializedPropertyType.Vector2:
                break;
            case SerializedPropertyType.Vector3:
                break;
            case SerializedPropertyType.Vector4:
                break;
        }
    }
}

/**
* CLASS NAME  : TagDropdownDrawer
* DESCRIPTION : Create a dropdown list of all tags in the current Unity project.
**/
[CustomPropertyDrawer(typeof(TagDropdown))]
public class TagDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Property is a string field that should be a tag dropdown.  If it isn't - abort!
        if(property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            List<string> tagList = new List<String>();
            tagList.Add("Not Set");
            tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

            int currentTagIndex = -1;

            //String value is empty, index set to 0.
            if (property.stringValue == "")
                currentTagIndex = 0;
            else
            {
                for(int i = 1; i < tagList.Count; i++)
                {
                    if (tagList[i] == property.stringValue)
                    {
                        currentTagIndex = i;
                        break;
                    }
                }
            }

            currentTagIndex = EditorGUI.Popup(position, label.text, currentTagIndex, tagList.ToArray());

            if(currentTagIndex == 0)
                property.stringValue = "";
            else if (currentTagIndex >= 1)
                property.stringValue = tagList[currentTagIndex];
            else
                property.stringValue = "";
        }

        //Not a string field - do normal drawing.
        else
            EditorGUI.PropertyField(position, property, label);

        EditorGUI.EndProperty();
    }
}

[CustomPropertyDrawer(typeof(SceneDropdown))]
public class SceneDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Property is a string field that should be a tag dropdown.  If it isn't - abort!
        if(property.propertyType == SerializedPropertyType.String)
        {
            List<string> sceneList = new List<String>();
            sceneList.Add("Not Set");

            for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
                sceneList.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));

            int currentIndex = -1;

            //String value is empty, index set to 0.
            if (property.stringValue == "")
                currentIndex = 0;
            else
            {
                for(int i = 1; i < sceneList.Count; i++)
                {
                    if (sceneList[i] == property.stringValue)
                    {
                        currentIndex = i;
                        break;
                    }
                }
            }

            currentIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneList.ToArray());

            if(currentIndex == 0)
                property.stringValue = "";
            else if (currentIndex >= 1)
                property.stringValue = sceneList[currentIndex];
            else
                property.stringValue = "";
        }

        //Not a string field - do normal drawing.
        else
            EditorGUI.PropertyField(position, property, label);
    }
}

/**
* CLASS NAME  : LayerDropdownDrawer
* DESCRIPTION : Create a dropdown list of all layers in the current Unity project.
**/
[CustomPropertyDrawer(typeof(LayerDropDown))]
public class LayerDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Property is a string field that should be a tag dropdown.  If it isn't - abort!
        if(property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            List<string> layerList = new List<String>();
            layerList.Add("Not Set");
            layerList.AddRange(UnityEditorInternal.InternalEditorUtility.layers);

            int currentTagIndex = -1;

            //String value is empty, index set to 0.
            if (property.stringValue == "")
                currentTagIndex = 0;
            else
            {
                for(int i = 1; i < layerList.Count; i++)
                {
                    if (layerList[i] == property.stringValue)
                    {
                        currentTagIndex = i;
                        break;
                    }
                }
            }

            currentTagIndex = EditorGUI.Popup(position, label.text, currentTagIndex, layerList.ToArray());

            if(currentTagIndex == 0)
                property.stringValue = "";
            else if (currentTagIndex >= 1)
                property.stringValue = layerList[currentTagIndex];
            else
                property.stringValue = "";
        }

        //Not a string field - do normal drawing.
        else
            EditorGUI.PropertyField(position, property, label);

        EditorGUI.EndProperty();
    }
}

#endif  //UNITY_EDITOR
