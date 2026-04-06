using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BonfireWarmZone : MonoBehaviour
{
    private readonly Dictionary<PlayerStats, int> playerContacts = new Dictionary<PlayerStats, int>();

    private void Reset()
    {
        Collider zoneCollider = GetComponent<Collider>();
        zoneCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponentInParent<PlayerStats>();
        if (playerStats == null)
        {
            return;
        }

        if (!playerContacts.ContainsKey(playerStats))
        {
            playerContacts[playerStats] = 0;
        }

        playerContacts[playerStats] += 1;
        if (playerContacts[playerStats] == 1)
        {
            playerStats.EnterWarmZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerStats playerStats = other.GetComponentInParent<PlayerStats>();
        if (playerStats == null || !playerContacts.ContainsKey(playerStats))
        {
            return;
        }

        playerContacts[playerStats] -= 1;
        if (playerContacts[playerStats] <= 0)
        {
            playerContacts.Remove(playerStats);
            playerStats.ExitWarmZone();
        }
    }

    private void OnDisable()
    {
        foreach (PlayerStats playerStats in playerContacts.Keys)
        {
            if (playerStats != null)
            {
                playerStats.ExitWarmZone();
            }
        }

        playerContacts.Clear();
    }
}
