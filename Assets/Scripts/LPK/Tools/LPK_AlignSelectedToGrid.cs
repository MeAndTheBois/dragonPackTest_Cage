/***************************************************
File:           LPK_AlignSelectedToGrid.cs
Authors:        Doug Zwick, James Farris
Last Updated:   2/6/19
Last Version:   3.0f2

Description:
  Defines a command that aligns the selected
  object(s) to the grid defined by the X, Y, and Z
  Move snap distances in the editor's Snap Settings.

Copyright 2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class AlignSelectedToGrid : EditorWindow
{
  // This command appears in the "Utilities" menu under the "Objects" group as
  // "Align Selected to Grid", and its hotkey is Ctrl + Shift + G
  [MenuItem("Utilities/Objects/Align Selected to Grid %#g")]
  public static void Align()
  {
    // Just get the objects in the selection that are editable GameObjects
    var relevantObjects = Selection.GetFiltered<GameObject>(SelectionMode.Editable);

    // If none are selected, then nothing happens
    if (relevantObjects.Length == 0)
      return;

    // The individual snap values have to be pulled
    // out of the editor preferences like so
    var snapX = EditorPrefs.GetFloat("MoveSnapX");
    var snapY = EditorPrefs.GetFloat("MoveSnapY");
    var snapZ = EditorPrefs.GetFloat("MoveSnapZ");

    // Snapping cannot occur on an axis whose value is 0.
    // If all three axes have zero values, then nothing happens
    if (snapX == 0 && snapY == 0 && snapZ == 0)
    {
      Debug.LogWarning("All three snap distances are 0, so no snapping was performed");

      return;
    }

    // James said I should do this
    Undo.IncrementCurrentGroup();
    // Create a name for the undo group (what shows up in the Edit menu)
    Undo.SetCurrentGroupName("Align Selected to Grid");

    // For each object to be snapped...
    foreach (var obj in relevantObjects)
    {
      var pos = obj.transform.position;

      // On each axis, if snapping can occur, perform the snap algorithm
      if (snapX != 0)
      {
        pos.x /= snapX;
        pos.x = Mathf.Round(pos.x);
        pos.x *= snapX;
      }
      if (snapY != 0)
      {
        pos.y /= snapY;
        pos.y = Mathf.Round(pos.y);
        pos.y *= snapY;
      }
      if (snapZ != 0)
      {
        pos.z /= snapZ;
        pos.z = Mathf.Round(pos.z);
        pos.z *= snapZ;
      }

      // Record the state of the object's transform (not the object
      // itself -- thanks again to James), and then modify its position
      Undo.RecordObject(obj.transform, "Align Object to Grid");
      obj.transform.position = pos;
    }

    // Collapse the current Undo group so that
    // it can all be undone / redone at once
    Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
  }
}

#endif
