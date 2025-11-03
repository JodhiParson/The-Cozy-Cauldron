using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    private InputAction Sprint;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction attack;
    [SerializeField] private PlayerMovement pMove;

    public void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        Sprint = playerInput.actions["Sprint"];
        attack = playerInput.actions["Attack"];
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        bool isSprinting = Sprint != null && Sprint.IsPressed();

        if (isSprinting)
        {

            pMove.mSpeed = 30f;
        }
        else
        {
            pMove.mSpeed = 20f;
        }

        bool isAttacking = attack != null && attack.WasPressedThisFrame();
        if (isAttacking && Movement == Vector2.zero)
        {
            pMove.TriggerAttack();
        }
    }
}
