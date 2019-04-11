using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CleanerChild : MonoBehaviour {

	public void CleanChild() {

        bool cleaning = true;
        while(cleaning)
        {
            if (transform.childCount != 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
            else
            {
                Debug.Log("Child clean");
                cleaning = false;

            }
        }

	}
	
}

[CustomEditor(typeof(CleanerChild))]
public class CleanerChild_Inspector : Editor
{


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CleanerChild myScript = (CleanerChild)target;
        if (GUILayout.Button("Clean child"))
        {
            myScript.CleanChild();
        }
    }


}