using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TambourineRhythm : MonoBehaviour
{
    // Start is called before the first frame update
    private string _accuracy = "bad"; // значение точности попадания
    public bool newHit = true; // переменная для проверки был ли совершен удар в текущей итерации
    public GameObject light, greenlight, yellowlight, redlight; // мигающий свет над врагом
    public TMP_Text resultText; // результат попадания
    public List<int> melodyList = new List<int>(); // список для задания ритма мелодии. 1 - удар есть, 0 - удара нет
    private int _noteCount = 0; // номер текущей ноты
    public EnemyLives NowEnemy;

    void Start()
    {
        NextNote();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void NextNote()
    {
        // проверяем нужно ли делать удар в текущей итерации
        StartCoroutine(melodyList[_noteCount % melodyList.Count] == 1 ? Rhythm() : Miss());
        _noteCount += 1;
    }
    public IEnumerator Rhythm(){ // изменяем значение accuracy в зависимости от времени
        newHit = true;
        _accuracy = "bad";
        yield return new WaitForSeconds(0.2f);
        _accuracy = "okay";
        yield return new WaitForSeconds(0.15f);
        light.SetActive(true);
        _accuracy = "super";
        yield return new WaitForSeconds(0.15f);
        light.SetActive(false);
        _accuracy = "okay";
        yield return new WaitForSeconds(0.2f);
        _accuracy = "bad";
        NextNote();
    }
    public IEnumerator Miss()
    {
        yield return new WaitForSeconds(0.7f); // пропуск удара
        NextNote();
    }
    public void Hit() // выполняется при нажатии на кнопку, проверяем текущее значение accuracy
    {
        if(!newHit || _accuracy == "bad")
        {
            StartCoroutine(TextShow("Неудачно", redlight));
        }
        else switch (_accuracy)
        {
            case "okay":
                StartCoroutine(TextShow("Почти в такт",yellowlight));
                NowEnemy.DecreaseShield(0.2f); // уменьшение щита у врага
                break;
            case "super":
                StartCoroutine(TextShow("В такт", greenlight));
                NowEnemy.DecreaseShield(0.1f);
                break;
        }
        newHit = false;
    }
    public IEnumerator TextShow(string result, GameObject resultLight) // выводим результат на экран после удара
    {
        resultText.text = result;
        resultLight.SetActive(true); // включение света, который соответствует результату
        yield return new WaitForSeconds(0.5f);
        resultText.text = "";
        resultLight.SetActive(false); 
    }
}
