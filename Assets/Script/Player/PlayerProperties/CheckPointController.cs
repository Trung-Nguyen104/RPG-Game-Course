using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public string ID;
    public bool actived;
    [SerializeField] private SpriteRenderer SaveIcon;
    private Animator Animator => GetComponent<Animator>();
    private AudioAreaControl AudioArea => GetComponentInChildren<AudioAreaControl>();

    private void Awake()
    {
        SaveIcon.enabled = false;
    }

    private void Update()
    {
        if(SaveIcon.enabled)
        {
            SaveHandle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var _))
        {
            SaveIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SaveIcon.enabled = false;
    }

    [ContextMenu("Generate CheckPoint ID")]
    private void GenerateID()
    {
        ID = System.Guid.NewGuid().ToString();
    }

    public void SetCheckPoint()
    {
        actived = true;
        Animator.SetBool("CheckPoint", actived);
    }

    private void GetLastCheckPoint()
    {
        GameManager.Instance.LastCheckPointID = ID;
    }

    private void SaveHandle()
    {
        if (Inputs.Instance.GetInputDown(InputAction.Save))
        {
            Debug.Log("Checked Point Saved");
            AudioArea.ActiveSoundArea();
            SetCheckPoint();
            GetLastCheckPoint();
            SaveLoadManager.Instance.SaveGame();
        }
    }
}
