using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    public AudioSource audioSource;
    public float shootVolume = 1f;


    public void Update() {
        audioSource.volume = shootVolume;
    }

    public void SetVolume(float volume)
    {
        shootVolume = volume;
    }
}