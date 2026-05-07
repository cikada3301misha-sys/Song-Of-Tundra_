using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    // ���������� ��� ����������� � ������� �������
    public Vector2 movementInput;

    // ������ ���� ����� ������ �� ��� ��� ����������
    public float verticalInput;
    public float horizontalInput;

    // ���������� ��� ����
    public bool attackInput;
    public bool altAttackInput;

    // ���������� ��� ����������� ���� � ��� �������
    public bool walkInput;
    public bool crouchInput;

    // ���������� ��� ������
    public bool jumpInput;

    // ��� ����� ���� � ����� �� ������� �� ��� ��������
    private void OnEnable() // ����� ������ �������
    {
        // ���� ������� ���������� ��� �� ������� �� ���� �������
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // ���� ��������
            playerControls.Player.Movement.performed += OnMovement;
            playerControls.Player.Movement.canceled += OnMovement;

            // ���� ����� � �������������� �����
            playerControls.Player.Attack.performed += OnAttack;
            playerControls.Player.Attack.canceled += OnAttack;

            playerControls.Player.AltAttack.performed += OnAltAttack;
            playerControls.Player.AltAttack.canceled += OnAltAttack;

            // ���� ���������� ���� � �������
            playerControls.Player.Walk.performed += OnWalk;
            playerControls.Player.Walk.canceled += OnWalk;

            playerControls.Player.Crouch.performed += OnCrouch;
            playerControls.Player.Crouch.canceled += OnCrouch;

            // ���� ������
            playerControls.Player.Jump.performed += OnJump;
        }

        // �������� ���� ������
        playerControls.Enable();
    }

    private void OnDisable()
    {
        // ��������� ���� ������
        playerControls.Disable();
    }

    // ���������� PlayerManager ������ ����
    public void HandleAllInput()
    {
        HandleMovementInput();
    }

    // ��������� Vector2 �� ���
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    // ���������� ����� ������������
    private void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // ����������� ����� ����
    private void OnAttack(InputAction.CallbackContext context)
    {
        attackInput = context.ReadValueAsButton();
    }

    private void OnAltAttack(InputAction.CallbackContext context)
    {
        altAttackInput = context.ReadValueAsButton();
    }

    // ����������� ����� ���������� ���� � �������
    private void OnWalk(InputAction.CallbackContext context)
    {
        walkInput = context.ReadValueAsButton();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        crouchInput = context.ReadValueAsButton();
    }

    // ���������� ����� ������
    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpInput = true;
    }
    public void ConsumeJumpInput()
    {
        jumpInput = false;
    }
}
