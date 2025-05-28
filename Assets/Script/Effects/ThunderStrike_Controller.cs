using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike_Controller : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy_Stats>(out var enemyTarget))
        {
            Debug.Log("Handle ThunderStrike effect");
            var playerStats = Player_Manager.Instance.Player.GetComponent<Player_Stats>();

            playerStats.HandleMagicalDamage(enemyTarget);
            Destroy(gameObject, 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
