using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TambourineRhytm : MonoBehaviour
{
    // Start is called before the first frame update
    private string accuracy = "bad";
    public bool newHit = true;
    public GameObject light;
    public TMP_Text resultText;
    public List<int> melodyList = new List<int>();
    private int noteCount = 0;

    void Start()
    {
        NextNote();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void NextNote(){
        if(melodyList[noteCount % melodyList.Count] == 1){
            StartCoroutine(Rhytm());
        }
        else{
            StartCoroutine(Miss());
        }
        Debug.Log(melodyList[noteCount % melodyList.Count]);
        noteCount += 1;
    }
    public IEnumerator Rhytm(){
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
        yield return new WaitForSeconds(0.7f);
        NextNote();
    }
    public void Hit()
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
    public IEnumerator TextShow(string result)
    {
        resultText.text = result;
        yield return new WaitForSeconds(0.5f);
        resultText.text = ""; 
    }
}
