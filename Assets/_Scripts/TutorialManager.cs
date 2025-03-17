using System.Collections;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private CLevelLogic _levelLogic;
    private CGameManager _gameManager;
    private TextMeshProUGUI messageText;
    private int index = 0;
    private GameObject _enemy;

    void Start()
    {
        _levelLogic = GetComponent<CLevelLogic>();
        _gameManager = GetComponent<CGameManager>();
        _enemy = GetComponentInChildren<CircleCollider2D>().gameObject; //this is stupid
        _enemy.SetActive(false);
    }

    void Update()
    {
        switch (index)
        {
            case 0: 
                index = 1;
                StartCoroutine(_levelLogic.DisplayLine("WASD to move", _levelLogic.messageDelay));
            break;
            
            
            case 1: 
                if (InputManager.Movement != Vector2.zero)
                {
                    index = 2;
                    StartCoroutine(_levelLogic.DisplayLine("SHIFT to aim with mouse", _levelLogic.messageDelay));
                }
            break;

            case 2: 
                if (InputManager.Ready)
                {
                    index = 3;
                    StartCoroutine(_levelLogic.DisplayLine("LEFT MOUSE to fire", _levelLogic.messageDelay));
                }
            break;

            case 3: 
                if (InputManager.Ready)
                {
                    index = 4;
                    StartCoroutine(_levelLogic.DisplayLine("LEFT MOUSE to fire", _levelLogic.messageDelay));
                }
            break;

            case 4:
                if (InputManager.Fire)
                {
                    index = 5;
                    StartCoroutine(WaitAfterBeam());
                }
            break;

            case 6:
                index = 7;
                _enemy.SetActive(true);

                StartCoroutine(_levelLogic.DisplayLine("Target the circles", _levelLogic.messageDelay));
            break;

        }
        
    }

    IEnumerator WaitAfterBeam()
    {
        yield return new WaitForSeconds(.8f);
        index = 6;
    }
}
