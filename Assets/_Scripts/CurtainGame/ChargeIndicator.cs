using UnityEngine.UI;
using UnityEngine;

public class ChargeIndicator : MonoBehaviour
{
    private Image _image;
    private CGameManager _gameManager;

    void Awake()
    {
        _image = GetComponent<Image>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CGameManager>();
    }

    void Update()
    {
        if (_gameManager._player != null)
        {
            _image.fillAmount = _gameManager._player.GetRemainingCharges() * .07f;
        }
        else
        {
            _image.fillAmount = 0;
        }
    }
}
