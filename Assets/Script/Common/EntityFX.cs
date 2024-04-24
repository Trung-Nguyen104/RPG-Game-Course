using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Color[] igniteColorEffect;
    private bool igniteBool;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void StunEffect()
    {
        if(sr.color != Color.white)
            sr.color = Color.white; 
        else 
            sr.color = Color.red;
    }

    public void IgniteEffect(float _igniteDuration)
    {
        InvokeRepeating(nameof(IgniteFx), 0, 0.3f);
        Invoke(nameof(CancelEffect), _igniteDuration);
    }

    private void IgniteFx()
    {
        if(sr.color != igniteColorEffect[0])
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
