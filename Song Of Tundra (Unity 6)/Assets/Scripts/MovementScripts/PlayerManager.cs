using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

    [SerializeField] private PlayerCrochVisualisation crochVisualisation; // �������� ��� ����� � ������� � ����������

    public MovementState currentState; // ��������� ��� ������������� �������� ��������� ������������

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update() // ���������� ������ ���� � ����� �������
    {
        inputManager.HandleAllInput();

        // ������� ������ ����
        HandleCombat();

        // �������� �������� �������� ��������� ������������
        MovementStateCheck();
        playerLocomotion.SetStatedSpeed(currentState);

        // ���������� �������
        crochVisualisation.OnCrouchAnimation(currentState);
    }

    private void FixedUpdate() // ������������ ���-�� ������, ������� ������ ��� ���������� � ����� ���������
    {
        playerLocomotion.HandleAllMovement();
    }

    // �������� �������� ��������� ������������
    public void MovementStateCheck()
    {
        if (inputManager.crouchInput) { currentState = MovementState.Crouch; return; }
        if (inputManager.walkInput) { currentState = MovementState.Walk; return; }
        if (inputManager.movementInput != Vector2.zero) { currentState = MovementState.Run; return; }

        currentState = MovementState.Idle; 
    }

    // ����� ������ ����, ����� �������
    private void HandleCombat()
    {
        if (inputManager.attackInput);
        if (inputManager.altAttackInput) Debug.Log("������ ����� �����)))");
    }

}
