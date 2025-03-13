using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CGameManager : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject currentLevel;
    private int index;
    public float progressAmount;
    public Image progressBarFill;
    public CPlayer _player;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        //StartGame();   
    }

    public void StartFirstLevel()
    {
        index = 0;
        progressAmount = 0;
        animator.Play("c_game_start", 0, 0f);
    }

    public void StartLevel()
    {
        currentLevel = Instantiate(levels[index], gameObject.transform);
    }

    public void CloseLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }

    public void FillProgress()
    {
        Coroutine progressFillCoroutine = StartCoroutine(FillCoroutine());
    }

    IEnumerator FillCoroutine()
    {
        float transparency = 0f;
        while (transparency < 1)
        {
            transparency += Time.deltaTime * 7;
            if (transparency > 1)
            {
                transparency = 1;
            }
            progressBarFill.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = transparency;
            yield return null;
        }

        int numberOfLevels = levels.Length;
        float originalFillAmount = progressBarFill.fillAmount;
        float newFillAmount = (float)(index) / numberOfLevels;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * 2.5f;
            if (t > 1)
            {
                t = 1;
            }
            progressBarFill.fillAmount = Mathf.Lerp(originalFillAmount, newFillAmount, t);
            yield return null;
        }

        yield return new WaitForSeconds(.5f);

        while (transparency > 0)
        {
            transparency -= Time.deltaTime * 9;
            if (transparency < 0)
            {
                transparency = 0;
            }
            progressBarFill.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = transparency;
            yield return null;
        }
    }

    public void RestartLevel()
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(levels[index], gameObject.transform);
    }

    public void WinLevel()
    {
        if (index < levels.Length - 1)
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
        Destroy(currentLevel);
        index++;
        animator.Play("c_level_transition");
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
