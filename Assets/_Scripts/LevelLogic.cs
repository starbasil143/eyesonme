using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    public int levelIndex;
    public int chargeLimit;
    public int enemyCount;
    public string nextScene;

    public GameObject winScreen;
    public GameObject loseScreen;
    public PauseManager pauseManager;



    void Awake()
    {
        pauseManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<PauseManager>();
    }

    public void HandleKill()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            HandleWin();
        }
    }

    public void HandleLoss()
    {
        loseScreen.SetActive(true);
        pauseManager.ForcePause();
    }

    public void HandleWin()
    {
        winScreen.SetActive(true);
        pauseManager.ForcePause();
    }

    public void RestartLevel()
    {
        pauseManager.ForceUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextScene()
    {
        pauseManager.ForceUnpause();
        SceneManager.LoadScene(nextScene);
    }


}
