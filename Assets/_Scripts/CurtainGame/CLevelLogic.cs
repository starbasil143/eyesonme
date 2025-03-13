using UnityEngine;

public class CLevelLogic : MonoBehaviour
{
    public int chargeLimit;
    public int enemyCount;
    public int enemiesRemaining;
    public PauseManager pauseManager;
    public CGameManager _gameManager;
    // public GameObject goalObject;



    void Start()
    {
        pauseManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<PauseManager>();
        _gameManager = transform.parent.gameObject.GetComponent<CGameManager>();
        enemiesRemaining = enemyCount;
        // goalObject = GameObject.FindGameObjectWithTag("Goal");
        // Color goalcolor = goalObject.GetComponentInChildren<SpriteRenderer>().color;
        // goalObject.GetComponentInChildren<SpriteRenderer>().color = new Color(goalcolor.r, goalcolor.g, goalcolor.b, .2f);
    }

    

    public void HandleKill()
    {
        enemiesRemaining--;
    }

    public void CheckWin()
    {
        if (enemiesRemaining == 0)
        {
            _gameManager.WinLevel();
        }
    }

}
