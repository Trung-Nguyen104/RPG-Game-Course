using UnityEngine;

public class FxManager : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Ailment Colors Fx")]
    [SerializeField] private Color[] igniteColorEffect;
    [SerializeField] private Color chillColorEffect;
    [SerializeField] private Color[] shockColorEffect;

    [Header("After Image")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float alpha = 1f;
    [SerializeField] private float afterImageInterval = 0.1f;
    private float afterImageTimer = 0f;


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        afterImageTimer -= Time.deltaTime;
    }


    private void StunEffect()
    {
        if (sr.color != Color.white)
        { sr.color = Color.white; }
        else
        { sr.color = Color.red; }
    }

    public void CreateAfterImage()
    {
        if (afterImageTimer > 0f) { return; }
        
        var imageObj = GameManager.Instance.GetAfterImagePrfabs();
        var img = imageObj.GetComponent<FxAfterImage>();
        img.Activate(sr.sprite, transform.position, transform.rotation, fadeDuration, alpha);

        afterImageTimer = afterImageInterval;
    }

    public void IgniteEffectFor(float _igniteDuration)
    {
        InvokeRepeating(nameof(IgniteFx), 0, 0.5f);
        Invoke(nameof(CancelEffect), _igniteDuration);
    }

    private void IgniteFx()
    {
        if (sr.color != igniteColorEffect[0])
        {
            sr.color = igniteColorEffect[0];
        }
        else
        {
            sr.color = igniteColorEffect[1];
        }
    }

    private void CancelEffect()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
