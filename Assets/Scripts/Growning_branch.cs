using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growning_branch : MonoBehaviour {

    Vector3 Pos0;
    Vector3 Pos1;
    float Size;
    int Maturate;
    float Valeur_M = 0;
    int timer2 = 1;
    int Old;
    float Speed;
    float timer = 0;
    
	// Use this for initialization
	public void Start_Branch (Vector3 pos0, Vector3 pos1, float size, int maturate, int old, float speed) {
        Pos0 = pos0;
        Pos1 = pos1;
        Size = size;
        Maturate = maturate;
        Old = old;
        Speed = speed;

        GetComponent<LineRenderer>().SetPosition(0, Pos0);
        GetComponent<LineRenderer>().SetPosition(1, Pos0);
        

        InvokeRepeating("UpdateBranch", 0, 0.01f);
        InvokeRepeating("UpdateSize", 0, 0.01f);
    }
	
	
	void UpdateBranch () {
        timer += Speed*0.01f; 

        GetComponent<LineRenderer>().SetPosition(1, Vector3.Lerp(Pos0,Pos1,timer));

        if (GetComponent<LineRenderer>().GetPosition(1) == Pos1) CancelInvoke("Updatebranch");
    }
    void UpdateSize()
    {
        float old_size = Maturate - Old;
        if (Old >= Maturate || Valeur_M >= 1) Valeur_M = 1;
        else
        {
            old_size = (Speed * 100) * old_size;
            Valeur_M = timer2/old_size;
            timer2++;
        }
        float size = Size * Valeur_M;

        GetComponent<LineRenderer>().startWidth = size;
        if (timer >= 1) GetComponent<LineRenderer>().endWidth = size;
        else GetComponent<LineRenderer>().endWidth = size * timer;



        if (GetComponent<LineRenderer>().endWidth >= Size) CancelInvoke("UpdateSize");
    }
}
