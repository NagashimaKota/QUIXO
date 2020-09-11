using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SundPlay : MonoBehaviour {
    public AudioSource systemAudio;
    public AudioSource startAudio;

    public void StartAudioPlay()
    {
        startAudio.Play();
    }

    public void SystemAudioPlay()
    {
        systemAudio.Play();
    }
}
