using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Ailment Colors")]
    [SerializeField] private Color[] igniteColorEffect;
    [SerializeField] private Color chillColorEffect;
    [SerializeField] private Color[] shockColorEffect;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void StunEffect()
    {
        if (sr.color != Color.white)
        { sr.color = Color.white; }
        else
        { sr.color = Color.red; }
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
