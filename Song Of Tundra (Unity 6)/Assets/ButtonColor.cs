using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Color32 white, yellow;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData d)
    {
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = yellow;
    }
    public void OnPointerExit(PointerEventData d1)
    {
        Debug.Log("exit");
        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = white;
    }
}
