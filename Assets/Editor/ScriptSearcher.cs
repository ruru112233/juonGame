using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptSearcher : EditorWindow
{
    private string searchScriptName = "";
    private Vector2 scrollPosition;
    private GameObject[] foundObjects;

    [MenuItem("Tools/Script Searcher")]
    public static void ShowWindow()
    {
        GetWindow<ScriptSearcher>("Sctipt Searcher");
    }

    private void OnGUI()
    {
        GUILayout.Label("Search for a script in the hieraruchy", EditorStyles.boldLabel);
        searchScriptName = EditorGUILayout.TextField("Script Name", searchScriptName);

        if (GUILayout.Button("Search"))
        {
            SearchForScript();
        }

        GUILayout.Space(10);

        if (foundObjects != null && foundObjects.Length > 0)
        {
            GUILayout.Label($"Found {foundObjects.Length} objects:", EditorStyles.boldLabel);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));

            foreach (GameObject obj in foundObjects)
            {
                if (GUILayout.Button(obj.name, GUILayout.ExpandWidth(true)))
                {
                    Selection.activeGameObject = obj;
                }
            }
            GUILayout.EndScrollView();
        }
        else if(foundObjects != null)
        {
            GUILayout.Label("No objects found,");
        }
    }

    void SearchForScript()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> results = new List<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach(Component component in components)
            {
                if (component.GetType().Name == searchScriptName)
                {
                    results.Add(obj);
                }
            }
        }
        foundObjects = results.ToArray();
    }


}
