using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    public float fadeDuration = 1.0f; // フェードイン・アウトの時間
    public float visibleDuration = 0.5f; // 完全に表示される時間
    public float invisibleDuration = 0.5f; // 完全に消えている時間
    private Image image;
    private Color originalColor;

    void Start()
    {
        image = GetComponent<Image>(); // Imageコンポーネントを取得
        if (image != null)
        {
            originalColor = image.color;
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0)); // フェードアウト
            yield return new WaitForSeconds(invisibleDuration); // 消えた状態をキープ
            yield return StartCoroutine(Fade(0, 1)); // フェードイン
            yield return new WaitForSeconds(visibleDuration); // 表示状態をキープ
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Color newColor = originalColor;
            newColor.a = alpha;
            image.color = newColor;
            yield return null;
        }
    }
}
