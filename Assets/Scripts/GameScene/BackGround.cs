using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    const float CHENGE_POS = -12.0f;
    const float START_POS = 12.0f;

    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        if (this.transform.position.y <= CHENGE_POS)
        {
            this.transform.position = new Vector3(this.transform.position.x, START_POS, this.transform.position.z);
        }
    }
}
