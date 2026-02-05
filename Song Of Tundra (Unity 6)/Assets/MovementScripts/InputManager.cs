using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    // Переменная для направления в формате вектора
    public Vector2 movementInput;

    // Вектор выше будем делить на эти две переменные
    public float verticalInput;
    public float horizontalInput;

    // Переменные для атак
    public bool attackInput;
    public bool altAttackInput;

    // Всю часть ниже я нихуя не понимаю но она работает
    private void OnEnable() // когда объект активен
    {
        // Если система управления ещё не создана то надо создать
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // ввод движения
            playerControls.Player.Movement.performed += OnMovement;
            playerControls.Player.Movement.canceled += OnMovement;

            // ввод атаки и альтернативной атаки
            playerControls.Player.Attack.performed += OnAttack;
            playerControls.Player.Attack.canceled += OnAttack;

            playerControls.Player.AltAttack.performed += OnAltAttack;
            playerControls.Player.AltAttack.canceled += OnAltAttack;
        }

        // Включаем приём инпута
        playerControls.Enable();
    }

    private void OnDisable()
    {
        // Выключаем приём инпута
        playerControls.Disable();
    }

    // Вызывается PlayerManager каждый кадр
    public void HandleAllInput()
    {
        HandleMovementInput();
    }

    // Разбираем Vector2 на оси
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    // Обработчик ввода передвижения
    private void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Обработчики ввода атак
    private void OnAttack(InputAction.CallbackContext context)
    {
        attackInput = context.ReadValueAsButton();
    }

    private void OnAltAttack(InputAction.CallbackContext context)
    {
        altAttackInput = context.ReadValueAsButton();
    }
}
