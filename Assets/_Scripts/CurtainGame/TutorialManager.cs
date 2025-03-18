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
        _levelLogic.restartAllowed = false;
    }

    void Update()
    {
        switch (index)
        {
            case 0: 
                index = 1;
                StartCoroutine(_levelLogic.DisplayLine("WASD to move", 0));
            break;
            
            
            case 1: 
                if (InputManager.Movement != Vector2.zero)
                {
                    index = 2;
                    StartCoroutine(_levelLogic.DisplayLine("SHIFT to aim with mouse", 0));
                }
            break;

            case 2: 
                if (InputManager.Ready)
                {
                    index = 3;
                    StartCoroutine(_levelLogic.DisplayLine("Click while aiming to fire", 0));
                }
            break;

            case 3:
                if (InputManager.Fire && InputManager.Ready)
                {
                    index = 4;
                    StartCoroutine(WaitAfterBeam());
                }
            break;

            case 5:
                index = 6;
                _enemy.SetActive(true);

                StartCoroutine(_levelLogic.DisplayLine("Target the circles", 0));
            break;

        }
        
    }

    IEnumerator WaitAfterBeam()
    {
        yield return new WaitForSeconds(.8f);
        index = 5;
    }
}
