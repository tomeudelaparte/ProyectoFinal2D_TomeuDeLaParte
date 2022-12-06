using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_PauseMenu : MonoBehaviour
{
    public GameObject playerInterface;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    private TP_AudioManager audioManager;

    private bool isActivePause = false;

    private void Start()
    {
        audioManager = FindObjectOfType<TP_AudioManager>();

        playerInterface.SetActive(true);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        if (!isActivePause)
        {
            Time.timeScale = 0;

            Cursor.visible = true;

            playerInterface.SetActive(false);
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);

            foreach (AudioSource source in audioManager.GetComponents<AudioSource>())
            {
                source.Pause();
            }

            isActivePause = true;
        }
        else
        {
            Time.timeScale = 1;

            Cursor.visible = false;

            playerInterface.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);

            foreach (AudioSource source in audioManager.GetComponents<AudioSource>())
            {
                source.UnPause();
            }

            isActivePause = false;
        }
    }
}
