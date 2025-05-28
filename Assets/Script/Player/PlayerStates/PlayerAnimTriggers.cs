using UnityEngine;

public class PlayerAnimTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();


    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        AudioManager.Instance.PlaySFX(2);
        Collider2D[] attackCollider = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in attackCollider)
        {
            if (hit.TryGetComponent<Enemy>(out var enemy))
            {
                var enemyTarget = hit.GetComponent<Entity_Stats>();
                var weaponData = Inventory_Controller.Instance.GetEquipment(EquipmentType.Weapon);

                if (enemy.CanBeStun() && Skill_Manager.Instance.Parry.skill_Unlocked)
                {
                    enemy.beCountered = true;
                }
                
                if (weaponData != null)
                {
                    weaponData.ExecuteItemEfftect(enemyTarget.transform);
                }

                player.EntityStats.HandleDamage(enemyTarget);
            }
        }
    }

    private void ThrowSword()
    {
        Skill_Manager.Instance.ThrowSword.CreateSword();
    }
}
