using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

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
    }

    private void FixedUpdate() // фиксированое кол-во кадров, поэтому именно тут обращаемся к нашим движениям
    {
        playerLocomotion.HandleAllMovement();
    }

    // ДЕБАГ РАБОТЫ АТАК, ПОТОМ УДАЛИТЬ
    private void HandleCombat()
    {
        if (inputManager.attackInput) Debug.Log("Атака нахуй)))");
        if (inputManager.altAttackInput) Debug.Log("Другая атака нахуй)))");
    }

}
