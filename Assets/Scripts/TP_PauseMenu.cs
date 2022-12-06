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
        // Si pulsas ESC y GameOver es False
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Muestra el menu de pausa
            ShowPauseMenu();
        }
    }

    // Muestra el menu de pausa
    public void ShowPauseMenu()
    {
        // Si pausa no está activa
        if (!isActivePause)
        {
            // Pausa el tiempo
            Time.timeScale = 0;

            // Desbloquea y muestra el ratón
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Oculta la interfaz de usuario
            playerInterface.SetActive(false);

            // Muestra el menu de pausa
            pauseMenu.SetActive(true);

            // Pausa la musica
            foreach (AudioSource source in audioManager.GetComponents<AudioSource>())
            {
                source.Pause();
            }

            // Pausa es True
            isActivePause = true;
        }
        else
        {
            // Reanuda el tiempo
            Time.timeScale = 1;

            // Bloquea y oculta el ratón
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            // Muestra la interfaz de usuario
            playerInterface.SetActive(true);

            // Oculta el menu de opciones y pausa
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);

            foreach (AudioSource source in audioManager.GetComponents<AudioSource>())
            {
                source.UnPause();
            }

            // Pausa es False
            isActivePause = false;
        }
    }
}
