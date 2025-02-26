using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum TargetType
    {
        Skeleton,
        Zombie,
        Revenant,
        Wall,
        BreakableWall,
        Mirror,
        Danger,
    }

    [System.NonSerialized]
    public bool enemy;

    public TargetType targetType;
    private GameObject TargetParent;
    
    void Awake()
    {
        TargetParent = transform.parent.gameObject;
        enemy = (targetType==TargetType.Skeleton || 
                 targetType==TargetType.Zombie || 
                 targetType==TargetType.Revenant);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string HandleBeam()
    {
        string msg = "none";

        switch (targetType)
        {
            case TargetType.Skeleton:

                Destroy(TargetParent);
                msg = "continue";
                break;

            case TargetType.Wall:

                msg = "stop";
                break;

            case TargetType.BreakableWall:

                Destroy(TargetParent);
                msg = "stop";
                break;

            case TargetType.Mirror:

                msg = "reflect";
                break;

            case TargetType.Danger:

                msg = "danger";
                break;

        }

        return msg;
    }
    
}
