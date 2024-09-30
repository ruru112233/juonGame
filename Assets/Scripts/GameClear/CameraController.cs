using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // comment test
    private Vector3 playerPos = new Vector3();

    private GameObject playerObj = null;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("EndingPlayer");

        if (playerObj)
        {
            playerPos = playerObj.transform.position;
        }

        if (null != playerPos) 
        {
            playerPos.z = -10;
            this.transform.position = playerPos;
        } 
    }

    private void LateUpdate()
    {
        if (playerObj)
        {
            playerPos = playerObj.transform.position;
        }

        if (null != playerPos)
        {
            playerPos.z = -10;
            this.transform.position = playerPos;
        }
    }
}
