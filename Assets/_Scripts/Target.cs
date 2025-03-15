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
    }

    [System.NonSerialized]
    public bool enemy;
    public bool freezeOnHit;

    public TargetType targetType;
    private GameObject TargetParent;
    private CLevelLogic _levelLogic;
    public GameObject deathObject;
    
    void Awake()
    {
        _levelLogic = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<CLevelLogic>();
        TargetParent = transform.parent.gameObject;
        enemy = (targetType==TargetType.Skeleton || 
                 targetType==TargetType.Zombie || 
                 targetType==TargetType.Revenant ||
                 targetType==TargetType.Tank);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PreFreezeHandleBeam()
    {
        if (deathObject != null)
        {
            TargetParent.GetComponentInChildren<SpriteRenderer>().enabled = false;
            Instantiate(deathObject, transform.position, transform.rotation, TargetParent.transform.parent);
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

        }

        return msg;
    }
    
}
