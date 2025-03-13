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
        Collider2D[] attackCollider = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in attackCollider)
        {
            if (hit.TryGetComponent<Enemy>(out var enemy))
            {
                var enemyTarget = hit.GetComponent<CharCommonStats>();
                var weaponData = Inventory_Controller.Instance.GetEquipment(EquipmentType.Weapon);

                if (enemy.CanBeStun())
                {
                    enemy.beCountered = true;
                }
                
                if (weaponData != null)
                {
                    weaponData.ExecuteItemEfftect(enemyTarget.transform);
                }

                player.charStats.HandleDamage(enemyTarget);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.Instance.throwSword.CreateSword();
    }
}
