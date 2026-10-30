using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioSource CrystalAudioSource;
    public AudioSource LifeAudioSource;
    public AudioClip[] CrystalSounds;

    private int index = 0;

    public void PlayCrystalSound()
    {
        if (CrystalSounds.Length == 0) return;

        CrystalAudioSource.clip = CrystalSounds[index];
        CrystalAudioSource.Play();

        index++;

        if (index >= CrystalSounds.Length)
        {
            index = 0;
        }
    }
    public void PlayLifeSound()
    {
        LifeAudioSource.Play();
    }

}