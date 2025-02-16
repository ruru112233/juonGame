using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoshi : MonoBehaviour
{
    bool isfadeOut = false;
    float scale = 0.2f;
    float scaleUpSpeed = 3.7f;
    // Start is called before the first frame update
    void Start()
    {
        MsgManager msgManager = GameObject.FindGameObjectWithTag("MsgManager").GetComponent<MsgManager>();
        if (msgManager) msgManager.IsWaitMessage = false;

        transform.localScale = new Vector3(scale, scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isfadeOut)
        {
            scale -= Time.deltaTime * scaleUpSpeed;
        }
        else
        {
            scale += Time.deltaTime * scaleUpSpeed;

            if (scale > 2.58f)
            {
                isfadeOut = true;
            }
        }

        transform.localScale = new Vector3(scale, scale, 1);

        if (scale < float.Epsilon)
        {
            Destroy(gameObject);
        }
    }
}
