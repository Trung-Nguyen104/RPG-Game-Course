public class SkeletonDeadState : EnemyState
{
    private SkeletonEnemy skeleton;
    public SkeletonDeadState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, SkeletonEnemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.animator.speed = 1;
        skeleton.cd2D.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(0, 0);
    }
}
