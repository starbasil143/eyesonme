using System;
using System.Collections;
using UnityEngine;

public class CRevenantAim : MonoBehaviour
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

    public LineRenderer _beamRenderer;
    public float hitStopDuration = .2f;


    void Awake()
    {
        PlayerParent = GameObject.FindGameObjectWithTag("Player");
        _player = PlayerParent.GetComponentInChildren<CPlayer>();
        _transform = PlayerParent.transform;
        _levelLogic = _player._levelLogic;
        _gameManager = _player._gameManager;
        layerMask = LayerMask.GetMask("Target");
        layerMaskWithPlayer = LayerMask.GetMask("Target", "Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

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
                    isFrozen = true;
                    float timeToReturnTo = Time.timeScale;

                    Time.timeScale=0f;

                    yield return new WaitForSecondsRealtime(hitStopDuration);

                    Time.timeScale = 1f;
                    isFrozen = false;
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


    private void LoseLevel()
    {
        Debug.Log("booooooo!!!! you lose");
        _gameManager.RestartLevel();
    }
}
