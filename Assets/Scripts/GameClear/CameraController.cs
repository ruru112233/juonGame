using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 playerPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("EndingPlayer").transform.position;
        
        if (null != playerPos) 
        {
            playerPos.z = -10;
            this.transform.position = playerPos;
        } 
    }

    private void LateUpdate()
    {
        playerPos = GameObject.FindGameObjectWithTag("EndingPlayer").transform.position;

        if (null != playerPos)
        {
            playerPos.z = -10;
            this.transform.position = playerPos;
        }
    }
}
