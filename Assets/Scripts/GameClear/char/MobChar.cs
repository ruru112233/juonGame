using System;
using System.Collections.Generic;
using UnityEngine;

public class MobChar : MonoBehaviour
{
    private const float CHENGE_TIME = 3.0f;
    private float stateTime;

    protected enum MoveState
    {
        FRONT,
        RIGHT,
        LEFT,
        BACK,
    }

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
        int moveStateCount = Enum.GetNames(typeof(MoveState)).Length;
        int rndState = UnityEngine.Random.Range(0, moveStateCount);

        MoveState moveState = (MoveState)Enum.ToObject(typeof(MoveState), rndState);

        MovePatternChenge(moveState);
    }

    protected void MovePatternChenge(MoveState state)
    {
        Debug.Log(state);

        anime.SetInteger("work_state", (int)state);
    }
}
