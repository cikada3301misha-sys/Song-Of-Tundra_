using Unity.VisualScripting;
using UnityEngine;


// Файл для визуализаци приседа
public class PlayerCrochVisualisation : MonoBehaviour
{
    public void OnCrouchAnimation(MovementState state)
    {
        if (state == MovementState.Crouch)
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f); // режем кубик на пополам типо он присел
            transform.localPosition = new Vector3(0f, -0.25f, 0f); // Переносим тут же кубик ниже потому что иначе он остается висеть в воздухе
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Возвращаем все в исходные значения до приседания
            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}
