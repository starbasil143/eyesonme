using UnityEngine;
using UnityEngine.UI;

public class ChargeIcon : MonoBehaviour
{
    public int index;
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateUIFirst(int chargesLeft)
    {
        if (chargesLeft < index)
        {
            //_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
            gameObject.SetActive(false);
        }
    }

    public void UpdateUI(int chargesLeft)
    {
        if (chargesLeft < index)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.3f);
        }
    }
}
