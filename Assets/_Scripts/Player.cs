using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PlayerParent;
    public Transform _transform;
    public LevelLogic _levelLogic;
    private GameObject ChargesPanel;


    private int remainingCharges;

    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _transform = PlayerParent.transform;
        _levelLogic = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelLogic>();
        remainingCharges = _levelLogic.chargeLimit;
        ChargesPanel = GameObject.FindGameObjectWithTag("ChargesPanel");
        foreach (ChargeIcon chargeIcon in ChargesPanel.GetComponentsInChildren<ChargeIcon>())
        {
            chargeIcon.UpdateUIFirst(remainingCharges);
        }
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

        foreach (ChargeIcon chargeIcon in ChargesPanel.GetComponentsInChildren<ChargeIcon>())
        {
            chargeIcon.UpdateUI(remainingCharges);
        }

        if (remainingCharges <= 0 && _levelLogic.enemyCount > 0)
        {
            //_levelLogic.HandleLoss();
        }
    }

    

}
