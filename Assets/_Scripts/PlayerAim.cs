using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public GameObject PlayerParent;
    public Camera MainCamera;
    public Transform _transform;
    public bool isReady;
    public Player _player;

    private LayerMask layerMask;

    [SerializeField] private LineRenderer _lineRenderer;


    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _player = PlayerParent.GetComponentInChildren<Player>();
        _transform = PlayerParent.transform;
        _lineRenderer = PlayerParent.GetComponentInChildren<LineRenderer>();
        _lineRenderer.positionCount = 2;
        layerMask = LayerMask.GetMask("Target");
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
            if (_player.GetRemainingCharges() >= 1)
            {
                Fire();
            }
            else
            {
                Debug.Log("fizzle.....");
            }
        }
    }


    private void ReadyUpdate()
    { 
        Vector2 playerPos = _transform.position;

        _lineRenderer.SetPosition(0, playerPos);
        _lineRenderer.SetPosition(1, playerPos + GetMouseDirectionVector()*20);

    }

    private void Fire()
    {
        BeamContinue(_transform.position, GetMouseDirectionVector(), true);
    }

    private void BeamContinue(Vector2 newOrigin, Vector2 direction, bool ignorePlayer = false)
    {
        RaycastHit2D ray;

        if (ignorePlayer)
        {
            ray = Physics2D.Raycast(newOrigin, direction, Mathf.Infinity, layerMask);
        }
        else
        {
            ray = Physics2D.Raycast(newOrigin, direction, Mathf.Infinity);
        }

        if (ray.collider.CompareTag("Target"))
        {
            switch (ray.collider.GetComponentInChildren<Target>().HandleBeam())
            {
                case "continue":
                    BeamContinue(ray.point, direction);
                    break;

                case "stop":
                    _player.ExpendCharge();
                    break;

                case "danger":
                    LoseLevel();
                    break;
            }
        }
        else
        {
            _player.ExpendCharge();
        }
    }

    private Vector2 GetMouseDirectionVector()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputManager.Aim);
        Vector2 playerPos = _transform.position;
        Vector2 differenceVector = mousePos - playerPos;
        Vector2 directionVector = differenceVector.normalized;

        return directionVector;
    }

    private void LoseLevel()
    {
        Debug.Log("booooooo!!!! you lose");
    }
}
