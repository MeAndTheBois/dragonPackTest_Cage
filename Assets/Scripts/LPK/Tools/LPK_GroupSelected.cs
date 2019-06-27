/***************************************************
File:           LPK_GroupSelected.cs
Authors:        Doug Zwick
Last Updated:   1/30/19
Last Version:   3.0f2

Description:
  Defines an editor window with options to group
  the currently selected GameObjects under a
  common parent (that it also creates).

Copyright 2019, DigiPen Institute of Technology
***************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class GroupSelected : EditorWindow
{

    /************************************************************************************/

    public enum GroupingMode
    {
        Average,
        Origin,
        First,
        LeastX,
        LeastY,
        LeastZ,
        GreatestX,
        GreatestY,
        GreatestZ,
        SpecifiedPosition,
    };

    /************************************************************************************/

    // Descriptions of each of the above options
    public static readonly Dictionary<GroupingMode, string> GroupingModeDescriptions =
      new Dictionary<GroupingMode, string>()
      {
        { GroupingMode.Average,           "selection's average position" },
        { GroupingMode.Origin,            "world origin, (0, 0, 0)" },
        { GroupingMode.First,             "position of the first-selected object" },
        { GroupingMode.LeastX,            "least (furthest left) X position of the selection" },
        { GroupingMode.LeastY,            "least (furthest down) Y position of the selection" },
        { GroupingMode.LeastZ,            "least (furthest back) Z position of the selection" },
        { GroupingMode.GreatestX,         "greatest (furthest right) X position of the selection" },
        { GroupingMode.GreatestY,         "greatest (furthest up) Y position of the selection" },
        { GroupingMode.GreatestZ,         "greatest (furthest forward) Z position of the selection" },
        { GroupingMode.SpecifiedPosition, "specified position:" },
      };
    
    // What the new parent object should be named
    public string ParentName = "Root";
    // The user's choice of the above set of grouping modes
    public GroupingMode GroupAt = GroupingMode.Average;
    // Where the new parent should go. Used only if GroupAt equals GroupingMode.SpecifiedPosition
    public Vector3 SpecifiedPosition = new Vector3();
    // Whether this editor window should close itself after the grouping is done
    public bool CloseWhenFinished = true;
    
    // The vertical size of this window's Group button
    private static readonly float ButtonHeight = 30;
    
    // The command to open this window appears in the "Utilities" menu
    // under the "Objects" group as "Group Selected",
    // and its hotkey is Ctrl + G
    [MenuItem("Utilities/Objects/Group Selected %g")]
    public static void ShowWindow()
    {
      var window = GetWindow<GroupSelected>();
      window.titleContent = new GUIContent("Group Selected");
    }
    
    private void OnGUI()
    {
      // Create the string input field for providing the parent name
      ParentName = EditorGUILayout.TextField("Parent Name", ParentName);
    
      // If the ParentName is invalid, revert it to the default
      if (string.IsNullOrEmpty(ParentName))
        ParentName = "Root";
    
      // Create the enum pop-up list for specifying the grouping mode
      GroupAt = (GroupingMode)EditorGUILayout.EnumPopup("Group At", GroupAt);
      // Supply a pair of labels explaining what the choice of grouping mode does
      var description = GroupingModeDescriptions[GroupAt];
      EditorGUILayout.LabelField("Places the parent at the");
      EditorGUILayout.LabelField(description);
    
      // Only create the Vector3 input fields for the specified position
      // if GroupAt is equal to GroupingMode.SpecifiedPosition
      if (GroupAt == GroupingMode.SpecifiedPosition)
        SpecifiedPosition = EditorGUILayout.Vector3Field("Position", SpecifiedPosition);
    
      // Create the boolean check box for specifying whether to close the window at the end
      CloseWhenFinished = EditorGUILayout.Toggle("Close When Finished", CloseWhenFinished);
    
      // Grouping can only happen if there is at least one object selected
      var relevantObjects = Selection.GetFiltered<GameObject>(SelectionMode.Editable);
      var notEnoughObjects = relevantObjects.Length == 0;
    
      // Create a flexible space to align the Group button at the bottom of the window
      GUILayout.FlexibleSpace();
    
      // The Group button will be grayed out if there isn't at least one object selected
      EditorGUI.BeginDisabledGroup(notEnoughObjects);
    
      // Create the button to do the actual grouping. If it's pressed, run the Group function
      if (GUILayout.Button("Group", GUILayout.Height(ButtonHeight)))
        Group();
    
      EditorGUI.EndDisabledGroup();
      
      // If there are enough objects...
      if (!notEnoughObjects)
      {
        // ... Process the keyboard input to check for the Enter key
        var e = Event.current;
    
        // if the Return or Enter key is pressed, run the Group function
        if (e.type == EventType.KeyDown)
          if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
            Group();
      }
    }
    
    private void Group()
    {
      // Create a name for the undo group (what shows up in the Edit menu).
      // NOTENOTE: This doesn't seem to work with Redo, and I'm not sure if
      // whether it's my fault
      Undo.SetCurrentGroupName("Group Selection");
      // Get the current undo group so that all the stuff this function does
      // can be undone / redone together as a unit
      var undoGroup = Undo.GetCurrentGroup();
    
      // Just get the objects in the selection that are editable GameObjects
      var children = Selection.GetFiltered<GameObject>(SelectionMode.Editable);
    
      // Create the new parent, naming it as specified by the user
      var newParent = new GameObject(ParentName);
      // Register the creation of the parent with the Undo system
      Undo.RegisterCreatedObjectUndo(newParent, "Create Parent");
      // Grab the transform of the first object that was selected
      var activeTransform = Selection.activeGameObject.transform;
      // Parent the new parent to the active object's parent. This way, if
      // the selected objects were already parented to something else when
      // this grouping is performed, their new parent will now be parented
      // to that object, so that they won't get pulled out of the hierarchy
      // they were in beforehand
      newParent.transform.SetParent(activeTransform.parent, worldPositionStays: true);
    
      // Where the parent will be placed
      var parentPosition = Vector3.zero;
    
      // If the selected grouping mode is...
      switch (GroupAt)
      {
          // Average:
          case GroupingMode.Average:
            // sum up all the kids' positions and divide by their count
            foreach (var child in children)
            {
              parentPosition += child.transform.position;
            }
            parentPosition /= children.Length;
            break;
          // First:
          case GroupingMode.First:
            // the parent goes where the active object is
            parentPosition = activeTransform.position;
            break;
          // LeastX:
          case GroupingMode.LeastX:
            // Run through the list of children and find the smallest X position.
            // The parent goes at that position
            parentPosition = Vector3.positiveInfinity;
            foreach (var child in children)
            {
              var childPosition = child.transform.position;
              if (childPosition.x < parentPosition.x)
                parentPosition = childPosition;
            }
            break;
          // LeastY:
          case GroupingMode.LeastY:
            // Run through the list of children and find the smallest Y position.
            // The parent goes at that position
            parentPosition = Vector3.positiveInfinity;
            foreach (var child in children)
            {
              var childPosition = child.transform.position;
              if (childPosition.y < parentPosition.y)
                parentPosition = childPosition;
            }
            break;
          // LeastZ:
          case GroupingMode.LeastZ:
            // Run through the list of children and find the smallest Z position.
            // The parent goes at that position
            parentPosition = Vector3.positiveInfinity;
            foreach (var child in children)
            {
              var childPosition = child.transform.position;
              if (childPosition.z < parentPosition.z)
                parentPosition = childPosition;
            }
            break;
          // GreatestX:
          case GroupingMode.GreatestX:
            // Run through the list of children and find the greatest X position.
            // The parent goes at that position
            parentPosition = Vector3.negativeInfinity;
            foreach (var child in children)
            {
              var childPosition = child.transform.position;
              if (childPosition.x > parentPosition.x)
                parentPosition = childPosition;
            }
            break;
          // GreatestY:
          case GroupingMode.GreatestY:
            // Run through the list of children and find the greatest Y position.
            // The parent goes at that position
            parentPosition = Vector3.negativeInfinity;
            foreach (var child in children)
            {
              var childPosition = child.transform.position;
              if (childPosition.y > parentPosition.y)
                parentPosition = childPosition;
            }
            break;
          // GreatestZ:
          case GroupingMode.GreatestZ:
            // Run through the list of children and find the greatest Z position.
            // The parent goes at that position
            parentPosition = Vector3.negativeInfinity;
            foreach (var child in children)
            {
              var childPosition = child.transform.position;
              if (childPosition.z > parentPosition.z)
                parentPosition = childPosition;
            }
            break;
          // SpecifiedPosition:
          case GroupingMode.SpecifiedPosition:
            // the parent goes where the user told it to go
            parentPosition = SpecifiedPosition;
            break;
          // GroupingMode.Origin:
          default:
            // don't do anything to the parentPosition variable because it's already at the origin
            break;
        }
    
        // Finally, put the parent at the position that has been decided upon
        newParent.transform.position = parentPosition;
    
        // Sort the kids by their sibling orders, so that their order in their hierarchies
        // is preserved after grouping
        System.Array.Sort(children, SiblingOrderComparison);
    
        // For each kid, attach it to the new parent via the Undo system
        foreach (var child in children)
        {
          Undo.SetTransformParent(child.transform, newParent.transform, "Add Object To Group");
        }
    
        // Collapse the current group so that it can all be undone / redone at once
        Undo.CollapseUndoOperations(undoGroup);
    
        // If the user has said so, close this window when it's all done
        if (CloseWhenFinished)
          Close();
    }
    
    // The function used to sort the children array
    private static int SiblingOrderComparison(GameObject a, GameObject b)
    {
        var aIndex = a.transform.GetSiblingIndex();
        var bIndex = b.transform.GetSiblingIndex();
        
        // Return negative if the first object's sibling index is less than the second's
        if (aIndex < bIndex)
          return -1;
        // Return positive if the first object's sibling index is greater than the second's
        if (aIndex > bIndex)
          return 1;
        
        // Return zero if the objects' sibling indices are equal
        return 0;
    }
}

#endif  //UNITY_EDITOR
