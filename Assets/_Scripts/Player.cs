using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerParent;
    public Transform _transform;
    public int startingCharges;

    private int remainingCharges;

    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _transform = PlayerParent.transform;
        remainingCharges = startingCharges;
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
    }
}
