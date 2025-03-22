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
    public GameObject killLevel;
    public bool killTime;
    public TimelineManager _spottedTimeline;

    public int indexForCutscene = -1;


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
        currentLevel = Instantiate(killTime?killLevel:levels[index], gameObject.transform);
        
        _player = currentLevel.GetComponentInChildren<CPlayer>();
        if (currentLevel.GetComponent<CLevelLogic>().startMusic)
        {
            AudioManager.instance.SetMusicArea(currentLevel.GetComponent<CLevelLogic>().startMusicArea);
            AudioManager.instance.SetSongVersion(currentLevel.GetComponent<CLevelLogic>().startMusicVersion);
        }
    }

    public void StartLevelKill()
    {
        killTime = true;
        animator.Play("c_game_start", 0, 0f);
    }

    public void GoToKillLevel()
    {
        StartLevelKill();
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

        if (killTime)
        {
            originalFillAmount = 0;
            newFillAmount = 1;
        }
        float t = 0f;
        AudioManager.instance.SetFillAmount(progressBarFill.fillAmount);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_fill, transform.position);
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
        if (currentLevel.GetComponent<CLevelLogic>() != null)
        {
            currentLevel.GetComponent<CLevelLogic>().ClearMessageText();
        }
    }

    public void CloseLevel()
    {
        if (currentLevel != null)
        {
        ClearMessage();
            Destroy(currentLevel);
        }
    }


    public void WinLevel(bool killLevel)
    {
        if (index < levels.Length - 1)
        {
            if (!winDelayRunning)
            {
                StartCoroutine(WinLevelDelay(killLevel));
            }
        }
        else 
        {
            if (!winDelayRunning)
            {
                StartCoroutine(WinLevelDelay(killLevel));
            }
        }
    }

    IEnumerator WinLevelDelay(bool isKillLevel)
    {
        winDelayRunning = true;
        yield return new WaitForSeconds(.4f);

        NextLevel(index == indexForCutscene);
        winDelayRunning = false;
    }


    public void NextLevel(bool isKillLevel = false)
    {
        if (currentLevel.GetComponent<CLevelLogic>().endMusic)
        {
            AudioManager.instance.SetMusicArea(currentLevel.GetComponent<CLevelLogic>().endMusicArea);
            AudioManager.instance.SetSongVersion(currentLevel.GetComponent<CLevelLogic>().endMusicVersion);
        }
        CloseLevel();
        index++;
        if (isKillLevel)
        {
            animator.Play("transition_to_notice");
        }
        else
        {
            animator.Play("c_level_transition");
        }
    }

    public void LastLevelComplete()
    {
        CloseLevel();
        _winTimeline.gameObject.SetActive(true);
    }

    public void SpottedCutscene()
    {
        CloseLevel();
        killTime = true;
        _spottedTimeline.gameObject.SetActive(true);
    }

    

 
}
