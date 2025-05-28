using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Entity_Stats
{
    private Player player;
    public override void TakeDamageHP(int _damage, Entity_Stats _targetStats)
    {
        base.TakeDamageHP(_damage, _targetStats);
        player.TakeDamageEffect();
    }

    protected override void HandleDie()
    {
        base.HandleDie();
        player.wasDead = true;
        player.DeadEffect();
        GameManager.Instance.DeathPosition = player.transform.position;
        GameManager.Instance.LostSouls = Skill_Manager.Instance.souls;
        Skill_Manager.Instance.souls = 0;
        Inventory_Controller.Instance.LoseAllItems();
    }

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
}
