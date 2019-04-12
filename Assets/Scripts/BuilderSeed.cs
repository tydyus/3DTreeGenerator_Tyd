using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderSeed : MonoBehaviour {
    //DEV
    [SerializeField]
    private bool is_generate = false;
    [SerializeField]
    private bool is_debug = false;

    //Seed property
    [SerializeField]
    private int Maturate; //         Age/noeud taille max                                        
    [SerializeField]
    private float Size_Base; //      taille tronc                                                
    [SerializeField]
    private float Size_Branch; //    taille branche  
    [SerializeField]
    private float Angle_Branch = 10; //    angle embranchement 
    [SerializeField]
    private int First_Branch = 3; //         
    [SerializeField]
    private int Frequence_Branch = 50; //         %

    private Seed seed; // initialise the seed
    int total_branch; // init branch

    //seed generation property
    [SerializeField]
    private int Old = 20;
    [SerializeField]
    private int Distance_nodes = 1;
    [SerializeField]
    private Vector2 courbure = new Vector3(0, 0);

    public void GenSeed() // generate the seed
    {
        if (is_debug) Debug.Log("generate seed BEGIN");

        // instanciate the seed
        seed = new Seed(Old, Maturate, Size_Base, Size_Branch, Angle_Branch, First_Branch, Frequence_Branch);   
        int time = 0;   // Set the time of growning
        total_branch = 1;

        /*BEGIN creating the seed*/
        List<Seed_Node> nodes_parent = new List<Seed_Node>(); ; // initiate list of parent's node

        while (time < Old) // generate Old% node in the seed
        {
            Seed_Node node; // initiate the node
            List<Seed_Node> nodes_parentNew = new List<Seed_Node>(); ; //reset parent's node
            

            if (is_debug) Debug.Log("Seed: step "+time);

            /*BEGIN creating the node*/
            if (time == 0) // first node
            {
                //Define angle
                Vector3 angle;
                angle = RotatePointAroundPivot(new Vector3(0, Distance_nodes, 0), new Vector3(0, 0, 0), new Vector3(courbure.x, 0, courbure.y));
                //Create node
                node = new Seed_Node(angle);
                seed.nodes.Add(node);
                nodes_parentNew.Add(node);
                if (is_debug) Debug.Log("Seed: add node in " + node.Center);

            }
            else
            {
                foreach (Seed_Node parent in nodes_parent) //boucle chaque node créé à la boucle préscedente
                {
                    node = gen_node(parent);
                    seed.nodes.Add(node);
                    nodes_parentNew.Add(node);

                    bool is_branch = false;
                    /* determine si embranchement */
                    if (time == seed.First_Branch || time > seed.First_Branch & seed.Frequence_Branch > Random.Range(0,99) ) is_branch = true;
                    /**/
                    if (is_branch)
                    {
                        node = gen_node(parent,true);
                        seed.nodes.Add(node);
                        nodes_parentNew.Add(node);
                    }
                }


            }
            /*ENDING creating the node*/

            nodes_parent = nodes_parentNew; //set parent
            time++;
        }
        /*ENDING creating the seed*/

        GetComponent<BuilderTree>().seed = seed;
        if (is_debug) Debug.Log("add seed to genTree");
        if (is_generate) GetComponent<BuilderTree>().GenTree(); // grown the tree with the seed created
    }

    public Seed_Node gen_node(Seed_Node parent, bool is_newBranch = false)
    {
        Seed_Node node;

        Vector3 center = parent.Angle;
        if (is_newBranch)
        {
            
            center = parent.Angle + (parent.Angle-parent.Parent.Center);
            center = RotatePointAroundPivot(center, parent.Parent.Center, 
                new Vector3(-courbure.x, 0, -courbure.y));
            center = RotatePointAroundPivot(center, parent.Parent.Center, 
                new Vector3(Random.Range(-seed.Angle_Branch, seed.Angle_Branch), 0, Random.Range(-seed.Angle_Branch, seed.Angle_Branch)));
        }

        /*Correction emplacement nextNode*/
        float distance = Vector3.Distance(center, parent.Center);
        center = Vector3.Lerp(parent.Center, center, Distance_nodes / distance);
        /**/

        Vector3 angle = center +(center - parent.Center);
        angle = RotatePointAroundPivot(angle, parent.Center, new Vector3(courbure.x, 0, courbure.y));

        int branch = parent.Branch;
        if (parent.is_newBranch)
        {
            branch = total_branch;
            total_branch++;
        }


        bool newbranch = false;
        if (is_newBranch) newbranch = true; //si le début d'une nouvelle branche


        node = new Seed_Node(parent.Old + 1, center, angle, parent, branch, newbranch);
        if (is_debug) Debug.Log("Seed: add node in " + node.Center);
        return node;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; 
        dir = Quaternion.Euler(angles) * dir; 
        point = dir + pivot; 

        return point; 
    }
}
