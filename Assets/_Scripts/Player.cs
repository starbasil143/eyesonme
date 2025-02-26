using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerParent;
    public Transform _transform;
    public LevelLogic _levelLogic;

    private int remainingCharges;

    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _transform = PlayerParent.transform;
        _levelLogic = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelLogic>();
        remainingCharges = _levelLogic.chargeLimit;
    }


    public int GetRemainingCharges()
    {
        return remainingCharges;
    }

    public void SetRemainingCharges(int newRemainingCharges)
    {
        remainingCharges = newRemainingCharges;
    }

    public void ExpendCharge(int amount = 1)
    {
        remainingCharges -= amount;
        if (remainingCharges <= 0)
        {
            _levelLogic.HandleLoss();
        }
    }
}
