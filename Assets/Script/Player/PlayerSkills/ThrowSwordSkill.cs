using UnityEngine;
public enum SwordType
{
    Regular,
    Bounce,
    Pierce
}
public class ThrowSwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;
    [SerializeField] private int regularDamage;

    [Header("Bounce Sword")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private int bounceDamage;

    [Header("Pierce Sword")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private int pierceDamage;

    [Space(20)]
    [SerializeField] private GameObject swordPrefabs;
    [SerializeField] private Vector2 aimLimit;
    [SerializeField] private float aimSpeed;

    public SwordSkillController swordController { get; private set; }
    private LineRenderer lineRenderer;
    private Vector3[] trajectoryLine;
    private Vector2 aimDirection;
    private int sizeOfLine = 25;
    private bool trajectoryLineEnabled;
    private float lineLength =0.06f;
    private float throwForce;
    private float swordGravity;
    private float inputX;
    private float inputY;

    protected override void Start()
    {
        base.Start();
        trajectoryLine = new Vector3[sizeOfLine];
        lineRenderer = player.GetComponentInChildren<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        SetUpTrajectoryLine();

        if (trajectoryLineEnabled)
        {
            TrajectoryLineController();
        }
        if (Inputs.Instance.GetInputUp(InputAction.ThorwSword))
        {
            aimDirection = new Vector2(inputX, inputY);
        }
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefabs, player.transform.position, transform.rotation);
        swordController = newSword.GetComponent<SwordSkillController>();
        SetActiveTrajectoryLine(false);
        swordController.SetUpSword(aimDirection, SetUpGravity(), SetUpThrowForce(), player);
        player.CanCreateNewSword(false);
        HandleSwordType();
    }

    private void HandleSwordType()
    {
        switch (swordType)
        {
            case SwordType.Bounce: 
                BounceSwordType();
                break; 
            case SwordType.Pierce:
                PierceSwordType();
                break;
            default:
                swordController.swordDamage = regularDamage;
                break;
        }
    }

    #region Set Up Sword Type
    private float SetUpGravity() => swordGravity = (swordType == SwordType.Pierce) ? 0 : 3.5f;

    private float SetUpThrowForce() => throwForce = (swordType == SwordType.Pierce) ? 2f : 1;

    private void BounceSwordType()
    {
        swordController.SetUpSwordBounce(bounceAmount, bounceSpeed, true, bounceDamage);
    }

    private void PierceSwordType()
    {
        swordController.SetUpSwordPierce(pierceAmount, true, pierceDamage);
    }
    #endregion

    #region TrajectoryLine
    private void SetUpTrajectoryLine()
    {
        if (Inputs.Instance.GetInput(InputAction.ThorwSword))
        {
            for (int i = 0; i < trajectoryLine.Length; i++)
            {
                trajectoryLine[i] = TrajectoryLinePosition(i * lineLength);
            }
        }
        lineRenderer.positionCount = sizeOfLine;
        lineRenderer.SetPositions(trajectoryLine);
    }

    private void TrajectoryLineController()
    {
        inputX += Inputs.Instance.GetHorizontal() * aimSpeed * Time.deltaTime;
        inputY += Inputs.Instance.GetVeritcal() * aimSpeed * Time.deltaTime;
        player.FlipController(inputX);
        if (inputY > aimLimit.y || inputX > aimLimit.x || inputX < -aimLimit.x || inputX < -aimLimit.y)
        {
            inputX = Mathf.Clamp(inputX, -aimLimit.x, aimLimit.x);
            inputY = Mathf.Clamp(inputY, -aimLimit.y, aimLimit.y);
        }
    }

    public void SetActiveTrajectoryLine(bool _isActive)
    {
        lineRenderer.enabled = _isActive;
        trajectoryLineEnabled = _isActive;
    }

    private Vector2 TrajectoryLinePosition(float _lineLength)
    {
        Vector2 position = (Vector2)player.transform.position
                            + new Vector2(inputX, inputY) * _lineLength
                            + (_lineLength * _lineLength) * .5f * (Physics2D.gravity * SetUpGravity());
        return position;
    }
    #endregion
}
