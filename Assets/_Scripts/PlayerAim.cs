using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject PlayerParent;
    public Camera MainCamera;
    public Transform _transform;
    public bool isReady;
    public Player _player;


    [SerializeField] private LineRenderer _lineRenderer;


    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _player = PlayerParent.GetComponentInChildren<Player>();
        _transform = PlayerParent.transform;
        _lineRenderer = PlayerParent.GetComponentInChildren<LineRenderer>();
        _lineRenderer.positionCount = 2;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        if (InputManager.Ready)
        {
            ReadyUpdate();

            if (!isReady)
            {
                isReady = true;
                _lineRenderer.enabled = true;
            }
        }
        else
        {
            if (isReady)
            {
                isReady = false;
                _lineRenderer.enabled = false;
            }
        }

        if (InputManager.Fire && isReady)
        {
            Fire();
        }
    }


    private void ReadyUpdate()
    { 
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.Aim);
        Vector2 playerPos = _transform.position;
        Vector2 differenceVector = mousePos - playerPos;
        Vector2 directionVector = differenceVector.normalized;

        _lineRenderer.SetPosition(0, playerPos);
        _lineRenderer.SetPosition(1, playerPos + directionVector*20);

    }

    private void Fire()
    {
        if (_player.GetRemainingCharges() >= 1)
        {
            _player.ExpendCharge();
            Debug.Log("bang!!!!!");
        }
        else
        {
            Debug.Log("fizzle.....");
        }
    }
}
