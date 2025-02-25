using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    
    public static Vector2 Movement;
    private InputAction _moveAction;

    public static bool Ready;
    private InputAction _readyAction;

    public static Vector2 Aim;
    private InputAction _aimAction;

    public static bool Fire;
    private InputAction _fireAction;



    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _aimAction = _playerInput.actions["Aim"];
        _readyAction = _playerInput.actions["Ready"];
        _fireAction = _playerInput.actions["Fire"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Aim = _aimAction.ReadValue<Vector2>();
        Ready = _readyAction.IsPressed();
        Fire = _fireAction.WasPressedThisFrame();

    }

}
