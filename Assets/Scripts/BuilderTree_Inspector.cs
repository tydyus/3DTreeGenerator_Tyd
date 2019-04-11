using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuilderTree))]
public class BuilderTree_Inspector : Editor
{


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BuilderTree myScript = (BuilderTree)target;
        if (GUILayout.Button("Generate Tree"))
        {
            myScript.GenTree();
        }
    }
}