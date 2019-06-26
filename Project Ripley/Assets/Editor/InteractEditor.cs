using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
//[CustomEditor(typeof(Interact))]
//[ExecuteInEditMode]
public class InteractEditor : Editor
{
    List<InteractKeyCodes> iInfos = new List<InteractKeyCodes>();
    public List<string> listNames = new List<string>();
    public List<string> listNamesForName = new List<string>();
    public List<int> listCount = new List<int>();
    int numberOfLists;
    int listNumberOfName;
    public int currentIndex = 0;
    public int currentIndexForName = 0;

    private void OnEnable()
    {
       // EditorApplication.update += Update;
    }

    void Update()
    {
        if (numberOfLists == 0)
        {
            numberOfLists = 1;
            currentIndex = listNames.Count - 1;
        }

        if (listNames.Count != numberOfLists)
        {
            if (listNames.Count > numberOfLists)
            {
                listNames.RemoveAt(currentIndex);
                iInfos.RemoveAt(currentIndex);
                currentIndex = listNames.Count - 1;
            }
            else if (listNames.Count < numberOfLists)
            {
                listNames.Add("Interactable Key " + (listNames.Count + 1));
                iInfos.Add(new InteractKeyCodes());
                iInfos[currentIndex].iInfos.Add(new InteractInfo());
                currentIndex = listNames.Count - 1;
            }
        }

        for(int i = 0; i < listNames.Count; i++)
        {
            if (listNames[i][listNames[i].Length - 1] != i.ToString()[i.ToString().Length - 1])
            {
                listNames[i] = "Interactable Key " + (i + 1);
            }
        }


        if (listNumberOfName == 0)
        {
            listNumberOfName = 1;
            currentIndexForName = listNamesForName.Count - 1;
        }

        //if(iInfos[currentIndex].iInfos.Count == 0)
        //{
        //    iInfos[currentIndex].iInfos.Add(new InteractInfo());
        //    listNumberOfName = 1;
        //    currentIndexForName = listNamesForName.Count - 1;
        //}

        if (listNamesForName.Count != listNumberOfName)
        {
            if (listNamesForName.Count > listNumberOfName)
            {
                listNamesForName.RemoveAt(currentIndexForName);
                iInfos[currentIndex].iInfos.RemoveAt(currentIndexForName);
                currentIndexForName = listNamesForName.Count - 1;
            }
            else if (listNamesForName.Count < listNumberOfName)
            {
                listNamesForName.Add("Interactable Data " + (listNamesForName.Count + 1));
                iInfos[currentIndex].iInfos.Add(new InteractInfo());
                currentIndexForName = listNamesForName.Count - 1;
            }
        }

        if(currentIndexForName > iInfos[currentIndex].iInfos.Count)
        {
            currentIndexForName = iInfos[currentIndex].iInfos.Count;
        }
    }

    public override void OnInspectorGUI()
    {
        //base.DrawDefaultInspector();


        GUILayout.BeginHorizontal();
        numberOfLists = EditorGUILayout.IntField("Number Of lists", numberOfLists);

        if (GUILayout.Button("Add"))
        {
            numberOfLists += 1;
        }

        if (GUILayout.Button("Remove"))
        {
            if (numberOfLists <= 1)
            {
                numberOfLists = 1;
                iInfos.RemoveAt(0);
                iInfos.Add(new InteractKeyCodes());
            }
            else
            {
                numberOfLists -= 1;
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        currentIndex = EditorGUILayout.Popup(currentIndex, listNames.ToArray());
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        iInfos[currentIndex].key = (KeyCode)EditorGUILayout.EnumPopup(iInfos[currentIndex].key);

        if (GUILayout.Button("Add"))
        {
            //listNumberOfName += 1;
            listCount.Add(0);
        }

        if (GUILayout.Button("Remove"))
        {
            if (listNumberOfName <= 1)
            {
                listNumberOfName = 1;
                iInfos[currentIndex].iInfos.RemoveAt(0);
                iInfos[currentIndex].iInfos.Add(new InteractInfo());
            }
            else
            {
                listNumberOfName -= 1;
            }

            if (listCount.Count <= 1)
            {
                //listNumberOfName = 1;
                iInfos[currentIndex].iInfos.RemoveAt(0);
                iInfos[currentIndex].iInfos.Add(new InteractInfo());
            }
            else
            {
                listCount.RemoveAt(currentIndexForName);
            }
        }
        GUILayout.EndHorizontal();

        listNumberOfName = EditorGUILayout.IntField("Number Of lists", listNumberOfName);

        //if (currentIndexForName > iInfos[currentIndex].iInfos.Count - 1)
        //{
        //    currentIndexForName = 0;
        //}



        if (iInfos[currentIndex].iInfos.Count >= 1 && currentIndexForName < iInfos[currentIndex].iInfos.Count)
        {
            GUILayout.BeginHorizontal();
            currentIndexForName = EditorGUILayout.Popup(currentIndexForName, listNamesForName.ToArray());
            GUILayout.EndHorizontal();

            iInfos[currentIndex].iInfos[currentIndexForName].name = EditorGUILayout.TextField("Name", iInfos[currentIndex].iInfos[currentIndexForName].name);
            iInfos[currentIndex].iInfos[currentIndexForName].tag = EditorGUILayout.TagField("Tag", iInfos[currentIndex].iInfos[currentIndexForName].tag);
        }
    }
}