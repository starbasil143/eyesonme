using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject PlayerParent;
    public Camera MainCamera;


    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = PlayerParent.transform.position;
        
        Vector2 differenceVector = mousePos - playerPos;

        Debug.Log(differenceVector);   
    }
}
