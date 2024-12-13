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
        // ‘S•ûŒü‚Éƒ‰ƒ“ƒ_ƒ€‚ÅŒü‚©‚¹‚é
        if (randomVectolFlag)
        {
            if (IsStateChenge())
            {
                RandomState();
            }
        }
        else if (upMoveFlag)
        {
            MovePatternChenge(EnumData.MoveState.BACK);
        }
        else if (downMoveFlag)
        {
            MovePatternChenge(EnumData.MoveState.FRONT);
        }
        else if (rightMoveFlag)
        {
            MovePatternChenge(EnumData.MoveState.RIGHT);
        }
        else if (leftMoveFlag)
        {
            MovePatternChenge(EnumData.MoveState.LEFT);
        }




    }
   

    

}
