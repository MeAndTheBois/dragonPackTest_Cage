/***************************************************
File:           LPK_ScaleSnappingSize.cs
Authors:        Christopher Onorati
Last Updated:   3/18/2019
Last Version:   2018.3.4

Description:
  This script contains two editor classes to scale up and down
  the snapping size of translation via hotkeys.  Min size of 0.25 and
  max size of 512.

Copyright 2018-2019, DigiPen Institute of Technology
***************************************************/

using UnityEngine;
using UnityEditor;
using System.Reflection;    /* Access to user prefs to manually reload the snap settings window. */
using System;               /* Access to type Type. */
using System.Linq;          /* Access to manual window reloading. */

#if UNITY_EDITOR

/**
* CLASS NAME  : LPK_DecreaseSnappingSize
* DESCRIPTION : Decrease the snapping size of movement in the editor.
**/
public class LPK_DecreaseSnappingSize : EditorWindow
{
    const float m_flMinSize = 1.0f;

    /**
    * FUNCTION NAME: DecreaseSnapSize
    * DESCRIPTION  : Decrease the snap size of the editor via power of 2.
    * INPUTS       : None
    * OUTPUTS      : None
    **/  
    [MenuItem("Utilities/Snapping/Decrease Snap Size %[")]
    public static void DecreaseSnapSize()
    {
        float snapX = EditorPrefs.GetFloat("MoveSnapX", 1.0f);
        float snapY = EditorPrefs.GetFloat("MoveSnapY", 1.0f);
        float snapZ = EditorPrefs.GetFloat("MoveSnapZ", 1.0f);

        float largest = Mathf.Max(snapX, snapY, snapZ);

        //Clamp.
        if (largest <= m_flMinSize)
        {
            EditorPrefs.SetFloat("MoveSnapX", m_flMinSize);
            EditorPrefs.SetFloat("MoveSnapY", m_flMinSize);
            EditorPrefs.SetFloat("MoveSnapZ", m_flMinSize);

            ReloadSnapSettings();  
            DrawNotification(m_flMinSize);     
        }

        else
        {
            //Set all snapping to the same power of 2 size.
            if (Mathf.IsPowerOfTwo((int)largest) && (snapX != largest || snapY != largest || snapZ != largest))
            {
                EditorPrefs.SetFloat("MoveSnapX", largest);
                EditorPrefs.SetFloat("MoveSnapY", largest);
                EditorPrefs.SetFloat("MoveSnapZ", largest);

                ReloadSnapSettings();  
                DrawNotification(largest);     
            }

            //Scale down.
            else
            {
                largest = Mathf.NextPowerOfTwo( ( ( (int)largest - 1) * 2 ) / 4);

                EditorPrefs.SetFloat("MoveSnapX", largest);
                EditorPrefs.SetFloat("MoveSnapY", largest);
                EditorPrefs.SetFloat("MoveSnapZ", largest); 
                
                ReloadSnapSettings();   
                DrawNotification(largest);    
            }
        }
    }

    /**
    * FUNCTION NAME: ReloadSnapSettings
    * DESCRIPTION  : Reload the snap settings for immediate application.
    * INPUTS       : None
    * OUTPUTS      : None
    **/ 
    static void ReloadSnapSettings()
    {
      	// If Unity snap sync is enabled, refresh the Snap Settings window if it's open. 
	    Type snapSettings = typeof(EditorWindow).Assembly.GetType("UnityEditor.SnapSettings");

	    if (snapSettings != null)
	    {
		    FieldInfo snapInitialized = snapSettings.GetField("s_Initialized", BindingFlags.NonPublic | BindingFlags.Static);

		    if (snapInitialized != null)
		    {
			    snapInitialized.SetValue(null, (object)false);

			    EditorWindow win = Resources.FindObjectsOfTypeAll<EditorWindow>().FirstOrDefault(x => x.ToString().Contains("SnapSettings"));

			    if (win != null)
				    win.Repaint();
		    }
	    }         
    }

