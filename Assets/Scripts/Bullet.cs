using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// íËêî
    /// </summary>

    const float UP_BULLET_LIMIT = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y >= UP_BULLET_LIMIT)
        {
            this.gameObject.SetActive(false);
        }
    }
}
