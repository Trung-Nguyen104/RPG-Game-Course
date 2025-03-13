using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike_Controller : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            var playerStats = Player_Manager.Instance.Player.GetComponent<PlayerStats>();
            var enemyTarget = collision.GetComponent<EnemyStats>();

            playerStats.HandleMagicalDamage(enemyTarget);
        }
    }
}
