using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private SkeletonEnemy skeleton;
    public SkeletonStunState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, SkeletonEnemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        HandleStunKnockBack();
        stateTimer = skeleton.stunDuration;
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.fx.Invoke("CancelEffect", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.idleState);
    }

    private void HandleStunKnockBack()
    {
        var knockBackForce = skeleton.knockBackForce + Random.Range(1, 3);
        enemy.fx.InvokeRepeating("StunEffect", 0, .1f);
        rb.AddForce(new Vector2(1 * -skeleton.facingDir, 1) * knockBackForce, ForceMode2D.Impulse);
    }
}
