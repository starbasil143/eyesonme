using UnityEngine;

public class CPlayer : MonoBehaviour
{
    public GameObject PlayerParent;
    public Transform _transform;
    public CGameManager _gameManager;
    public CLevelLogic _levelLogic;
    private GameObject ChargesPanel;
    private Animator _resetTextAnimator;


    private int remainingCharges;

    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _transform = PlayerParent.transform;
        _gameManager = _transform.parent.transform.parent.GetComponent<CGameManager>();
        _levelLogic = _transform.parent.GetComponent<CLevelLogic>();
        remainingCharges = _levelLogic.chargeLimit;
        _gameManager._player = this;
        _resetTextAnimator = GameObject.FindGameObjectWithTag("ResetTextPanel").GetComponent<Animator>();
        // ChargesPanel = GameObject.FindGameObjectWithTag("ChargesPanel");
        // foreach (ChargeIcon chargeIcon in ChargesPanel.GetComponentsInChildren<ChargeIcon>())
        // {
        //     chargeIcon.UpdateUIFirst(remainingCharges);
        // }
    }


    public int GetRemainingCharges()
    {
        return remainingCharges;
    }

    public void SetRemainingCharges(int newRemainingCharges)
    {
        remainingCharges = newRemainingCharges;
    }

    public void GainCharge(int amount = 1)
    {
        remainingCharges += amount;
    }

    public void ExpendCharge(int amount = 1)
    {
        remainingCharges -= amount;

        // foreach (ChargeIcon chargeIcon in ChargesPanel.GetComponentsInChildren<ChargeIcon>())
        // {
        //     chargeIcon.UpdateUI(remainingCharges);
        // }

        if (remainingCharges <= 0 && _levelLogic.enemyCount > 0)
        {
            //_levelLogic.HandleLoss();
        }
    }

    

}
