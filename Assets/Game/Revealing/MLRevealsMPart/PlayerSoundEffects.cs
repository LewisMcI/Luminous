using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : AudioManager
{
    Sound humNoise;
    private void Awake()
    {
        humNoise = GetAudio("Hum Noise");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (humNoise.audioSource.isPlaying == false && other.gameObject.tag == "Hum")
        {
            humNoise.audioSource.Play();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hum")
        {
            humNoise.audioSource.Stop();
        }
    }
}
