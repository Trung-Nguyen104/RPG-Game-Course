public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.IsBusy = true;
        timerState = player.dashDuration;
        player.SkillManager.CreateClone.CreateClone(player.transform, new UnityEngine.Vector3(0,0,0));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.DashDir, 0);
        if (timerState < 0)
            playerStateMachine.ChangeState(player.IdleState);

        if (player.WallDetected())
            playerStateMachine.ChangeState(player.WallSlideState);
        player.fx.CreateAfterImage();
    }
}
