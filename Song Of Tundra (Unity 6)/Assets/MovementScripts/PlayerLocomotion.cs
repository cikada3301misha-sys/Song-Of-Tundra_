using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    public Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    // public float speedMultiplier = 1f;

    // Модификаторы скорости
    public float snowMultiplier = 1f;

    // Переменная модификатор зависиящая от состояния передвижения игрока
    public float currentSpeedMultiplier;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayers = ~0; // по умолчанию всё

    public bool isGrounded;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement() // входная точка в поворот и в движение
    {
        HandleMovement();
        HandleRotation();

        HandleJump(inputManager.jumpInput);
        inputManager.ConsumeJumpInput();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0; // шоб не улетать когда смотришь вверх

        moveDirection *= movementSpeed * currentSpeedMultiplier * snowMultiplier; // Результирующее направление с учетом изменений

        Vector3 movementVelocity = moveDirection;
        var currentVel = playerRigidbody.linearVelocity;
        playerRigidbody.linearVelocity = new Vector3(movementVelocity.x, currentVel.y, movementVelocity.z);
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        { 
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    // Передаем множителю скорости состояний значение
    public void SetStatedSpeed(MovementState state) 
    {
        switch (state)
        {
            case MovementState.Crouch:
                currentSpeedMultiplier = 0.4f; 
                break;
            case MovementState.Walk:
                currentSpeedMultiplier = 0.5f;
                break;
            case MovementState.Run:
                currentSpeedMultiplier = 1f; 
                break;
            case MovementState.Idle:
                currentSpeedMultiplier = 0f; 
                break;
        }
    }

    public void HandleJump(bool jumpPressed)
    {
        CheckGround();

        if (!jumpPressed) return;
        if (!isGrounded) return;

        // v = sqrt(2 * g * h)
        float jumpVelocity = Mathf.Sqrt(2f * Physics.gravity.magnitude * jumpHeight);

        var v = playerRigidbody.linearVelocity;
        playerRigidbody.linearVelocity = new Vector3(v.x, jumpVelocity, v.z);
    }

    private void CheckGround()
    {
        // маленький луч вниз из центра
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance + 0.1f, groundLayers, QueryTriggerInteraction.Ignore);
    }
}