    /**
    * FUNCTION NAME: DrawNotification
    * DESCRIPTION  : Display confirmation of the new snapping size.
    * INPUTS       : newSize - New snapping size to display.
    * OUTPUTS      : None
    **/ 
    static void DrawNotification(float newSize)
    {
        EditorWindow window = EditorWindow.GetWindow<SceneView>();
        window.ShowNotification(new GUIContent("Snap " + newSize));
    }
}

/**
* CLASS NAME  : LPK_IncreaseSnappingSize
* DESCRIPTION : Increase the snapping size of movement in the editor.
**/
public class LPK_IncreaseSnappingSize : EditorWindow
{
    const float m_flMaxSize = 512.0f;

    /**
    * FUNCTION NAME: IncreaseSnapSize
    * DESCRIPTION  : Increase the snap size of the editor via power of 2.
    * INPUTS       : None
    * OUTPUTS      : None
    **/  
    [MenuItem("Utilities/Snapping/Increase Snap Size %]")]
    public static void IncreaseSnapSize()
    {
        float snapX = EditorPrefs.GetFloat("MoveSnapX", 1.0f);
        float snapY = EditorPrefs.GetFloat("MoveSnapY", 1.0f);
        float snapZ = EditorPrefs.GetFloat("MoveSnapZ", 1.0f);

        float largest = Mathf.Max(snapX, snapY, snapZ);
        
        //Clamp.
        if (largest >= m_flMaxSize)
        {
            EditorPrefs.SetFloat("MoveSnapX", m_flMaxSize);
            EditorPrefs.SetFloat("MoveSnapY", m_flMaxSize);
            EditorPrefs.SetFloat("MoveSnapZ", m_flMaxSize);

            ReloadSnapSettings();
            DrawNotification(m_flMaxSize);
        }
        else
        {
            //Set all snapping to the same power of 2 size.
            if (Mathf.IsPowerOfTwo((int)largest) && (snapX != largest || snapY != largest || snapZ != largest))
            {
                EditorPrefs.SetFloat("MoveSnapX", largest);
                EditorPrefs.SetFloat("MoveSnapY", largest);
                EditorPrefs.SetFloat("MoveSnapZ", largest);

                ReloadSnapSettings();
                DrawNotification(largest);
            }

            //Scale up.
            else
            {
                largest = Mathf.NextPowerOfTwo((int)largest + 1);

                EditorPrefs.SetFloat("MoveSnapX", largest);
                EditorPrefs.SetFloat("MoveSnapY", largest);
                EditorPrefs.SetFloat("MoveSnapZ", largest);

                ReloadSnapSettings();
                DrawNotification(largest);
            }
        }
    }

    /**
    * FUNCTION NAME: ReloadSnapSettings
    * DESCRIPTION  : Reload the snap settings for immediate application.
    * INPUTS       : None
    * OUTPUTS      : None
    **/  
    static void ReloadSnapSettings()
    {
	    Type snapSettings = typeof(EditorWindow).Assembly.GetType("UnityEditor.SnapSettings");

	    if (snapSettings != null)
	    {
		    FieldInfo snapInitialized = snapSettings.GetField("s_Initialized", BindingFlags.NonPublic | BindingFlags.Static);

		    if (snapInitialized != null)
		    {
			    snapInitialized.SetValue(null, (object)false);

			    EditorWindow win = Resources.FindObjectsOfTypeAll<EditorWindow>().FirstOrDefault(x => x.ToString().Contains("SnapSettings"));

			    if (win != null)
				    win.Repaint();
		    }
	    }         
    }

    /**
    * FUNCTION NAME: DrawNotification
    * DESCRIPTION  : Display confirmation of the new snapping size.
    * INPUTS       : newSize - New snapping size to display.
    * OUTPUTS      : None
    **/ 
    static void DrawNotification(float newSize)
    {
        EditorWindow window = EditorWindow.GetWindow<SceneView>();
        window.ShowNotification(new GUIContent("Snap " + newSize) );
    }
}

#endif  //UNITY_EDITOR
