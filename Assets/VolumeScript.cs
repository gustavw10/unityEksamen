using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    public AudioListener AudioListener;
    public float shootVolume = 1f;


    public void Update() {
        AudioListener.volume = shootVolume;
    }

    public void SetVolume(float volume)
    {
        shootVolume = volume;
    }
}