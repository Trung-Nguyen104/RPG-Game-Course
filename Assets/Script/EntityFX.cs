using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

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

    private void CancelStunEffect()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
