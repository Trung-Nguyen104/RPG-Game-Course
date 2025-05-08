public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        timerState = player.dashDuration;
        player.skillManager.CreateClone.CreateClone(player.transform, new UnityEngine.Vector3(0,0,0));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (timerState < 0)
            playerStateMachine.ChangeState(player.idleState);

        if (player.WallDetected())
            playerStateMachine.ChangeState(player.wallSlideState);
    }
}
