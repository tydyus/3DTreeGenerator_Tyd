using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBranch : MonoBehaviour {

    private Seed_Node NodeOrigine;
    private Seed_Node NextNode;
    private List<Seed_Node> Nodes;
    private Seed seed;
    private AnimationCurve SizeBranch;

    private float speed = 1;
    private int Time;
    private int ActualTime;
    private float UpdateForTime;
    private float grown = 0;

    private int Old;
    private int id_node;

    void Start()
    {
        
    }

    // Use this for initialization
    public void StartBranch (int time, int old, Seed_Node nodeOrigine, List<Seed_Node> nodes, Seed set_seed) {

        //init
        NodeOrigine = nodeOrigine;
        Nodes = nodes;
        seed = set_seed;
        Time = time+1;
        ActualTime = time;
        id_node = 0;

        //set line
        gameObject.AddComponent<LineRenderer>();
        gameObject.GetComponent<LineRenderer>().SetPosition(0,NodeOrigine.Center);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, NodeOrigine.Center);
        gameObject.GetComponent<LineRenderer>().material =
                        Resources.Load("Materials/TreeBase", typeof(Material)) as Material;
        SizeBranch = new AnimationCurve(new Keyframe(0, seed.Size_Base), new Keyframe(1, 0.2f));
        gameObject.GetComponent<LineRenderer>().widthCurve = SizeBranch;
        


        //invoke
        UpdateForTime = (speed / 0.01f)-1;
        InvokeRepeating("UpdateBranch", speed, 0.01f);
        InvokeRepeating("UpdateTime", speed*2, speed);
    }

    void UpdateBranch()
    {
 
        if (ActualTime == Time) //suivi du node actuel
        {
            grown += 1/ UpdateForTime;
            gameObject.GetComponent<LineRenderer>().SetPosition(id_node,
                Vector3.Lerp(gameObject.GetComponent<LineRenderer>().GetPosition(id_node - 1),
                NextNode.Center, grown));
        }
        else //suivi du prochain node
        {
            grown = 0;
            ActualTime++;
            foreach (Seed_Node node in Nodes)
            {
                if (node.Old == ActualTime) NextNode = node; //set le prochain node de la branch
            }
            id_node++;
            gameObject.GetComponent<LineRenderer>().positionCount = id_node+1; //set une nouvelle ligne
            gameObject.GetComponent<LineRenderer>().SetPosition(id_node, 
                gameObject.GetComponent<LineRenderer>().GetPosition(id_node-1)); //set le nouveau point au niveau du precedent


            // embranchement
            if (NextNode.is_newBranch) transform.parent.parent.GetComponent<BuilderTree>().GenBranch(NextNode, seed, NextNode.newBranch_id);
        }
        if (Time > Old & gameObject.GetComponent<LineRenderer>().GetPosition(id_node - 1) == NextNode.Center)
        {
            Destroy(this); // clean script
            CancelInvoke(); // fin de pousse de la branche
        }
    }

    void UpdateTime()
    {
        Time++;
        //if (Time > Old) CancelInvoke("UpdateTime");
    }



}
