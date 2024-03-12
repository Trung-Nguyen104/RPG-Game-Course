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
    [SerializeField] private float dashCoolDown;

    [Header("Attack")]
    [SerializeField] public Vector2[] attackMovement;

    public float dashDir { get; private set; }
    private bool doubleJump;
    private float dashTimer;

    #region State
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAIrState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSildeState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttack attackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerStateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, playerStateMachine, "Idle");
        moveState = new PlayerMoveState(this, playerStateMachine, "Move");
        jumpState = new PlayerJumpState(this, playerStateMachine, "Jump");
        airState = new PlayerAIrState(this, playerStateMachine, "Jump");
        dashState = new PlayerDashState(this, playerStateMachine, "Dash");
        wallSlideState = new PlayerWallSildeState(this, playerStateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, playerStateMachine, "Jump");
        attackState = new PlayerPrimaryAttack(this, playerStateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();
        playerStateMachine.Initialize(idleState);
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
            OnDoubleJump();
        }
    }

    private void OnDash()
    {
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.G) && dashTimer < 0)
        {
            dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;

            playerStateMachine.ChangeState(dashState);
        }
    }

    private void OnDoubleJump()
    {
        if (GroundDetected())
            doubleJump = true;
        if (Input.GetKeyDown(KeyCode.UpArrow) && doubleJump == true)
        {
            doubleJump = false;
            playerStateMachine.ChangeState(jumpState);
        }
    }

    public void AnimationTrigger() => playerStateMachine.currentState.AnimCalledTrigger();
}
