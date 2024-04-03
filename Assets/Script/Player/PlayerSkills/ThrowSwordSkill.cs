using UnityEngine;
public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class ThrowSwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce Sword")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceSpeed;

    [Header("Pierce Sword")]
    [SerializeField] private int pierceAmount;

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
            TrajectoryLineController();

        if (Input.GetKeyUp(KeyCode.H))
            aimDirection = new Vector2(inputX, inputY);
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefabs, player.transform.position, transform.rotation);
        swordController = newSword.GetComponent<SwordSkillController>();
        SetActiveTrajectoryLine(false);
        swordController.SetUpSword(aimDirection, SetUpGravity(), SetUpThrowForce(), player);
        player.CanCreateNewSword(false);

        BounceSwordType();
        PierceSwordType();
    }

    #region Set Up Sword Type
    private float SetUpGravity() => swordGravity = (swordType == SwordType.Pierce) ? 0 : 3.5f;

    private float SetUpThrowForce() => throwForce = (swordType == SwordType.Pierce) ? 2f : 1;

    private void BounceSwordType()
    {
        if (swordType == SwordType.Bounce)
            swordController.SetUpSwordBounce(bounceAmount, bounceSpeed, true);
    }

    private void PierceSwordType()
    {
        if (swordType == SwordType.Pierce)
            swordController.SetUpSwordPierce(pierceAmount, true);
    }
    #endregion

    #region TrajectoryLine
    private void SetUpTrajectoryLine()
    {
        if (Input.GetKey(KeyCode.H))
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
        inputX += player.PlayerInputHorizontal() * aimSpeed * Time.deltaTime;
        inputY += player.PlayerInputVertical() * aimSpeed * Time.deltaTime;
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

    private Vector2 TrajectoryLinePosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position
                            + new Vector2(inputX, inputY) * t
                            + (t * t) * .5f * (Physics2D.gravity * SetUpGravity());
        return position;
    }
    #endregion
}
