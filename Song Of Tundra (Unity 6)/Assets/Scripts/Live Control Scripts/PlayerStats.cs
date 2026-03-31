using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float mana = 400f;
    public int health = 2;
    public RectTransform manaStrip;
    public List<GameObject> healthIcons = new List<GameObject>();
    void Start()
    {
        StartCoroutine(ManaRecovery());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ManaSpend()
    {
        mana -= 10f;
        manaStrip.sizeDelta = new Vector3(manaStrip.rect.width - 10f, manaStrip.rect.height);
    }
    IEnumerator ManaRecovery()
    {
        while (true)
        {
            if(mana < 400f)
            {
                mana += 1f;
                manaStrip.sizeDelta = new Vector3(manaStrip.rect.width + 1f, manaStrip.rect.height);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void HealthLose()
    {
        if(health != 0){
            health -= 1;
            healthIcons[health].SetActive(false);
        }
    }
}
