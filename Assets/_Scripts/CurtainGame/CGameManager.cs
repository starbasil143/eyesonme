using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CGameManager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject currentLevel;
    private int index;
    public float progressAmount;
    public Image progressBarFill;
    public CPlayer _player;

    private void Start()
    {
        StartGame();   
    }

    public void StartGame()
    {
        index = 0;
        progressAmount = 0;
        currentLevel = Instantiate(levels[0], gameObject.transform);
    }

    public void RestartLevel()
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(levels[index], gameObject.transform);
    }

    public void WinLevel()
    {
        if (index < levels.Length)
        {
            NextLevel();
        }
        else 
        {
            Debug.Log("no more levels for you");
        }
    }

    public void NextLevel()
    {
        _player.gameObject.SetActive(false);
        index++;
        RestartLevel();
        progressBarFill.fillAmount = (float)index/levels.Length; 
    }

    IEnumerator LevelTransitionDelay()
    {
        _player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(currentLevel);
        index++;
        progressBarFill.fillAmount = (float)index/levels.Length; 
    }
}
