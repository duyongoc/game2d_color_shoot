using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Pools;


#if UNITY_EDITOR
public class ObjectPoolingEditorWindow : EditorWindow
{

    private GUIStyle menuButton;
    private GUIStyle selectedMenuButton;
    private string selectedMenuIndex;
    private Vector2 menuScrollPosition;



    [MenuItem("Window/Open Object Pooling Tracking", false, 600)]
    public static void ShowWindow()
    {
        GetWindow<ObjectPoolingEditorWindow>();
    }


    private void OnGUI()
    {
        if (!Application.isPlaying)
        {
            GUILayout.Label("Please run the game to check Object Pooling Tracking!");
            return;
        }

        InitStyle();
        UpdateEditor();
    }


    private void UpdateEditor()
    {
        var poolPrefab = PoolHelper.GetInstancePrefab();
        GUILayout.BeginArea(new Rect(0, 0, 300, position.height), string.Empty, "box");
        {
            menuScrollPosition = GUILayout.BeginScrollView(menuScrollPosition);
            GUILayout.BeginVertical();
            {
                foreach (KeyValuePair<GameObject, Pool> kvp in poolPrefab)
                {
                    // int callbackLenght = 0;
                    // if (kvp.Value != null)
                    //     callbackLenght = kvp.Value.ToString();

                    string btnString = $"{kvp.Key.name.ToString()} | {kvp.Value.ToString()}";
                    var modeStyle = kvp.Key.name == selectedMenuIndex ? selectedMenuButton : menuButton;
                    if (GUILayout.Button(btnString, menuButton, GUILayout.Height(32)))
                    {
                        selectedMenuIndex = kvp.Key.name;
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
            var poolLookup = PoolHelper.GetInstanceLookup();
            // var lookup = poolLookup.Select(kvp => kvp.Key).ToList();
            // List<string> stringList = lookup.ConvertAll(f => f.ToString());
            // stringList.Sort();

            foreach (KeyValuePair<GameObject, Pool> kvp in poolLookup)
            {
                GUILayout.Label($"Key: {kvp.Key.name.ToString()} | {kvp.Value.ToString()}");

                // GUILayout.Label($"Key: {kvp.Key}  -  Callback count: {callbackLenght}");
                // kvp.Value?.GetInvocationList().ToList().ForEach(x =>
                // {
                //     GUILayout.Label($"==> Method: {x?.Method.Name}  |  Target: {x?.Target} ");
                // });
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