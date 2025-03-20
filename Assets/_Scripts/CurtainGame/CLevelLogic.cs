using System.Collections;
using Ink.Parsed;
using TMPro;
using UnityEngine;

public class CLevelLogic : MonoBehaviour
{
    public int chargeLimit;
    public int enemyCount;
    public int enemiesRemaining;
    public PauseManager pauseManager;
    public CGameManager _gameManager;
    public string levelStartMessage;
    private TextMeshProUGUI messageText;
    public float messageDelay;
    public float typingSpeed = 0.02f;
    private float typingSpeedMultiplier = 1f;

    public bool restartAllowed = true;


    public bool tutorialLevel;
    private CPlayer _player;

    public bool startMusic;
    public MusicArea startMusicArea;
    public int startMusicVersion;
    public bool endMusic;
    public MusicArea endMusicArea;
    public int endMusicVersion = -1;


    // public GameObject goalObject;



    void Start()
    {
        pauseManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<PauseManager>();
        _gameManager = transform.parent.gameObject.GetComponent<CGameManager>();
        enemiesRemaining = enemyCount;
        messageText = GameObject.FindGameObjectWithTag("LevelMessage").GetComponent<TextMeshProUGUI>();
        _player = GetComponentInChildren<CPlayer>();

        if (levelStartMessage != null && levelStartMessage != "")
        {
            StartCoroutine(DisplayLine(levelStartMessage, messageDelay));
        }

        // goalObject = GameObject.FindGameObjectWithTag("Goal");
        // Color goalcolor = goalObject.GetComponentInChildren<SpriteRenderer>().color;
        // goalObject.GetComponentInChildren<SpriteRenderer>().color = new Color(goalcolor.r, goalcolor.g, goalcolor.b, .2f);
    }

    void Update()
    {
        if (InputManager.Reset && restartAllowed)
        {
            GameObject.FindGameObjectWithTag("ResetTextPanel").GetComponent<Animator>().SetTrigger("LevelReset");
            _gameManager.RestartLevel();
        }   
    }

    public void ClearMessageText()
    {
        messageText.text = "";
    }
    

    public void HandleKill()
    {
        enemiesRemaining--;
    }

    public void CheckWin()
    {
        StartCoroutine(CheckWinDelay());
    }

    IEnumerator CheckWinDelay()
    {
        yield return new WaitForSeconds(.5f);
        if (enemiesRemaining == 0)
        {
            ClearMessageText();
            GameObject.FindGameObjectWithTag("ResetTextPanel").GetComponent<Animator>().SetTrigger("LevelReset");
            _gameManager.WinLevel();
        }
        else if (_player.GetRemainingCharges() <= 0)
        {
            GameObject.FindGameObjectWithTag("ResetTextPanel").GetComponent<Animator>().SetTrigger("TextOn");
        }
    }

    public void ExitLevel()
    {
        if (endMusic)
        {
            AudioManager.instance.SetMusicArea(endMusicArea);
            AudioManager.instance.SetSongVersion(endMusicVersion);
        }
        ClearMessageText();
        Destroy(gameObject);
    }


    public IEnumerator DisplayLine(string line, float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = line;
        messageText.maxVisibleCharacters = 0;

        bool ignoringText = false;
        foreach (char letter in line)
        {
            if (letter == '<')
            {
                ignoringText = true;
            }

            
            if (!ignoringText)
            {


                yield return new WaitForSecondsRealtime(typingSpeed * typingSpeedMultiplier);
                messageText.maxVisibleCharacters++;
                if (letter != ' ' && letter != '\n')
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.voice_typing, transform.position);
                }

                switch (letter)
                {
                    case '?':
                    case '!':
                    case '.':
                        typingSpeedMultiplier = 5;
                        break;

                    case ',':
                        typingSpeedMultiplier = 3;
                        break;

                    default:
                        typingSpeedMultiplier = 1;
                        break;
                }
            }

            if (letter == '>')
            {
                ignoringText = false;
            }
        }
        
    }

}
