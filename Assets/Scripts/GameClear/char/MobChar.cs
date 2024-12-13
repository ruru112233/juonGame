using System;
using System.Collections.Generic;
using UnityEngine;

public class MobChar : MonoBehaviour
{
    private const float CHENGE_TIME = 3.0f;
    private float stateTime;

    [SerializeField] protected bool randomVectolFlag, // ‘S•ûŒü‚ÖŒü‚©‚¹‚é
                                    upMoveFlag,
                                    downMoveFlag,
                                    rightMoveFlag,
                                    leftMoveFlag;

    protected Animator anime;

    // Start is called before the first frame update
    public virtual void Start()
    {
        anime = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected bool IsStateChenge()
    {
        stateTime += Time.deltaTime;

        if (CHENGE_TIME <= stateTime)
        {
            stateTime = 0;
            return true;
        }

        return false;
    }
    protected void RandomState()
    {
        int moveStateCount = Enum.GetNames(typeof(EnumData.MoveState)).Length;
        int rndState = UnityEngine.Random.Range(0, moveStateCount);

        EnumData.MoveState moveState = (EnumData.MoveState)Enum.ToObject(typeof(EnumData.MoveState), rndState);

        MovePatternChenge(moveState);
    }

    protected void MovePatternChenge(EnumData.MoveState state)
    {
        anime.SetInteger("work_state", (int)state);
    }

}
