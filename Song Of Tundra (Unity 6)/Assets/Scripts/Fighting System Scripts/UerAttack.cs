using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UerAttack : MonoBehaviour
{
    public GameObject player;
    private bool attack = false, reloading = false, jerk = false, stun = false;
    private Vector3 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Rigidbody>().linearVelocity.magnitude > 0.1f && Vector3.Distance(transform.position, player.transform.position) < 12 && !attack)
        {
            attack = true;
            GetComponent<EnemyDrifting>().enabled = false;
            GetComponent<EnemyMove>().enabled = true;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        if(attack && !jerk &&  Vector3.Distance(transform.position, player.transform.position) > 5 && !reloading)
        {
            StartCoroutine(Jerk());
        }
        if(Vector3.Distance(transform.position, player.transform.position) < 3 && !reloading && !jerk && attack)
        {
            Debug.Log("swing attack");
            StartCoroutine(Swing());
        }
        if(GetComponent<EnemyLives>().shieldNum == 0 && !stun)
        {
            StartCoroutine(Stun());
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && jerk)
        {
            player.GetComponent<PlayerStats>().HealthLose();
        }
    }
    IEnumerator Swing()
    {
        reloading = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(0.4f);
        if(Vector3.Distance(transform.position, player.transform.position) < 3){
            player.GetComponent<PlayerStats>().HealthLose();
        }
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        yield return new WaitForSeconds(1.6f);
        reloading = false;
    }
    IEnumerator Jerk()
    {
        transform.LookAt(player.transform.position);
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        jerk = true;
        playerPos = player.transform.position;
        float far = Vector3.Distance(playerPos, transform.position) * 10 + 60;
        for(int i = 0; i < 40; i++)
        {
            if (stun)
            {
                break;
            }
            transform.position -= transform.forward * 0.1f;
            yield return new WaitForSeconds(0.015f);
        }
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i < far; i++)
        {
            transform.position += transform.forward * 0.1f;
            yield return new WaitForSeconds(0.01f);
            if(stun)
            {
                break;
            }
        }
        
        if(!stun){
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        }
        yield return new WaitForSeconds(4f);
        jerk = false;
    }
    IEnumerator Stun()
    {
        stun = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(3f);
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
        stun = false;
        GetComponent<EnemyLives>().shieldNum = 1f;
        GetComponent<EnemyLives>().shieldStrip.sizeDelta = new Vector3(1,  GetComponent<EnemyLives>().shieldStrip.rect.height);
    }
}
