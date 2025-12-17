using UnityEngine;
using UnityEngine.Audio;


public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private const float _minVolume = -80f;
    private const float _maxVolume = 20f;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";

    public void SetMusicVolume (float percentage)
    {
        _mixer.SetFloat(MusicKey, Mathf.Lerp(_minVolume, _maxVolume, percentage));
    }

    public void SetSFXVolume (float percentage)
    {
        _mixer.SetFloat(SFXKey, Mathf.Lerp(_minVolume, _maxVolume, percentage));
    }
}
