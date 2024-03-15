using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword_Skill : Skill
{
    [SerializeField] private GameObject swordPrefabs;
    [SerializeField] private Vector2 aimLimit;
    [SerializeField] private float aimSpeed;
    [SerializeField] private float swordGravity;
    [SerializeField] private float lineLength;

    private int sizeOfLine = 25;
    private Vector3[] trajectoryLine;
    private LineRenderer lineRenderer;
    private GameObject newSword;
    public Sword_Skill_Controller swordController {  get; private set; }
    private Vector2 aimDirection;
    private bool trajectoryLineEnabled;
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
        newSword = Instantiate(swordPrefabs, player.transform.position, transform.rotation);
        swordController = newSword.GetComponent<Sword_Skill_Controller>();
        SetAciveTrajectoryLine(false);
        swordController.SetUpSword(aimDirection, swordGravity, player);
        player.CanCreateNewSword(false);
    }

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

    public void SetAciveTrajectoryLine(bool _isActive)
    {
        lineRenderer.enabled = _isActive;
        trajectoryLineEnabled = _isActive;
    }

    private Vector2 TrajectoryLinePosition(float t)
    {
        Vector3 position = (Vector2)player.transform.position 
                            + new Vector2(inputX, inputY) * t 
                            + (t * t) * .5f * (Physics2D.gravity * swordGravity);
        return position;
    }
}
