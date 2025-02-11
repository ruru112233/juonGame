using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMove : MonoBehaviour
{
    [SerializeField] private Sprite idle, work;
    Image charImage;

    float changeTime = 0.5f;
    float latestTime = 0;
    bool isWork = false;

    // Start is called before the first frame update
    void Start()
    {
        charImage = GetComponent<Image>();
        charImage.sprite = idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > (latestTime + changeTime))
        {
            latestTime = Time.time;
            charImage.sprite = ChangeSprite();

            if (isWork)
            {
                transform.position += new Vector3(70, 0, 0);
            }

            isWork = !isWork;
        }
    }

    Sprite ChangeSprite()
    {
        if (isWork)
        {
            return work;
        }
        else
        {
            return idle;
        }
    }
}
