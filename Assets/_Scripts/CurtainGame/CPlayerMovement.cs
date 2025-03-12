using UnityEngine;

public class CPlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private GameObject PlayerParent;

    void Awake()
    {
        PlayerParent = transform.parent.gameObject;
        _rb = PlayerParent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.linearVelocity = _movement * _moveSpeed;
        
    }

}
