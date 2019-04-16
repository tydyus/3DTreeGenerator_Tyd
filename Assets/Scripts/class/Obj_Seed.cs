using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed  {

    public int Old { get; set; } /*              Age                                                         */
    public int Old__Branch_Secondaire { get; set; }
    public int Maturate { get; set; } /*         Age/noeud taille max                                        */
    public float Size_Base { get; set; } /*      taille tronc                                                */
    public float Size_Branch { get; set; } /*    taille branche                                              */
    public float Angle_Branch { get; set; }
    public float Angle_Branch_Secondaire { get; set; }
    public int First_Branch { get; set; } /*              Age                                                         */
    public int MaxNbr_Branch_Fondamental { get; set; }
    public int MaxRank_Branch_Fondamental { get; set; }
    public int Frequence_Branch { get; set; }
    public int Frequence_Branch_Secondaire { get; set; }
    public int Nbr_Branch;
    public int Nbr_Branch_Fondamental;
    public AnimationCurve Density_Leaf = new AnimationCurve(new Keyframe(0, 1));
    public int Frequence_Leaf { get; set; }
    public float Size_Leaf { get; set; }


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
        float angle_Branch = 10, int first_branch = 4, int frequence_Branch = 80,
        int frequence_Branch_Secondaire = 30, int old_secondaire = 3,
        int maxnbr_Branch_Fondamental = 3, int maxRank_Branch_Fondamental = 2, float angle_Branch_f = 10,
        int frequence_Leaf = 50, float size_leaf = 1)
    {
        Nbr_Branch_Fondamental = 1;
        Nbr_Branch = 1;
        Old = old;
        Maturate = maturate;
        Size_Base = size_Base;
        Size_Branch = size_Branch;
        Angle_Branch = angle_Branch;
        Angle_Branch_Secondaire = angle_Branch_f;
        First_Branch = first_branch;
        Frequence_Branch = frequence_Branch;
        Frequence_Branch_Secondaire = frequence_Branch_Secondaire;
        Old__Branch_Secondaire = old_secondaire;
        MaxNbr_Branch_Fondamental = maxnbr_Branch_Fondamental;
        MaxRank_Branch_Fondamental = maxRank_Branch_Fondamental;
        Frequence_Leaf = frequence_Leaf;
        Size_Leaf = size_leaf;

    }
   

}

