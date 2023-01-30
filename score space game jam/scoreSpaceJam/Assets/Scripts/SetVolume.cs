using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void setLevel(float sVal)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sVal) * 20);
    }
}
