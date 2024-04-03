public class SkeletonTakeDamgeState : EnemyState
{
    SkeletonEnemy skeleton;
    public SkeletonTakeDamgeState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, SkeletonEnemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.CloseCounterSignal();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(skeleton.sawPlayerState);
    }
}
