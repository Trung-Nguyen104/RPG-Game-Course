using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_FadeSrceen : MonoBehaviour
{
    public static UI_FadeSrceen Instance {  get; private set; }
    private Animator Animator => GetComponent<Animator>();

    [SerializeField] private GameObject youDiedText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void FadeOut() => Animator.SetTrigger("FadeOut");

    public void FadeOut(float _second) => StartCoroutine(WaitASecond(_second, FadeOut));

    public void FadeIn() => Animator.SetTrigger("FadeIn");

    public void FadeIn(float _second) => StartCoroutine(WaitASecond(_second, FadeIn));

    public void EndGame()
    {
        var restartGameBtn = Instantiate(youDiedText, transform).GetComponentInChildren<Button>();
        if (restartGameBtn != null)
        {
            restartGameBtn.onClick.AddListener(() => GameManager.Instance.RestartGame());
        }
    } 

    public void EndGame(float _second) => StartCoroutine(WaitASecond(_second, EndGame));

    private IEnumerator WaitASecond(float _second, Action _action)
    {
        yield return new WaitForSeconds(_second);
        _action();
    }
}
