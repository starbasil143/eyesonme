using System;
using System.Collections;
using UnityEngine;

public class CPlayerAim : MonoBehaviour
{
    public GameObject PlayerParent;
    public Camera MainCamera;
    public Transform _transform;
    public bool isReady;
    public CPlayer _player;
    public CLevelLogic _levelLogic;
    public CGameManager _gameManager;
    public Material aimMaterial;
    public Material beamMaterial;
    public bool isFrozen = false;

    private LayerMask layerMask;

    [SerializeField] private LineRenderer _lineRenderer;

    public float aimWidth = .13f;
    public float beamWidth = .3f;


    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _player = PlayerParent.GetComponentInChildren<CPlayer>();
        _transform = PlayerParent.transform;
        _lineRenderer = PlayerParent.GetComponentInChildren<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _levelLogic = _player._levelLogic;
        _gameManager = _player._gameManager;
        layerMask = LayerMask.GetMask("Target");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _lineRenderer.material = aimMaterial;
    }


    void Update()
    {
        if (!isFrozen)
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

            if (InputManager.Reset)
            {
                _gameManager.RestartLevel();
            }   
        }
    
    }


    private void ReadyUpdate()
    { 
        Vector2 playerPos = _transform.position;

        if (_lineRenderer.material != aimMaterial)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.startWidth = aimWidth;
            _lineRenderer.endWidth = aimWidth;
            _lineRenderer.material = aimMaterial;
        }

        RaycastHit2D ray;
        ray = Physics2D.Raycast(playerPos, GetMouseDirectionVector(), Mathf.Infinity, layerMask);
        _lineRenderer.SetPosition(0, playerPos);
        _lineRenderer.SetPosition(1, ray.point);

    }

    private void Fire()
    {
        StartCoroutine(BeamContinue(_transform.position, GetMouseDirectionVector(), true));
    }

    IEnumerator BeamContinue(Vector2 newOrigin, Vector2 direction, bool ignorePlayer = false)
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

        _lineRenderer.material = beamMaterial;
        _lineRenderer.startWidth = beamWidth;
        _lineRenderer.endWidth = beamWidth;

        if(ignorePlayer) // if this is the initial beam segment
        {
            _lineRenderer.SetPosition(0, newOrigin);
            _lineRenderer.SetPosition(1, ray.point);
        }
        else
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount-1, ray.point);
        }



        if (ray.collider.CompareTag("Target"))
        {
            
            
            if (!isFrozen)
            {
                isFrozen = true;
                float timeToReturnTo = Time.timeScale;

                Time.timeScale=0f;

                yield return new WaitForSecondsRealtime(.15f);

                Time.timeScale = 1f;
                isFrozen = false;

                switch (ray.collider.GetComponentInChildren<Target>().HandleBeam())
                {
                    case "continue":
                        StartCoroutine(BeamContinue(ray.point, direction));
                        Debug.Log("beam: continue");
                        break;

                    case "stop":
                        _levelLogic.CheckWin();
                        _player.ExpendCharge();
                        break;

                    case "danger":
                        LoseLevel();
                        break;
                }
            }
        }
        else
        {
            _levelLogic.CheckWin();
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
        _gameManager.RestartLevel();
    }
}
