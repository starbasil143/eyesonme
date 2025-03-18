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
    public bool isFrozen = false;

    private LayerMask layerMask;
    private LayerMask layerMaskWithPlayer;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private LineRenderer _beamRenderer;
    public float hitStopDuration = .2f;


    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _player = PlayerParent.GetComponentInChildren<CPlayer>();
        _transform = PlayerParent.transform;
        _lineRenderer.positionCount = 2;
        _levelLogic = _player._levelLogic;
        _gameManager = _player._gameManager;
        layerMask = LayerMask.GetMask("Target");
        layerMaskWithPlayer = LayerMask.GetMask("Target", "Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
                
            }

            
        }

    
    }


    private void ReadyUpdate()
    { 
        Vector2 playerPos = _transform.position;

        RaycastHit2D ray;
        ray = Physics2D.Raycast(playerPos, GetMouseDirectionVector(), Mathf.Infinity, layerMask);
        _lineRenderer.SetPosition(0, playerPos);
        _lineRenderer.SetPosition(1, ray.point);

    }

    private void Fire()
    {
        _lineRenderer.enabled = false;
        _beamRenderer.positionCount = 1;
        _beamRenderer.enabled = true;
        StartCoroutine(BeamContinue(_transform.position, GetMouseDirectionVector(), true, true));
        _gameManager.BeamFired();
    }
    
    // private IEnumerator CheckExpended()
    // {
    //     if (_player.GetRemainingCharges() <= 0)
    //     {
    //         Debug.Log("id");
    //         yield return new WaitForSeconds(1.5f);
    //         GameObject.FindGameObjectWithTag("ResetTextPanel").GetComponent<Animator>().Play("c_reset_text", 0, 0f);
    //     }
    // }

    public IEnumerator BeamContinue(Vector2 newOrigin, Vector2 direction, bool ignorePlayer = false, bool shootNoise = false)
    {
        RaycastHit2D ray;
        

        if (ignorePlayer)
        {
            ray = Physics2D.Raycast(newOrigin, direction, Mathf.Infinity, layerMask);
        }
        else
        {
            ray = Physics2D.Raycast(newOrigin, direction, Mathf.Infinity, layerMaskWithPlayer);
        }


        if(_beamRenderer.positionCount == 1) // if this is the initial beam segment
        {
            _beamRenderer.positionCount = 2;
            _beamRenderer.SetPosition(0, newOrigin);
            _beamRenderer.SetPosition(1, ray.point);
        }
        else
        {
            _beamRenderer.positionCount++;
            _beamRenderer.SetPosition(_beamRenderer.positionCount-1, ray.point);
        }



        if (ray.collider.CompareTag("Target") || ray.collider.CompareTag("Player"))
        {
            ray.collider.gameObject.GetComponentInChildren<Target>().PreFreezeHandleBeam();
            
            if (!isFrozen)
            {
                if (ray.collider.gameObject.GetComponentInChildren<Target>().freezeOnHit)
                {
                    _levelLogic.restartAllowed = false;
                    isFrozen = true;
                    float timeToReturnTo = Time.timeScale;

                    Time.timeScale=0f;

                    yield return new WaitForSecondsRealtime(hitStopDuration);

                    Time.timeScale = 1f;
                    isFrozen = false;
                    _levelLogic.restartAllowed = true;
                }
                


                
                yield return new WaitForSeconds(.06f);

                
                switch (ray.collider.GetComponentInChildren<Target>().HandleBeam())
                {
                    case "continue":
                        StartCoroutine(BeamContinue(ray.point, direction));
                        break;

                    case "stop":
                        _beamRenderer.enabled = false;
                        _levelLogic.CheckWin();
                        _player.ExpendCharge();
                        // StartCoroutine(CheckExpended());
                        break;

                    case "danger":
                        LoseLevel();
                        break;

                    case "reflect":
                        StartCoroutine(BeamContinue(ray.point - direction/10, Vector2.Reflect(direction, ray.normal)));
                        break;
                }
            }
        }
        else
        {
            _beamRenderer.enabled = false;
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
