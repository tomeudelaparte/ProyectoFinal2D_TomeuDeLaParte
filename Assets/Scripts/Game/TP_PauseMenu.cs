using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_PauseMenu : MonoBehaviour
{
    // Public references
    public GameObject playerInterface;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    // Private references
    private TP_AudioManager audioManager;

    // Values
    private bool isActivePause = false;

    private void Start()
    {
        // Gets references
        audioManager = FindObjectOfType<TP_AudioManager>();

        // Shows the UI and hide pause/options panel
        playerInterface.SetActive(true);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        // If the ESC key is pressed, shows the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    // Pause Menu
    public void ShowPauseMenu()
    {
        // If the pause menu is not active
        if (!isActivePause)
        {
            // Pause time scale
            Time.timeScale = 0;

            // Shows cursor
            Cursor.visible = true;

            // Hide UI and shows menus
            playerInterface.SetActive(false);
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);

            // Pause all music and sounds
            foreach (AudioSource source in audioManager.GetComponents<AudioSource>())
            {
                source.Pause();
            }

            // Pause is true
            isActivePause = true;
        }

        // If the pause menu its active
        else
        {
            // Unpause time scale
            Time.timeScale = 1;

            // Hides cursor
            Cursor.visible = false;

            // Hide menus and shows UI
            playerInterface.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);

            // Unpause all music and sounds
            foreach (AudioSource source in audioManager.GetComponents<AudioSource>())
            {
                source.UnPause();
            }

            // Pause is false
            isActivePause = false;
        }
    }
}
