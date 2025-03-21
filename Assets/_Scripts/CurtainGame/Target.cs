using System.Collections;
using Unity.VisualScripting;
using UnityEngine;




public class Target : MonoBehaviour
{
    public enum TargetType
    {
        Skeleton,
        Zombie,
        Wall,
        BreakableWall,
        Mirror,
        Danger,
        FlimsyWall,
        Revenant,
        Tank,
        Player,
    }

    [System.NonSerialized]
    public bool enemy;
    public bool freezeOnHit;

    public TargetType targetType;
    private GameObject TargetParent;
    private CLevelLogic _levelLogic;
    public GameObject deathObject;
    public CGameManager _gameManager;
    public bool zombieActive = false;
    private GameObject _player;
    public GameObject skeletonPrefab;
    
    void Awake()
    {
        _levelLogic = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<CLevelLogic>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CGameManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        TargetParent = transform.parent.gameObject;
        enemy = (targetType==TargetType.Skeleton || 
                 targetType==TargetType.Zombie || 
                 targetType==TargetType.Revenant ||
                 targetType==TargetType.Tank);
                 
        switch (targetType)
        {
            case TargetType.Skeleton:

                
                break;

            case TargetType.Zombie:

                CGameManager.onBeamFire += BecomeZombieActive;
                break;

            case TargetType.Revenant:

                CGameManager.onBeamFire += ShootAtPlayer;
                break;

            case TargetType.Tank:

                
                break;

            case TargetType.Wall:

                
                break;

            case TargetType.BreakableWall:

                
                break;

            case TargetType.Mirror:

                
                break;

            case TargetType.Danger:

                
                break;

            case TargetType.FlimsyWall:
                
                
                break;

        }

        
    }

    void BecomeZombieActive()
    {
        StartCoroutine(ZombieActiveCoroutine());
    }

    IEnumerator ZombieActiveCoroutine()
    {
        yield return new WaitForSeconds(.3f);
        Instantiate(skeletonPrefab, transform.position, transform.rotation, _levelLogic.gameObject.transform);
        Destroy(transform.parent.gameObject);
    }
        

    void ShootAtPlayer()
    {
        StartCoroutine(ShootAtPlayerCoroutine());
    }

    IEnumerator ShootAtPlayerCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        Vector2 directionToPlayer = (_player.transform.position - transform.position).normalized;
        GetComponent<CRevenantAim>()._beamRenderer.positionCount = 1;
        GetComponent<CRevenantAim>()._beamRenderer.enabled = true;
        StartCoroutine(GetComponent<CRevenantAim>().BeamContinue((Vector2)transform.position + directionToPlayer/3, directionToPlayer, false, true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        switch (targetType)
        {
            case TargetType.Zombie:
                CGameManager.onBeamFire -= BecomeZombieActive;
                break;

            case TargetType.Revenant:
                CGameManager.onBeamFire -= ShootAtPlayer;     
                break;   
        }
    }

    public void PreFreezeHandleBeam()
    {

        if (deathObject != null)
        {
            switch (targetType)
            {
                case TargetType.Skeleton:
                    TargetParent.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    Instantiate(deathObject, transform.position, transform.rotation, TargetParent.transform.parent);
                    break;
                case TargetType.Tank:
                    TargetParent.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    Instantiate(deathObject, transform.position, transform.rotation, TargetParent.transform.parent);
                    break;
                case TargetType.Revenant:
                    TargetParent.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    Instantiate(deathObject, transform.position, transform.rotation, TargetParent.transform.parent);
                    break;
            }
        }

        switch (targetType)
        {
            case TargetType.Mirror:
                AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_wall_mirror, transform.position);
                break;
        }
    }

    public string HandleBeam()
    {
        string msg = "none";

        switch (targetType)
        {
            case TargetType.Skeleton:

                _levelLogic.HandleKill();
                msg = "continue";
                Destroy(TargetParent);
                break;

            case TargetType.Zombie:

                msg = "continue";

                break;

            case TargetType.Revenant:

                _levelLogic.HandleKill();
                msg = "continue";
                Destroy(TargetParent);
                break;


            case TargetType.Tank:

                _levelLogic.HandleKill();
                msg = "stop";
                Destroy(TargetParent);
                break;

            case TargetType.Wall:

                msg = "stop";
                break;

            case TargetType.BreakableWall:

                msg = "stop";
                Destroy(TargetParent);
                break;

            case TargetType.Mirror:

                msg = "reflect";
                break;

            case TargetType.Danger:

                msg = "danger";
                _levelLogic._gameManager.RestartLevel();
                break;

            case TargetType.FlimsyWall:
                msg = "continue";
                Destroy(TargetParent);
                break;

            case TargetType.Player:
                _player.GetComponentInChildren<CPlayer>().SelfDestruct();
                msg = "stop";
                break;
        }

        return msg;
    }
    
}
