using UnityEngine;

public class EnemyLives : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float shieldNum = 1f, livesNum = 1f;
    public RectTransform shieldStrip, healthStrip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DecreaseShield(float damage)
    {
        shieldNum -= damage; // уменьшение значения щита
        shieldStrip.sizeDelta = new Vector3(shieldStrip.rect.width - damage, shieldStrip.rect.height); // укорачивание полоски щита
        if(shieldNum < 0.001f) // фикс погрешности в вычитании дробных чисел (0.2-0.2 по какой то причине не всегда равно нулю)
        {
            shieldNum = 0;
        }
    }
    public void DecreaseLives(float damage) // тоже самое только для жизней
    {
        if(shieldNum <= 0) // проверка уничтожен ли щит
        {
            livesNum -= damage;
            healthStrip.sizeDelta = new Vector3(healthStrip.rect.width - damage, healthStrip.rect.height);
            if(livesNum < 0.001f)
            {
                livesNum = 0;
            }
        }
    }
}
