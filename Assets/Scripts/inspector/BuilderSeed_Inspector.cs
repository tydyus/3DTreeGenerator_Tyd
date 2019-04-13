using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuilderSeed))]
public class BuilderSeed_Inspector : Editor {


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BuilderSeed myScript = (BuilderSeed)target;
        if (GUILayout.Button("Generate Seed"))
        {
            myScript.GenSeed();
        }
    }


}
