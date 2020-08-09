using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour
{
    public TimeManager timeManager;
    public AudioManager audioManager;
    public PlayerController player;

    void Start()
    {
        audioManager.OnGameOverSoundPlayed += Event_OnGameOverSoundPlayed;
        player.OnPlayerDied += Event_OnPlayerDied;
    }

    private void Event_OnPlayerDied(object sender, EventArgs e)
    {
        timeManager.SlowMotion();
        audioManager.PlayGameOverSound();
    }

    private void Event_OnGameOverSoundPlayed(object sender, EventArgs e)
    {
        SceneManager.LoadScene(1);
    }
}
