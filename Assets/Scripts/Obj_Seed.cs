using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed  {

    public int Old { get; set; } /*              Age                                                         */
    public int Maturate { get; set; } /*         Age/noeud taille max                                        */
    public float Size_Base { get; set; } /*      taille tronc                                                */
    public float Size_Branch { get; set; } /*    taille branche                                              */
    public float Angle_Branch { get; set; }
    public int First_Branch { get; set; } /*              Age                                                         */
    public int Frequence_Branch { get; set; } /*              Age                                                         */

    public List<Seed_Node> nodes = new List<Seed_Node>();

    /// <summary>
    /// Initializes a tree. Old = 10,  Maturate = 5, Size_Base= 1, Size_Branch = 0.5 /> class.
    /// </summary>
    public Seed()
    {
        Old = 10;
        Maturate = 5;
        Size_Base = 1;
        Size_Branch = 0.5f;
    }

    /// <summary>
    /// Initializes a tree. /> class.
    /// </summary>
    public Seed(int old, int maturate, float size_Base, float size_Branch, 
        float angle_Branch = 10, int first_branch = 4, int frequence_node = 50)
    {
        Old = old;
        Maturate = maturate;
        Size_Base = size_Base;
        Size_Branch = size_Branch;
        Angle_Branch = angle_Branch;
        First_Branch = first_branch;
        Frequence_Branch = frequence_node;
    }
   

}

public class Seed_Node 
{

    
    public int Old { get; set; } /* age */
    public Vector3 Center { get; set; } /* centre */
    public Vector3 Angle { get; set; } /* courbure de la tige*/
    public int Branch { get; set; } /* embranchement */
    public bool is_newBranch = false;
    public Seed_Node Parent { get; set; } /* " */

    /// <summary>
    /// Initializes the first node of a tree, angle: courbure of the tree. /> class.
    /// </summary>
    public Seed_Node(Vector3 angle)
    {
        Angle = angle;
        Old = 0;
        Branch = 0;
        is_newBranch = true;
    }

    /// <summary>
    /// Initializes a new node in a tree. /> class.
    /// </summary>
    public Seed_Node(int old, Vector3 center, Vector3 angle, Seed_Node parent, int branch, bool is_new_branch = false)
    {
        Old = old;
        Center = center;
        Angle = angle;
        Parent = parent;
        Branch = branch;
        is_newBranch = is_new_branch;
    }


}