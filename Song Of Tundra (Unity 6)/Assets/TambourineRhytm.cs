using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TambourineRhytm : MonoBehaviour
{
    // Start is called before the first frame update
    private string accuracy = "bad"; // значение точности попадания
    public bool newHit = true; // переменная для проверки был ли совершен удар в текущей итерации
    public GameObject light; // мигающий свет над врагом
    public TMP_Text resultText; // результат попадания
    public List<int> melodyList = new List<int>(); // список для задания ритма мелодии. 1 - удар есть, 0 - удара нет
    private int noteCount = 0; // номер текущей ноты

    void Start()
    {
        NextNote();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void NextNote(){ // проверяем нужно ли делать удар в текущей итерации
        if(melodyList[noteCount % melodyList.Count] == 1){
            StartCoroutine(Rhytm());
        }
        else{
            StartCoroutine(Miss());
        }
        noteCount += 1;
    }
    public IEnumerator Rhytm(){ // изменяем значение accuracy в зависимости от времени
        newHit = true;
        accuracy = "bad";
        yield return new WaitForSeconds(0.2f);
        accuracy = "okay";
        yield return new WaitForSeconds(0.15f);
        light.SetActive(true);
        accuracy = "super";
        yield return new WaitForSeconds(0.15f);
        light.SetActive(false);
        accuracy = "okay";
        yield return new WaitForSeconds(0.2f);
        accuracy = "bad";
        NextNote();
    }
    public IEnumerator Miss()
    {
        yield return new WaitForSeconds(0.7f); // пропуск удара
        NextNote();
    }
    public void Hit() // выполняется при нажатии на кнопку, проверяем текущее значение accuracy
    {
        if(!newHit || accuracy == "bad")
        {
            StartCoroutine(TextShow("Неудачно"));
        }
        else if(accuracy == "okay")
        {
            StartCoroutine(TextShow("Почти в такт"));
        }
        else if(accuracy == "super")
        {
            StartCoroutine(TextShow("В такт"));
        }
        newHit = false;
    }
    public IEnumerator TextShow(string result) // выводим результат на экран после удара
    {
        resultText.text = result;
        yield return new WaitForSeconds(0.5f);
        resultText.text = ""; 
    }
}
