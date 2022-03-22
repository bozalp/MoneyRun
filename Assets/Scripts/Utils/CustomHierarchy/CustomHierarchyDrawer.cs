#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[InitializeOnLoad]
public class CustomHierarcyDrawer
{
    static Color selectionColor = Color.white * 0.6f;

    static CustomHierarcyDrawer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOmGUI;
    }

    static void HandleHierarchyWindowItemOmGUI(int inSelectionID, Rect inSelectionRect)
    {
        EditorApplication.RepaintHierarchyWindow();

        GameObject obj = EditorUtility.InstanceIDToObject(inSelectionID) as GameObject;

        if (obj != null)
        {
            var customItems = obj.GetComponents<CustomHierarchyItem>();

            if (customItems != null && customItems.Length > 0)
            {
                bool isSelected = Selection.instanceIDs.Contains(inSelectionID);

                foreach (var item in customItems)
                {
                    paintObject(item, inSelectionRect);
                }

                drawSelection(inSelectionRect, isSelected);
            }
        }

        EditorApplication.RepaintHierarchyWindow();
    }

    static void drawSelection(Rect inSelectionRect, bool isSelected)
    {
        if (isSelected)
        {
            Rect backgroundRect = inSelectionRect;
            backgroundRect.x = 0;
            backgroundRect.xMax *= 1.5f;

            //EditorGUI.DrawRect(BackgroundOffset, bgCol * 1.5f);
            //EditorGUI.DrawRect(BackgroundOffset, Color.white * 0.5f);
            EditorGUI.DrawRect(backgroundRect, selectionColor);
        }
    }

    static void paintObject(CustomHierarchyItem label, Rect inSelectionRect)
    {

        if (label != null && Event.current.type == EventType.Repaint)
        {
            #region Determine Styling

            Color bgCol = label.BackgroundColor;

            #endregion

            //Draw selected background color on to hierachy object
            #region Draw Background
            //Only draw background if background color is not completely transparent
            if (bgCol.a > 0f)
            {
                Rect backgroundRect = inSelectionRect;
                backgroundRect.x = 0;
                backgroundRect.xMax *= 1.5f;

                EditorGUI.DrawRect(backgroundRect, bgCol);
            }


            #endregion


         

            EditorApplication.RepaintHierarchyWindow();
        }
    }


}

#endif