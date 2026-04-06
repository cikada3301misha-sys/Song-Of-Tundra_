using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float mana = 400f;
    public int health = 2;
    public RectTransform manaStrip;
    public List<GameObject> healthIcons = new List<GameObject>();

    [Header("Freezing System")]
    [SerializeField] private float freezeTickInterval = 3f;
    [SerializeField] private float warmTickInterval = 1.5f;

    private int maxHealth;
    private int activeWarmZones;

    void Start()
    {
        maxHealth = healthIcons.Count > 0 ? healthIcons.Count : Mathf.Max(health, 1);
        health = Mathf.Clamp(health, 0, maxHealth);
        RefreshHealthIcons();

        StartCoroutine(ManaRecovery());
        StartCoroutine(TemperatureHealthRoutine());
    }

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

    IEnumerator TemperatureHealthRoutine()
    {
        while (true)
        {
            if (activeWarmZones > 0)
            {
                HealthGain();
                yield return new WaitForSeconds(warmTickInterval);
            }
            else
            {
                HealthLose();
                yield return new WaitForSeconds(freezeTickInterval);
            }
        }
    }

    public void HealthLose()
    {
        if (health > 0)
        {
            health -= 1;
            RefreshHealthIcons();
        }
    }

    public void HealthGain()
    {
        if (health < maxHealth)
        {
            health += 1;
            RefreshHealthIcons();
        }
    }

    public void EnterWarmZone()
    {
        activeWarmZones += 1;
    }

    public void ExitWarmZone()
    {
        activeWarmZones = Mathf.Max(0, activeWarmZones - 1);
    }

    private void RefreshHealthIcons()
    {
        if (healthIcons == null || healthIcons.Count == 0)
        {
            return;
        }

        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (healthIcons[i] != null)
            {
                healthIcons[i].SetActive(i < health);
            }
        }
    }
}
