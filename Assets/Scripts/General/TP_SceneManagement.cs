using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP_SceneManagement : MonoBehaviour
{
    // Loads a scene with scenes name
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
