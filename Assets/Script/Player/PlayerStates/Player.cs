using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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
    public SkillManager skillManager { get; private set; }
    public bool canCreateNewSword { get; private set; }
    public bool isBusy { get; set; }

    #region State
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAIrState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSildeState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerCatchSword catchSwordState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        canCreateNewSword = true;
        playerStateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, playerStateMachine, "Idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "Move");
        jumpState = new PlayerJumpState(this, playerStateMachine, "Jump");
        airState = new PlayerAIrState(this, playerStateMachine, "Jump");
        dashState = new PlayerDashState(this, playerStateMachine, "Dash");
        wallSlideState = new PlayerWallSildeState(this, playerStateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, playerStateMachine, "Jump");
        attackState = new PlayerAttackState(this, playerStateMachine, "Attack");
        aimState = new PlayerAimState(this, playerStateMachine, "AimSword");
        catchSwordState = new PlayerCatchSword(this, playerStateMachine, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();
        playerStateMachine.Initialize(idleState);
        skillManager = SkillManager.Instance;
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
        if (Input.GetKeyDown(KeyCode.G) && skillManager.dash.CanUseSkill() && !isBusy)
        {
            dashDir = PlayerInputHorizontal();
            if (dashDir == 0)
                dashDir = facingDir;

            playerStateMachine.ChangeState(dashState);
        }
    }

    public float PlayerInputHorizontal() => Input.GetAxisRaw("Horizontal");
    public float PlayerInputVertical() => Input.GetAxisRaw("Vertical");
    public void AnimationTrigger() => playerStateMachine.currentState.AnimCalledTrigger();
}
