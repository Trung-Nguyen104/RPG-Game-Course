public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cd2D.enabled = false;
        player.rb.isKinematic = true;
        UI_FadeSrceen.Instance.FadeOut(1.3f);
        UI_FadeSrceen.Instance.EndGame(0.7f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.IsBusy = true;
        player.SetVelocity(0, 0);
    }

}
