using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public GameObject player;
    public int maximumDist;
    public bool move = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < maximumDist && move){
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
    }
}
