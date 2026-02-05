using UnityEngine;

public class SnowSlowZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // При входе в снег
    {
        PlayerLocomotion playerLocomotion = other.GetComponent<PlayerLocomotion>(); // Передаем класс с проверкой в локальную переменную

        if (playerLocomotion != null) playerLocomotion.speedMultiplier = 0.4f; // Передаем переменной множителя скорорсти значение
    }

    private void OnTriggerExit(Collider other) // При выходе из снега
    {
        PlayerLocomotion playerLocomotion = other.GetComponent<PlayerLocomotion>();

        if (playerLocomotion != null) playerLocomotion.speedMultiplier = 1f;
    }
}
