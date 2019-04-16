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
    private float Angle_Branch_Secondaire = 20; //    angle embranchement 
    [SerializeField]
    private int First_Branch = 3; //         
    [SerializeField]
    private int Frequence_Branch = 50; //         %
    [SerializeField]
    private int Frequence_Branch_Secondaire = 50; //
    [SerializeField]
    private int Old_Secondaire = 5; // taille/agemax des branche secondaire
    [SerializeField]
    private int Max_nbr_Fondamental = 5; // nombre max de branche principale
    [SerializeField]
    private int MaxRank_Fondamental = 2; // zones'apparition des branches principales
    [SerializeField]
    private AnimationCurve Density_Leaf = new AnimationCurve(new Keyframe(0, 1));
    [SerializeField]
    private int Frequence_Leaf = 20;
    [SerializeField]
    private float Size_Leaf = 1;

    private Seed seed; // initialise the seed
    int total_branch; // init branch

    //seed generation property
    [SerializeField]
    private int Old = 20;
    [SerializeField]
    private int Distance_nodes = 1;
    [SerializeField]
    private Vector2 courbure = new Vector2(0, 0);
    [SerializeField]
    private Vector3 courbure_Secondaire = new Vector3(0, -0.1f, 0);

    public void GenSeed() // generate the seed
    {
        if (is_debug) Debug.Log("generate seed BEGIN");

        // instanciate the seed
        seed = new Seed(Old, Maturate, Size_Base, Size_Branch, Angle_Branch, First_Branch, Frequence_Branch,
            Frequence_Branch_Secondaire, Old_Secondaire, Max_nbr_Fondamental, MaxRank_Fondamental, Angle_Branch_Secondaire,
            Frequence_Leaf, Size_Leaf);
        seed.Density_Leaf = Density_Leaf;
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
                angle = RotatePointAroundPivot(new Vector3(0, Distance_nodes, 0), new Vector3(0, 0, 0), 
                    new Vector3(courbure.x+courbure_Secondaire.x, courbure_Secondaire.y, courbure.y+ courbure_Secondaire.z));
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
                    if (parent.is_secondaire == false ||
                        time - parent.BornOld_Branch < seed.Old__Branch_Secondaire)
                    {
                        node = gen_node(parent);
                        seed.nodes.Add(node);
                        nodes_parentNew.Add(node);

                        bool is_branch = false;
                        bool is_secondaire;

                        /* determine si embranchement et secondaire ou fondamental*/
                        int frequence = seed.Frequence_Branch;

                        if (seed.Nbr_Branch_Fondamental <= seed.MaxNbr_Branch_Fondamental & parent.Branch_rank < seed.MaxRank_Branch_Fondamental)
                            is_secondaire = false;
                        else is_secondaire = true;

                        if (is_secondaire) frequence = seed.Frequence_Branch_Secondaire;

                        if (time == seed.First_Branch || time > seed.First_Branch & frequence > Random.Range(0,99) ) is_branch = true;
                        /**/

                        if (is_branch)
                        {
                            if (!is_secondaire) seed.Nbr_Branch_Fondamental ++;
                            Debug.Log(seed.Nbr_Branch_Fondamental);
                            seed.Nbr_Branch++;
                        
                            node = gen_node(parent,true, is_secondaire);
                            seed.nodes.Add(node);
                            nodes_parentNew.Add(node);
                        }
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

    public Seed_Node gen_node(Seed_Node parent, bool is_newBranch = false, bool is_secondaire_embranchement = false)
    {

        Seed_Node node;

        Vector3 center = parent.Angle;
        if (is_newBranch)
        {
            
            center = parent.Angle + (parent.Angle-parent.Parent.Center);
            center = RotatePointAroundPivot(center, parent.Parent.Center, 
                new Vector3(-courbure.x, 0, -courbure.y));
            if (is_secondaire_embranchement)
                center = RotatePointAroundPivot(center, parent.Parent.Center,
                    new Vector3(Random.Range(-seed.Angle_Branch_Secondaire, seed.Angle_Branch_Secondaire), 0, 
                    Random.Range(-seed.Angle_Branch_Secondaire, seed.Angle_Branch_Secondaire)));
            else center = RotatePointAroundPivot(center, parent.Parent.Center, 
                new Vector3(Random.Range(-seed.Angle_Branch, seed.Angle_Branch), 0, Random.Range(-seed.Angle_Branch, seed.Angle_Branch)));
        }

        /*Correction emplacement nextNode*/
        float distance = Vector3.Distance(center, parent.Center);
        center = Vector3.Lerp(parent.Center, center, Distance_nodes / distance);
        /**/

        Vector3 angle = center +(center - parent.Center);
        angle = RotatePointAroundPivot(angle, parent.Center, new Vector3(courbure.x, 0, courbure.y));

        if (is_newBranch) parent.is_newBranch = true; //si le début d'une nouvelle branche
            

        int branch = parent.Branch;
        if (parent.is_newBranch)
        {
            branch = total_branch;
            parent.newBranch_id = total_branch;
            total_branch++;

        }



        node = new Seed_Node(parent.Old + 1, center, angle, parent, branch);

        if (parent.is_newBranch)
        {
            node.BornOld_Branch = parent.Old;
            node.Branch_rank = parent.Branch_rank + 1;
            if (is_secondaire_embranchement)
                node.is_secondaire = true;
            else node.is_secondaire = false;
        }
        else
        {
            node.is_secondaire = parent.is_secondaire;
            node.Branch_rank = parent.Branch_rank;
            node.BornOld_Branch = parent.BornOld_Branch;
        }
        

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
