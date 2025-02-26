using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    public int levelIndex;
    public int chargeLimit;
    public int enemyCount;



    void Awake()
    {
        
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandleWin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
