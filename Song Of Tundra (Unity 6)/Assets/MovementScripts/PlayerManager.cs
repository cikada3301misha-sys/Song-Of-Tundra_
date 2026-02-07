using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

    [SerializeField] private PlayerCrochVisualisation crochVisualisation; // Вызываем тут класс с ссылкой в инспекторе

    public MovementState currentState; // перменная для отслеживанияя текущего состояния передвижения

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update() // обращаемся каждый кадр к нашим инпутам
    {
        inputManager.HandleAllInput();

        // Дебажим кнопки атак
        HandleCombat();

        // Вызываем проверку текущего состояния передвижения
        MovementStateCheck();
        playerLocomotion.SetStatedSpeed(currentState);

        // Визуалиция приседа
        crochVisualisation.OnCrouchAnimation(currentState);
    }

    private void FixedUpdate() // фиксированое кол-во кадров, поэтому именно тут обращаемся к нашим движениям
    {
        playerLocomotion.HandleAllMovement();
    }

    // Проверка текущего состояния передвижения
    public void MovementStateCheck()
    {
        if (inputManager.crouchInput) { currentState = MovementState.Crouch; return; }
        if (inputManager.walkInput) { currentState = MovementState.Walk; return; }
        if (inputManager.movementInput != Vector2.zero) { currentState = MovementState.Run; return; }

        currentState = MovementState.Idle; 
    }

    // ДЕБАГ РАБОТЫ АТАК, ПОТОМ УДАЛИТЬ
    private void HandleCombat()
    {
        if (inputManager.attackInput) Debug.Log("Атака нахуй)))");
        if (inputManager.altAttackInput) Debug.Log("Другая атака нахуй)))");
    }

}
