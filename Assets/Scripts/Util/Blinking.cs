using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blinking : MonoBehaviour
{
    public float fadeDuration = 1.0f; // �t�F�[�h�C���E�A�E�g�̎���
    public float visibleDuration = 0.5f; // ���S�ɕ\������鎞��
    public float invisibleDuration = 0.5f; // ���S�ɏ����Ă��鎞��
    private Image image;
    private TextMeshProUGUI text;
    private Color originalColor;

    public bool isColorChenge;

    private int fadeCounter = 0;

    private void OnEnable()
    {
        image = GetComponent<Image>(); // Image�R���|�[�l���g���擾
        text = GetComponent<TextMeshProUGUI>();
        if (image != null)
        {
            originalColor = image.color;
            StartCoroutine(BlinkCoroutine());
        }

        if (text != null)
        {
            originalColor = text.color;
            StartCoroutine(BlinkCoroutine());
        }
    }

    private void Update()
    {
        if (fadeCounter >= 2)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0)); // �t�F�[�h�A�E�g
            fadeCounter++;
            yield return new WaitForSeconds(invisibleDuration); // ��������Ԃ��L�[�v
            yield return StartCoroutine(Fade(0, 1)); // �t�F�[�h�C��
            yield return new WaitForSeconds(visibleDuration); // �\����Ԃ��L�[�v
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;
        if (image)
        {
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

                if (!isColorChenge && alpha > 0.05f)
                {
                    alpha = 0.05f;
                }

                Color newColor = originalColor;
                newColor.a = alpha;
                image.color = newColor;
                yield return null;
            }
        }

        if (text)
        {
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
                Color newColor = originalColor;
                newColor.a = alpha;
                text.color = newColor;
                yield return null;
            }
        }
    }
}
