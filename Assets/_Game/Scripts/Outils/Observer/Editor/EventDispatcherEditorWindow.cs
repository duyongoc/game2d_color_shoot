using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
public class EventDispatcherEditorWindow : EditorWindow
{

    protected EventDispatcher myScript;

    private GUIStyle menuButton;
    private GUIStyle selectedMenuButton;
    private int selectedMenuIndex;
    private Vector2 menuScrollPosition;



    [MenuItem("Window/Open Event Dispatcher", false, 600)]
    public static void ShowWindow()
    {
        GetWindow<EventDispatcherEditorWindow>();
    }


    private void OnGUI()
    {
        myScript = FindObjectOfType<EventDispatcher>();
        if (!myScript)
        {
            GUILayout.Label("Please run the game to check Event Dispatcher!");
            return;
        }

        InitStyle();
        UpdateEditor();
    }


    private void UpdateEditor()
    {
        var listAction = myScript._listeners.Select(kvp => kvp.Key).ToList();
        List<string> stringList = listAction.ConvertAll(f => f.ToString());
        stringList.Sort();


        GUILayout.BeginArea(new Rect(0, 0, 300, position.height), string.Empty, "box");
        {
            menuScrollPosition = GUILayout.BeginScrollView(menuScrollPosition);
            GUILayout.BeginVertical();
            {
                foreach (KeyValuePair<EventID, Action<object>> kvp in myScript._listeners)
                {
                    int callbackLenght = 0;
                    if (kvp.Value != null)
                    {
                        callbackLenght = kvp.Value.GetInvocationList().Length;
                    }

                    string btnString = $"{kvp.Key.ToString()}: {callbackLenght}";
                    var modeStyle = (int)kvp.Key == selectedMenuIndex ? selectedMenuButton : menuButton;
                    if (GUILayout.Button(btnString, modeStyle, GUILayout.Height(32)))
                    {
                        selectedMenuIndex = (int)kvp.Key;
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();


        GUILayout.BeginArea(new Rect(300, 0, position.width - 300, position.height), string.Empty);
        {
            Draw(position.width - 120, position.height);
        }
        GUILayout.EndArea();
    }


    private void Draw(float width, float height)
    {
        GUILayout.BeginArea(new Rect(0, 0, 700, height), string.Empty);
        {
            foreach (KeyValuePair<EventID, Action<object>> kvp in myScript._listeners)
            {

                if ((int)kvp.Key == selectedMenuIndex)
                {
                    int callbackLenght = 0;
                    if (kvp.Value != null)
                    {
                        callbackLenght = kvp.Value.GetInvocationList().Length;
                    }

                    GUILayout.Label($"Key: {kvp.Key}  -  Callback count: {callbackLenght}");
                    kvp.Value?.GetInvocationList().ToList().ForEach(x =>
                    {
                        GUILayout.Label($"==> Method: {x?.Method.Name}  |  Target: {x?.Target} ");
                        // GUILayout.Label($"Key: {kvp.Key} | Method: {x?.Method.Name} | Target: {x?.Target} ");
                    });
                }
            }
        }
        GUILayout.EndArea();
    }


    private void InitStyle()
    {
        menuButton = new GUIStyle(EditorStyles.label);
        menuButton.fontSize = 10;
        menuButton.alignment = TextAnchor.MiddleLeft;

        var background = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        background.SetPixel(0, 0, EditorGUIUtility.isProSkin ? new Color(0.243f, 0.373f, 0.588f) : new Color(0.247f, 0.494f, 0.871f));
        background.Apply();

        selectedMenuButton = new GUIStyle(EditorStyles.label);
        selectedMenuButton.fontSize = 10;
        selectedMenuButton.alignment = TextAnchor.MiddleLeft;
        selectedMenuButton.active.background = selectedMenuButton.normal.background = background;
    }


}
#endif