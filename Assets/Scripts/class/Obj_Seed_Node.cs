using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_Node
{


    public int Old { get; set; } /* age */
    public Vector3 Center { get; set; } /* centre */
    public Vector3 Angle { get; set; } /* courbure de la tige*/
    public int Branch { get; set; } /* embranchement */
    public bool is_newBranch = false;
    public bool is_secondaire = false;
    public int Branch_rank = 0;
    public int BornOld_Branch = 0;
    public int newBranch_id { get; set; } /* id new embranchement */
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
