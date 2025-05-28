using UnityEngine;

public class EnemySoundArea : AudioAreaControl
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var _))
        {
            if (enemy == null) { return; }
            if (enemy.StateMachine.CurrentState == enemy.IdleStateProperty)
            {
                AudioManager.Instance.PlaySFX(soundFxNumber);
            }
            else
            {
                AudioManager.Instance.StopSFX(soundFxNumber);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
