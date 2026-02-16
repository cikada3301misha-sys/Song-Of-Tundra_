using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    [Header("Настройки цели")]
    public Transform player;           // Ссылка на трансформ игрока
    public LayerMask enemyLayer;       // Слой, на котором находятся враги
    public float lockOnRadius = 15f;   // Радиус поиска врага

    [Header("Настройки камеры")]
    public float rotationSpeed = 10f;  // Скорость поворота камеры к врагу
    public KeyCode lockOnKey = KeyCode.CapsLock; // Клавиша для удержания (Shift)

    private Transform _currentTarget;   // Текущая цель

    void Update()
    {
        // Если клавиша зажата
        if (Input.GetKey(lockOnKey))
        {
            // Если цели еще нет, ищем ближайшую
            if (!_currentTarget)
            {
                FindNearestEnemy();
            }

            // Если цель найдена, фокусируемся на ней
            if (_currentTarget)
            {
                FocusOnTarget();
            }
        }
        else
        {
            // Если клавиша отпущена, сбрасываем цель
            _currentTarget = null;
        }
    }

    private void FindNearestEnemy()
    {
        // Находим все коллайдеры на слое врагов в радиусе
        Collider[] enemiesInRadius = Physics.OverlapSphere(player.position, lockOnRadius, enemyLayer);
        
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        // Ищем самого близкого из них
        foreach (Collider enemyCollider in enemiesInRadius)
        {
            float distanceToEnemy = Vector3.Distance(player.position, enemyCollider.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemyCollider.transform;
            }
        }

        _currentTarget = closestEnemy;
    }

    private void FocusOnTarget()
    {
        // Вычисляем направление от камеры к врагу
        // Если скрипт висит на игроке, замените на направление от игрока
        Vector3 directionToTarget = _currentTarget.position - transform.position;
        
        // (Опционально) Игнорируем разницу по высоте, чтобы камера не "клевала" носом в пол
        // directionToTarget.y = 0; 

        // Вычисляем нужный поворот
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Плавно поворачиваем камеру к цели с помощью Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}