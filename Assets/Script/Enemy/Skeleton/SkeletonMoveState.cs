public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, SkeletonEnemy _skeleton) : base(_enemy, _stateMachine, animBoolName, _skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(skeleton.moveSpeed * skeleton.facingDir, rb.velocity.y);
        if (skeleton.WallDetected() || !skeleton.GroundDetected())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.IdleState);
        }
    }
}
