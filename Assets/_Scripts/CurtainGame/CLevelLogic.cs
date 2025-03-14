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


    // public GameObject goalObject;



    void Start()
    {
        pauseManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<PauseManager>();
        _gameManager = transform.parent.gameObject.GetComponent<CGameManager>();
        enemiesRemaining = enemyCount;
        messageText = GameObject.FindGameObjectWithTag("LevelMessage").GetComponent<TextMeshProUGUI>();

        if (levelStartMessage != null && levelStartMessage != "")
        {
            StartCoroutine(DisplayLine(levelStartMessage, messageDelay));
        }

        // goalObject = GameObject.FindGameObjectWithTag("Goal");
        // Color goalcolor = goalObject.GetComponentInChildren<SpriteRenderer>().color;
        // goalObject.GetComponentInChildren<SpriteRenderer>().color = new Color(goalcolor.r, goalcolor.g, goalcolor.b, .2f);
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
        if (enemiesRemaining == 0)
        {
            ClearMessageText();
            _gameManager.WinLevel();
        }
    }

    public void ExitLevel()
    {
        ClearMessageText();
        Destroy(gameObject);
    }


    private IEnumerator DisplayLine(string line, float delay)
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
