using UnityEngine;
using UnityEngine.UI;

public class JoystickRotationCounter : MonoBehaviour
{
    private float lastAngle = 0f;
    private int rotationCount = 0;
    private float totalRotation = 0f;

    [SerializeField] private FloatingJoystick joystick;

    [SerializeField] private Image juonImage, satokoImage, jellImage, ampImage;

    private bool rightFlag = false;

    private FullMaxFlag fullMaxFlagObj;

    private void Start()
    {
        rightFlag = false;
        fullMaxFlagObj = GameObject.FindGameObjectWithTag("FullMaxFlag").GetComponent<FullMaxFlag>();
    }

    // Update is called once per frame
    void Update()
    {
        HiddenCommand();

        // F‚ð“øF‚É‚·‚é
        RainbowImage();
    }

    private void RainbowImage()
    {
        if (!fullMaxFlagObj.FullMax) return;

        float hue = (Time.time * 1.0f) % 1.0f;
        Color raindowColor = Color.HSVToRGB(hue, 1.0f, 1.0f);

        juonImage.color = raindowColor;
        satokoImage.color = raindowColor;
        jellImage.color = raindowColor;
        ampImage.color = raindowColor;
    }

    private void HiddenCommand()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if (x == 0 && y == 0) return;

        float currentAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);

        totalRotation += deltaAngle;

        if (Mathf.Abs(totalRotation) >= 360f)
        {
            rotationCount += (int)(totalRotation / 360f);
            totalRotation %= 360f;
        }

        if(!rightFlag && rotationCount >= 1)
        {
            rotationCount = 0;
        }
        else if (rotationCount <= -3 || (rightFlag && rotationCount <= -1))
        {
            rightFlag = true;
            rotationCount = 0;
        }
        else if (rightFlag && rotationCount >= 1)
        {
            fullMaxFlagObj.FullMax = true;
        }

        lastAngle = currentAngle;
    }

}
