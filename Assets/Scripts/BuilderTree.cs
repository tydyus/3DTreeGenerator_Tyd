using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderTree : MonoBehaviour {

public Seed seed;

    //DEV
    [SerializeField]
    private bool is_debug = false;

    //parameters of growning
    private int time = -1;
    GameObject tree;
    [SerializeField]
    private float speed = 1;

    public void GenTree()
    {
       
        if (seed != null)
        {
            time = 0;
            tree = new GameObject("tree");
            InvokeRepeating("UpdateTree", 0, speed);
        }
        else if (is_debug) Debug.Log("generate the seed for generate the tree");
	}

    void UpdateTree()
    {
        if (is_debug) Debug.Log("generate Tree BEGIN");
        if (time > 0)
        {
            foreach (Seed_Node node in seed.nodes)
            {
                if (is_debug) Debug.Log("test");
                if (node.Old == time )
                {
                    if (is_debug) Debug.Log("Create Branch BEGIN");
                    GameObject branch = new GameObject("branch");
                    branch.transform.SetParent(tree.transform);
                    branch.AddComponent<LineRenderer>();
                    //branch.GetComponent<LineRenderer>().numCapVertices = 10;
                    branch.GetComponent<LineRenderer>().material =
                        Resources.Load("Materials/TreeBase", typeof(Material)) as Material;
                    branch.AddComponent<Growning_branch>();
                    branch.GetComponent<Growning_branch>().Start_Branch(
                        node.Parent.Center, node.Center, seed.Size_Base, seed.Maturate, node.Old, speed);
                    if (is_debug) Debug.Log("Create Branch");

                }

            }
        }

        if (time < seed.Old)
        {
            if (is_debug) Debug.Log("genTree step " + time);
            time++;
        }
        else
        {
            if (is_debug) Debug.Log("ENDING gentree");
            CancelInvoke();
        }
        

    }
	
}
