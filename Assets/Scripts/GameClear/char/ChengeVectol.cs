using System;
using UnityEngine;

public class ChengeVectol : MobChar
{
 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStateChenge())
        {
            RandomState();
        }
    }
   

    

}
