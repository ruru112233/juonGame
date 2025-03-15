using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurnWithMask : MonoBehaviour
{
    public RectTransform pageFront, pageBack, bookBase, maskObject;
    private bool isFlipping = false;
    private float flipSpeed = 5.0f;
    private float targetAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        if (isFlipping)
        {
            float angle = Mathf.LerpAngle(pageFront.localEulerAngles.y, targetAngle, Time.deltaTime * flipSpeed);
            pageFront.localEulerAngles = new Vector3(0, angle, 0);
            pageBack.localEulerAngles = new Vector3(0, angle - 180, 0);

            maskObject.sizeDelta = new Vector2(Mathf.Abs(180 - angle) * 2, maskObject.sizeDelta.y);

            if (Mathf.Abs(pageFront.localEulerAngles.y - targetAngle) < 1f)
            {
                isFlipping = false;
                bookBase.gameObject.SetActive(true);
                pageFront.gameObject.SetActive(false);
            }
        }
    }

    public void FlipPage()
    {
        if (!isFlipping)
        {
            isFlipping = true;
            targetAngle = (targetAngle == 0f) ? 180f : 0f;
        }
    }
}
