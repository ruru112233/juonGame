using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// íËêî
    /// </summary>

    protected const float UP_BULLET_LIMIT = 5.0f;
    protected const float DOWN_BULLET_LIMIT = -5.0f;
    protected const float LEFT_BULLET_LIMIT = -2.9f;
    protected const float RIGHT_BULLET_LIMIT = 2.9f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (this.transform.position.y >= UP_BULLET_LIMIT ||
            this.transform.position.y <= DOWN_BULLET_LIMIT ||
            this.transform.position.x <= LEFT_BULLET_LIMIT ||
            this.transform.position.x >= RIGHT_BULLET_LIMIT)
        {
            this.gameObject.SetActive(false);
        }
    }
}
