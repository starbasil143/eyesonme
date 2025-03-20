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
    bool isFrozen = false;

    public delegate void OnBeamFire();
    public static event OnBeamFire onBeamFire;
    private bool winDelayRunning;

    public TimelineManager _winTimeline;
    public GameObject _filledProgressBar;


    private void Start()
    {
        animator = GetComponent<Animator>();

        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_audience_murmur, transform.position);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ambience, transform.position);

        //StartGame();   
    }

    public void BeamFired()
    {
        onBeamFire?.Invoke();
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
        _player = currentLevel.GetComponentInChildren<CPlayer>();
        if (currentLevel.GetComponent<CLevelLogic>().startMusic)
        {
            AudioManager.instance.SetMusicArea(currentLevel.GetComponent<CLevelLogic>().startMusicArea);
            AudioManager.instance.SetSongVersion(currentLevel.GetComponent<CLevelLogic>().startMusicVersion);
        }
    }


    public void ImpactFreeze(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    IEnumerator FreezeCoroutine(float duration)
    {
        if (!isFrozen)
        {
            isFrozen = true;
            float timeToReturnTo = Time.timeScale;

            Time.timeScale=0f;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = timeToReturnTo;
            isFrozen = false;
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
            transparency += Time.deltaTime * 10;
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
            t += Time.deltaTime * 3f;
            if (t > 1)
            {
                t = 1;
            }
            progressBarFill.fillAmount = Mathf.Lerp(originalFillAmount, newFillAmount, t);
            yield return null;
        }

        if (newFillAmount >= 1)
        {
            _filledProgressBar.SetActive(true);
            animator.SetTrigger("levelWon");
            progressBarFill.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            yield return new WaitForSeconds(.25f);
            while (transparency > 0)
            {
                transparency -= Time.deltaTime * 10;
                if (transparency < 0)
                {
                    transparency = 0;
                }
                progressBarFill.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = transparency;
                yield return null;
            }
        }
    }


    public void RestartLevel()
    {
        GetComponent<Animator>().Play("c_level_restart");
    }

    public void ClearMessage()
    {
        currentLevel.GetComponent<CLevelLogic>().ClearMessageText();
    }

    public void CloseLevel()
    {
        ClearMessage();
        if (currentLevel != null)
        {
            if (currentLevel.GetComponent<CLevelLogic>().endMusic)
            {
                AudioManager.instance.SetMusicArea(currentLevel.GetComponent<CLevelLogic>().endMusicArea);
                AudioManager.instance.SetSongVersion(currentLevel.GetComponent<CLevelLogic>().endMusicVersion);
            }
            Destroy(currentLevel);
        }
    }


    public void WinLevel()
    {
        if (index < levels.Length - 1)
        {
            if (!winDelayRunning)
            {
                StartCoroutine(WinLevelDelay());
            }
        }
        else 
        {
            if (!winDelayRunning)
            {
                StartCoroutine(WinLevelDelay());
            }
        }
    }

    IEnumerator WinLevelDelay()
    {
        winDelayRunning = true;
        yield return new WaitForSeconds(.4f);
        NextLevel();
        winDelayRunning = false;
    }


    public void NextLevel()
    {
        CloseLevel();
        index++;
        animator.Play("c_level_transition");
    }
    // IEnumerator WinAllLevelsDelay()
    // {
    //     winDelayRunning = true;
    //     yield return new WaitForSeconds(.4f);
    //     LastLevelComplete();
    //     winDelayRunning = false;
    // }

    public void LastLevelComplete()
    {
        CloseLevel();
        _winTimeline.gameObject.SetActive(true);
    }

    

 
}
