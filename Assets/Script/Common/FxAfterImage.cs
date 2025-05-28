using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxAfterImage : MonoBehaviour
{
    private SpriteRenderer sr;
    private float fadeDuration;
    private float alpha;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Activate(Sprite sprite, Vector3 position, Quaternion rotation, float fadeDuration, float alpha)
    {
        transform.SetPositionAndRotation(position, rotation);
        sr.sprite = sprite;
        this.fadeDuration = fadeDuration;
        this.alpha = alpha;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float a = Mathf.Lerp(alpha, 0f, elapsed / fadeDuration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
            yield return null;
        }

        Destroy(gameObject);
    }
}
