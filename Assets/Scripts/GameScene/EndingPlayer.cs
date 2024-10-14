using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayer : Player
{
    private const float CAMERA_ZOOM_OUT = 12;

    private float moveSpeed = 2.0f;
    private Camera mainCamera = null;

    private Animator anime;
    private Vector3 defaultScale;

    private float x;
    private float y;

    private bool eventStartFlag = false;
    private bool messageFlag = false;
    private bool fastFlag = false;

    [SerializeField] private MsgManager msgManager = null;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        GameManager.crearFlag = true;
        anime = this.GetComponent<Animator>();
        defaultScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (eventStartFlag)
        {
            Debug.Log("Event");
            StartCoroutine(EventStart());

            //msgManager.aaa();
            if (messageFlag)
            {
                msgManager.TextFieldObj.SetActive(true);

                if (fastFlag)
                {
                    StartCoroutine(msgManager.ShowText(msgManager.MessageList));
                    fastFlag = false;
                }

                msgManager.MessageStart();

            }
        }
    }

    private IEnumerator EventStart()
    {
        Transform targetTransform = GameObject.FindGameObjectWithTag("EndingEvent").transform;

        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);

        anime.SetInteger("work_state", 3);

        CameraZoomOut();

        yield return new WaitForSeconds(1.6f);

        messageFlag = true;
        

    }

    private void CameraZoomOut()
    {
        if (CAMERA_ZOOM_OUT >= mainCamera.orthographicSize)
        {
            mainCamera.orthographicSize += Time.deltaTime * 5.0f;
        }
    }

    protected override void PlayerMove()
    {
        base.PlayerMove();

        x = joystick.Horizontal;
        y = joystick.Vertical;
        Vector3 newPosition = this.transform.position;

        HorizontalPosSetting(x, ref newPosition);

        VerticalPosSetting(y, ref newPosition);
    }

    private void LateUpdate()
    {
        PlayerAnimation(x, y);
    }

    protected override void HorizontalPosSetting(float joyconX, ref Vector3 newPosition)
    {
        newPosition.x += joyconX * moveSpeed * Time.deltaTime;
    }

    protected override void VerticalPosSetting(float joyconY, ref Vector3 newPosition)
    {
        newPosition.y += joyconY * moveSpeed * Time.deltaTime;
    }

    private void PlayerAnimation(float x, float y)
    {
        if (0 < y && 0.5f < (y - x))
        {
            // è„
            anime.SetInteger("work_state", 3);
        }
        else if (0 > y && 0.5f > (y - x))
        {
            // â∫
            anime.SetInteger("work_state", 0);
        }
        else if (0 < x)
        {
            // âE
            anime.SetInteger("work_state", 1);
            
        }
        else if (0 > x)
        {
            // ç∂
            anime.SetInteger("work_state", 2);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndingEvent"))
        {
            joystick.gameObject.SetActive(false);
            eventStartFlag = true;
            fastFlag = true;
        }
    }

}
