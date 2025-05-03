using System.Data.Common;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool paused;
    public bool pausable;
    public bool unpausable;

    public GameObject pauseMenuCanvas;
    void Update()
    {
        if (InputManager.Pause)
        {
            if (pausable)
            {
                PauseGame();
            }
        }
        if (InputManager.Unpause)
        {
            if (unpausable)
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        paused = true;
        InputManager.instance.SwitchToUIMap();
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        paused = false;
        InputManager.instance.SwitchToPuzzleMap();
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ForcePause()
    {
        pausable = false;
        unpausable = false;
        PauseGame();
    }

    public void ForceUnpause()
    {
        pausable = true;
        unpausable = true;
        UnpauseGame();
    }
}
