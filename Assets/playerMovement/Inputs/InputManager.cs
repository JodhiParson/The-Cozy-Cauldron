using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    private InputAction Sprint;
    private PlayerInput playerInput;
    private InputAction moveAction;
    [SerializeField] private PlayerMovement pMove;

    public void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        Sprint = playerInput.actions["Sprint"];
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        bool isSprinting = Sprint != null && Sprint.IsPressed();

        if (isSprinting)
        {
            
            pMove.mSpeed = 20f;
        }
        else
        {
            pMove.mSpeed = 15f;
        }
    }
}
