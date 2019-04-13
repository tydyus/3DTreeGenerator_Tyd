using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderTree : MonoBehaviour {

public Seed seed;

    //DEV
    [SerializeField]
    private bool is_debug = false;
    [SerializeField]
    private bool DontCleanScript = false;

    //parameters of growning
    private int time = -1;
    GameObject tree;
    [SerializeField]
    private float speed = 1;

    public void GenTree()
    {
       
        if (seed != null)
        {
            tree = new GameObject("tree");
            tree.transform.SetParent(transform);
            Seed_Node first_node = new Seed_Node(new Vector3(0,0,0));
            foreach (Seed_Node node in seed.nodes) if (node.Old == 0) first_node = node;
            GenBranch(first_node, seed, 1);
        }
        else if (is_debug) Debug.Log("generate the seed for generate the tree");
	}

    public void GenBranch(Seed_Node parent, Seed seed, int id_branch)
    {
        GameObject branch = new GameObject("branch");
        branch.transform.SetParent(tree.transform);
        branch.AddComponent<GenBranch>();
        //set list node
        List<Seed_Node> nodes = new List<Seed_Node>();
        foreach (Seed_Node node in seed.nodes) if (node.Branch == id_branch) nodes.Add(node);
        if (is_debug)  foreach (Seed_Node node in nodes) Debug.Log("add node time "+node.Old);
        //
        if (nodes[0].is_secondaire) branch.GetComponent<GenBranch>().is_Fondamental = false;
        else branch.GetComponent<GenBranch>().is_Fondamental = true;
        if (DontCleanScript) branch.GetComponent<GenBranch>().clean = false;
        branch.GetComponent<GenBranch>().StartBranch(parent.Old, seed.Old, parent, nodes, seed); // start gen
    }

}
