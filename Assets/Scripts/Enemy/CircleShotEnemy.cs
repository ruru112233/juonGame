using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShotEnemy : EnemyMove
{
    // Update is called once per frame
    public override void Update()
    {
        Debug.Log("CircleShotEnemy ");
        base.Update();
        base.MoveDirection(MoveDirectionType.BOTTOM_RIGHT, 2.0f); 
    }
}
