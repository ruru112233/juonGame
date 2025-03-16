using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blinking : MonoBehaviour
{
    public float fadeDuration = 1.0f; // フェードイン・アウトの時間
    public float visibleDuration = 0.5f; // 完全に表示される時間
    public float invisibleDuration = 0.5f; // 完全に消えている時間
    private Image image;
    private TextMeshProUGUI text;
    private Color originalColor;

    public bool isColorChenge;

    private int fadeCounter = 0;

    private void OnEnable()
    {
        image = GetComponent<Image>(); // Imageコンポーネントを取得
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
            yield return StartCoroutine(Fade(1, 0)); // フェードアウト
            fadeCounter++;
            yield return new WaitForSeconds(invisibleDuration); // 消えた状態をキープ
            yield return StartCoroutine(Fade(0, 1)); // フェードイン
            yield return new WaitForSeconds(visibleDuration); // 表示状態をキープ
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
