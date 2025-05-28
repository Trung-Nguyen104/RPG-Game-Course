using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardController : MonoBehaviour
{
    private int lostSouls;

    public void SetLostSouls(int _soulsAmout)
    {
        lostSouls = _soulsAmout;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out var _))
        {
            Skill_Manager.Instance.souls = lostSouls;
            Destroy(gameObject);
        }
    }
}
