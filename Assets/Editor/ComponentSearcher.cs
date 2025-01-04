using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class ComponentSearcher : EditorWindow
{
    private string searchComponentName = "";
    private Vector2 scrollPosition;
    private GameObject[] foundObjects;

    private string[] searchSelectOptions = { "���S��v", "������v" };
    private string distinctionStr = "�啶������������ʂ���";
    private string foldingStr = "�����I�v�V������܂肽����";
    private string DeploymentStr = "�����I�v�V������W�J����";
    private int selectedIndex = 0;

    private bool isFolded = false; 

    private bool isSelectTypeDistinction = true;

    [MenuItem("Tools/Component Searcher")]
    public static void ShowWindow()
    {
        GetWindow<ComponentSearcher>("Component Searcher");
    }

    private void OnGUI()
    {
        GUILayout.Label("�q�G�����L�[����R���|�[�l���g��T��", EditorStyles.boldLabel);
        searchComponentName = EditorGUILayout.TextField("�R���|�[�l���g��", searchComponentName);

        GUILayout.BeginVertical();
        string foldedMssage = isFolded ? foldingStr : DeploymentStr;
        isFolded = GUILayout.Toggle(isFolded, foldedMssage);

        GUILayout.Space(10);

        if (isFolded)
        {
            GUILayout.Label("�������I�v�V����");

            for (int i = 0; i < searchSelectOptions.Length; i++)
            {
                bool isSelected = (selectedIndex == i);

                if (GUILayout.Toggle(isSelected, searchSelectOptions[i]))
                {
                    selectedIndex = i;
                }
            }

            GUILayout.Space(10);
            GUILayout.Label("�����̑��I�v�V����");
            isSelectTypeDistinction = GUILayout.Toggle(isSelectTypeDistinction, distinctionStr);

        }

        GUILayout.EndVertical();

        GUILayout.Space(10);

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
                if (!obj) continue;
                
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
                switch (selectedIndex)
                {
                    case 0: // ���S��v
                        ExactMatch(component, obj, ref results);
                        break;
                    case 1: // ������v
                        PartialMatch(component, obj, ref results);
                        break;
                    default:
                        break;
                }
            }
        }
        foundObjects = results.ToArray();
    }

    private void ExactMatch(Component component, GameObject obj, ref List<GameObject> results)
    {
        StringComparison compartison = isSelectTypeDistinction ? StringComparison.CurrentCulture : StringComparison.OrdinalIgnoreCase;

        // �啶������������ʂ��Ȃ�
        if (string.Equals(component.GetType().Name, searchComponentName, compartison))
        {
            results.Add(obj);
        }
    }

    private void PartialMatch(Component component, GameObject obj, ref List<GameObject> results)
    {
        RegexOptions options = isSelectTypeDistinction ? RegexOptions.None : RegexOptions.IgnoreCase;

        if (Regex.IsMatch(component.GetType().Name, ".*" + Regex.Escape(searchComponentName) + ".*", options))
        {
            results.Add(obj);
        }
    }


}
