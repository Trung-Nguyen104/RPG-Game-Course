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

    public float dashDir { get; private set; }
    public Skill_Manager skillManager { get; private set; }
    public bool canCreateNewSword { get; private set; }
    public bool isBusy { get; set; }

    #region State
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSildeState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerUltimateState ultimateState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        canCreateNewSword = true;
        playerStateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, playerStateMachine, "Idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "Move");
        jumpState = new PlayerJumpState(this, playerStateMachine, "Jump");
        airState = new PlayerAirState(this, playerStateMachine, "Jump");
        dashState = new PlayerDashState(this, playerStateMachine, "Dash");
        wallSlideState = new PlayerWallSildeState(this, playerStateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, playerStateMachine, "Jump");
        attackState = new PlayerAttackState(this, playerStateMachine, "Attack");
        aimState = new PlayerAimState(this, playerStateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, playerStateMachine, "CatchSword");
        ultimateState = new PlayerUltimateState(this, playerStateMachine, "Jump");
        deadState = new PlayerDeadState(this, playerStateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        playerStateMachine.Initialize(idleState);
        skillManager = Skill_Manager.Instance;
    }

    protected override void Update()
    {
        base.Update();
        playerStateMachine.currentState.Update();
        if (playerStateMachine.currentState == attackState)
            FlipController(0);

        if (!WallDetected())
        {
            OnDash();
        }
    }

    public void CanCreateNewSword(bool _boolValue)
    {
        canCreateNewSword = _boolValue;
    }

    private void OnDash()
    {
        if (!skillManager.Dash.skill_Unlocked)
        {
            return;
        }
        if (Inputs.Instance.GetInputDown(InputAction.Dash) && skillManager.Dash.CanUseSkill() && !isBusy)
        {
            dashDir = Inputs.Instance.GetHorizontal();
            if (dashDir == 0)
                dashDir = facingDir;

            playerStateMachine.ChangeState(dashState);
        }
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        playerStateMachine.ChangeState(deadState);
    }

    public void AnimationTrigger() => playerStateMachine.currentState.AnimCalledTrigger();
}
