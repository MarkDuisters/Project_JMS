﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GamePaused = false;
    GameManager gm;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject keybindUI;
    [SerializeField] private AudioSource[] audioSources;
    private float[] oldVolumes;
    [SerializeField] private string menuName;

    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = oldVolumes[i];
        }
        pauseUI.SetActive(false);
        gm.movementController.RestartMovement();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        oldVolumes = new float[audioSources.Length];
        for (int i = 0; i < audioSources.Length; i++)
        {
            oldVolumes[i] = audioSources[i].volume;
            audioSources[i].volume = 0;
        }
        pauseUI.SetActive(true);
        gm.movementController.StopMovement();
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        if (menuName != "")
        {
            SceneManager.LoadScene(menuName);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key: " + e.keyCode);
        }
    }

    public void BackToOptions()
    {

    }
}
