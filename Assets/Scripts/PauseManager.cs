using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf ? 0f : 1f;
    }
}
