using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 定数
    /// </summary>

    const float UP_MOVE_LIMIT = 4.8f;
    const float DOWN_MOVE_LIMIT = -4.8f;
    const float RIGHT_MOVE_LIMIT = 2.6f;
    const float LEFT_MOVE_LIMIT = -2.6f;

    /// <summary>
    /// 変数
    /// </summary>

    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        
    }

    void PlayerAction()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        // 上
        if (this.transform.position.y < UP_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
        }

        // 下
        if (this.transform.position.y > DOWN_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }
        }

        // 右
        if (this.transform.position.x < RIGHT_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

        // 左
        if (this.transform.position.x > LEFT_MOVE_LIMIT)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        
    }
}
