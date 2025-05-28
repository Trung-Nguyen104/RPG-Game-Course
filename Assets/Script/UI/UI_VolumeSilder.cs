using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSilder : MonoBehaviour
{
    public string pramater;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier;
    private Slider VolumeSlider => GetComponent<Slider>();

    public void SliderValue(float _value)
    {
        audioMixer.SetFloat(pramater, Mathf.Log10(_value) * multiplier);
    }

    public float GetVolumeValue() => VolumeSlider.value;

    public void SetVolumeValue(float _value) => VolumeSlider.value = _value;
}
