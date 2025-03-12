using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private LevelLogic levelLogic;

    void Awake()
    {
        levelLogic = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("th");
            if (levelLogic.goalActive)
            {
                levelLogic.HandleWin();
            }
        }
    }
}
