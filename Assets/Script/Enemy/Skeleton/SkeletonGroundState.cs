using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected SkeletonEnemy skeleton;
    protected Transform playerTransform;
    public SkeletonGroundState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, SkeletonEnemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = Player_Manager.Instance.Player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.PlayerDetected() || Vector3.Distance(skeleton.transform.position, playerTransform.position) < 2f)
        {
            stateMachine.ChangeState(skeleton.SawPlayerState);
        }
    }
}
