using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity_Behavior
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("Dash Asbility")]
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashDuration;

    [Header("Attack")]
    [SerializeField] public Vector2[] attackMovement;

    public float DashDir { get; private set; }
    public Skill_Manager SkillManager { get; private set; }
    public bool CanThrowSword { get; private set; }
    public bool IsBusy { get; set; }

    #region State
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSildeState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerAimState AimState { get; private set; }
    public PlayerCatchSwordState CatchSwordState { get; private set; }
    public PlayerUltimateState UltimateState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerTakeDamgeState TakeDamgeState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        CanThrowSword = true;
        PlayerStateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, PlayerStateMachine, "Idle");
        MoveState = new PlayerMoveState(this, PlayerStateMachine, "Move");
        JumpState = new PlayerJumpState(this, PlayerStateMachine, "Jump");
        AirState = new PlayerAirState(this, PlayerStateMachine, "Jump");
        DashState = new PlayerDashState(this, PlayerStateMachine, "Dash");
        WallSlideState = new PlayerWallSildeState(this, PlayerStateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, PlayerStateMachine, "Jump");
        AttackState = new PlayerAttackState(this, PlayerStateMachine, "Attack");
        AimState = new PlayerAimState(this, PlayerStateMachine, "AimSword");
        CatchSwordState = new PlayerCatchSwordState(this, PlayerStateMachine, "CatchSword");
        UltimateState = new PlayerUltimateState(this, PlayerStateMachine, "Jump");
        DeadState = new PlayerDeadState(this, PlayerStateMachine, "Die");
        TakeDamgeState = new PlayerTakeDamgeState(this, PlayerStateMachine, "TakeDamge");
    }

    protected override void Start()
    {
        base.Start();
        PlayerStateMachine.Initialize(IdleState);
        SkillManager = Skill_Manager.Instance;
    }

    protected override void Update()
    {
        base.Update();
        PlayerStateMachine.currentState.Update();

        if(isTakeDamged)
        {
            PlayerStateMachine.ChangeState(TakeDamgeState);
        }

        if (PlayerStateMachine.currentState == AttackState)
            FlipController(0);

        if (!WallDetected())
        {
            OnDash();
        }
    }

    public void CanCreateNewSword(bool _boolValue)
    {
        CanThrowSword = _boolValue;
    }

    private void OnDash()
    {
        if (!SkillManager.Dash.skill_Unlocked)
        {
            return;
        }
        if (Inputs.Instance.GetInputDown(InputAction.Dash) && SkillManager.Dash.CanUseSkill() && !IsBusy)
        {
            AudioManager.Instance.PlaySFX(11);
            DashDir = Inputs.Instance.GetHorizontal();
            if (DashDir == 0)
                DashDir = facingDir;

            PlayerStateMachine.ChangeState(DashState);
        }
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        PlayerStateMachine.ChangeState(DeadState);
    }

    public void AnimationTrigger() => PlayerStateMachine.currentState.AnimCalledTrigger();
}
