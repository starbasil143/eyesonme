
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour
{
    public int levelIndex;
    public int chargeLimit;
    public int enemyCount;
    public string nextScene;
    public bool goalActive;

    public GameObject winScreen;
    public GameObject loseScreen;
    public PauseManager pauseManager;
    public GameObject goalObject;



    void Awake()
    {
        pauseManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<PauseManager>();
        goalObject = GameObject.FindGameObjectWithTag("Goal");
        Color goalcolor = goalObject.GetComponentInChildren<SpriteRenderer>().color;
        goalObject.GetComponentInChildren<SpriteRenderer>().color = new Color(goalcolor.r, goalcolor.g, goalcolor.b, .2f);
    }

    void Update()
    {
        if (InputManager.Reset)
        {
            RestartLevel();
        }
    }

    public void HandleKill()
    {
        enemyCount--;
        Debug.Log(enemyCount);
        if (enemyCount == 0)
        {
            ActivateGoal();
        }
    }

    public void ActivateGoal()
    {
        goalActive = true;
        Color goalcolor = goalObject.GetComponentInChildren<SpriteRenderer>().color;
        goalObject.GetComponentInChildren<SpriteRenderer>().color = new Color(goalcolor.r, goalcolor.g, goalcolor.b, 1f);
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
