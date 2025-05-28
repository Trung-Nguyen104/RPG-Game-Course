using UnityEngine;

public class AudioAreaControl : MonoBehaviour
{
    [SerializeField] protected bool interactive;
    [SerializeField] protected int soundFxNumber;
    [SerializeField] protected float fadeIn;
    [SerializeField] protected float fadeOut;

    public void ActiveSoundArea() => AudioManager.Instance.PlaySFX(soundFxNumber, fadeIn);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var _) && !interactive)
        {
            AudioManager.Instance.PlaySFX(soundFxNumber, fadeIn);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopSFX(soundFxNumber, fadeOut);
        }
    }
}
