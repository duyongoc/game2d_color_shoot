using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(EventDispatcher))]
public class EventDispatcherEditor : Editor
{

    protected bool showMoreDebug = true;
    protected EventDispatcher myScript;
    protected static GUIStyle cheatStyle;



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DefineStyle();
        DrawCustomDebug();
    }


    private static void DefineStyle()
    {
        cheatStyle = new GUIStyle("Foldout");
        cheatStyle.fontStyle = FontStyle.Bold;
        cheatStyle.normal.textColor = Color.red;
    }


    private void DrawCustomDebug()
    {
        GUILayout.Space(20);
        myScript = target as EventDispatcher;
        showMoreDebug = EditorGUILayout.Foldout(showMoreDebug, "Show Debug Seting", cheatStyle);

        if (showMoreDebug)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Show Debug"))
            {
                myScript.ShowDebug();
            }
            if (GUILayout.Button("Show Callback Counting"))
            {
                myScript.ShowCallbackCounting();
            }
            GUILayout.EndHorizontal();
        }
    }

}
#endif