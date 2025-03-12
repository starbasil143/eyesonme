using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private PlayerInput _playerInput;

    
    public static Vector2 Movement;
    private InputAction _moveAction;

    public static bool Ready;
    private InputAction _readyAction;

    public static Vector2 Aim;
    private InputAction _aimAction;

    public static bool Fire;
    private InputAction _fireAction;

    public static bool Pause;
    private InputAction _pauseAction;
    
    public static bool Unpause;
    private InputAction _unpauseAction;

    public static bool Reset;
    private InputAction _resetAction;

    public static bool Advance;
    private InputAction _advanceAction;

    public static bool Right;
    private InputAction _rightAction;

    public static bool Left;
    private InputAction _leftAction;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        


        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _aimAction = _playerInput.actions["Aim"];
        _readyAction = _playerInput.actions["Ready"];
        _fireAction = _playerInput.actions["Fire"];
        _pauseAction = _playerInput.actions["Pause"];
        _unpauseAction = _playerInput.actions["Unpause"];
        _resetAction = _playerInput.actions["Reset"];
        _advanceAction = _playerInput.actions["Advance"];
        _leftAction = _playerInput.actions["Left"];
        _rightAction = _playerInput.actions["Right"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Aim = _aimAction.ReadValue<Vector2>();
        Ready = _readyAction.IsPressed();
        Fire = _fireAction.WasPressedThisFrame();
        Pause = _pauseAction.WasPressedThisFrame();
        Unpause = _unpauseAction.WasPressedThisFrame();
        Reset = _resetAction.WasPressedThisFrame();
        Advance = _advanceAction.WasPressedThisFrame();
        Left = _leftAction.WasPressedThisFrame();
        Right = _rightAction.WasPressedThisFrame();
    }

    public void SwitchToPuzzleMap()
    {
        _playerInput.SwitchCurrentActionMap("Puzzle");
    }
    public void SwitchToUIMap()
    {
        _playerInput.SwitchCurrentActionMap("UI");
    }
    public void SwitchToDialogueMap()
    {
        _playerInput.SwitchCurrentActionMap("Dialogue");
    }

}
