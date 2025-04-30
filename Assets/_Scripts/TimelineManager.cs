using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    private GameObject _dialogueManager;
    private PlayableDirector _director;
    public List<TextAsset> dialogueAssets;
    public List<GameObject> objectsToDisable;
    public List<GameObject> objectsToEnable;
    public GameObject _curtain;
    public GameObject _stage;
    public CGameManager _gameManager;
    public MusicArea musicArea;
    public int musicVersion;
    public string associatedFlag;
    

    private void Start()
    {
        _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager");
        _director = GetComponent<PlayableDirector>();
    }


    public void DTrigger1()
    {
        DialogueManager.instance.EnterDialogue(dialogueAssets[0], true);
        PauseTimeline();
    }
    public void DTrigger2()
    {
        DialogueManager.instance.EnterDialogue(dialogueAssets[1], true);
        PauseTimeline();
    }
    public void DTrigger3()
    {
        DialogueManager.instance.EnterDialogue(dialogueAssets[2], true);
        PauseTimeline();
    }
    public void DTrigger4()
    {
        DialogueManager.instance.EnterDialogue(dialogueAssets[3], true);
        PauseTimeline();
    }
    public void DTrigger5()
    {
        DialogueManager.instance.EnterDialogue(dialogueAssets[4], true);
        PauseTimeline();
    }
    public void DTrigger6()
    {
        DialogueManager.instance.EnterDialogue(dialogueAssets[5], true);
        PauseTimeline();
    }

    public void PauseTimeline()
    {
        _director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void ResumeTimeline()
    {
        _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void DisableObjects()
    {
        objectsToDisable.ForEach(i => i.SetActive(false));
    }

    public void EnableObjects()
    {
        objectsToEnable.ForEach(i => i.SetActive(true));
    }

    public void DeactivateDirector()
    {
        gameObject.SetActive(false);
    }

    public void StartPuzzle()
    {
        _gameManager.StartFirstLevel();
    }

    public void ClosePuzzle()
    {
        _gameManager.GetComponent<Animator>().SetTrigger("turnOff");
    }

    public void OpenCurtain()
    {
        _curtain.GetComponent<Animator>().Play("curtain_open_full");
    }

    public void CloseCurtain()
    {
        _curtain.GetComponent<Animator>().Play("curtain_close_full");
    }

    public void SetMusic()
    {
        AudioManager.instance.SetMusicArea(musicArea);
    }
    public void SetMusicVersion()
    {
        AudioManager.instance.SetSongVersion(musicVersion);
    }

    public void KillLevel()
    {
        _gameManager.GoToKillLevel();
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            AudioManager.instance.SetMusicArea(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void GoToMenu()
    {
        AudioManager.instance.SetMusicArea(0);
        SceneManager.LoadScene("Title");
    }

    public void SaveProgress()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Curtain_1":
                PlayerPrefs.SetInt("part1_finished", 1);
                break;

            case "Curtain_2":
                PlayerPrefs.SetInt("part2_finished", 1);
                break;

            case "Curtain_3":
                PlayerPrefs.SetInt("part3_finished", 1);
                break;
        }
    }

}
