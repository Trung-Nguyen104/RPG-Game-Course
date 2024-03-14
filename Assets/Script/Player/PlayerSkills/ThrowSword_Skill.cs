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
    private Vector2 aimDirection;
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
        if (Input.GetKeyUp(KeyCode.H))
            aimDirection = new Vector2(inputX, inputY);
        SetUpTrajectoryLine();
        TrajectoryLineController();
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefabs, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = newSword.GetComponent<Sword_Skill_Controller>();
        SetAciveTrajectoryLine(false);
        swordController.SetUpSword(aimDirection, swordGravity);
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
        inputY += Input.GetAxisRaw("Vertical") * aimSpeed * Time.deltaTime;
        inputX += Input.GetAxisRaw("Horizontal") * aimSpeed * Time.deltaTime;
        if (inputY > aimLimit.y || inputX > aimLimit.x || inputX < -aimLimit.x || inputX < -aimLimit.y)
        {
            inputY = Mathf.Clamp(inputY, -aimLimit.y, aimLimit.y);
            inputX = Mathf.Clamp(inputX, -aimLimit.x, aimLimit.x);
        }
    }

    public void SetAciveTrajectoryLine(bool _isActive)
    {
        lineRenderer.enabled = _isActive;
    }

    private Vector2 TrajectoryLinePosition(float t)
    {
        Vector3 position = (Vector2)player.transform.position 
                            + new Vector2(inputX, inputY) * t 
                            + (t * t) * .5f * (Physics2D.gravity * swordGravity);
        return position;
    }
}
