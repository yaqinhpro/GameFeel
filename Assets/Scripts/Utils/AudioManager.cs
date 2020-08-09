using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backGroundSound;
    public AudioSource gameOverSound;

    public event EventHandler OnGameOverSoundPlayed;

    public void PlayGameOverSound()
    {
        backGroundSound.Stop();
        gameOverSound.Play();
        StartCoroutine(WaitForSound(gameOverSound.clip));
    }

    public IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => gameOverSound.isPlaying == false);
        OnGameOverSoundPlayed.Invoke(this, EventArgs.Empty);
    }
}
