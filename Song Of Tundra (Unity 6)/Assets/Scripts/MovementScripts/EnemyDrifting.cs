using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrifting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(RandomRotate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move()
    {
        while(enabled){
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator RandomRotate()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1f);
            int angle = UnityEngine.Random.Range(-180, 180);
            if(angle > 0){
                for(int i = 0; i < angle; i++)
                {
                    transform.Rotate(0, 1, 0);
                    yield return new WaitForSeconds(0.03f);
                    if (!enabled)
                    {
                        break;
                    }
                }
            }
            else
            {
                for(int i = 0; i < -angle; i++)
                {
                    transform.Rotate(0, -1, 0);
                    yield return new WaitForSeconds(0.03f);
                    if (!enabled)
                    {
                        break;
                    }
                }
            }
        }
    }
    public void StopDrift()
    {
        StopCoroutine(Move());
        StopCoroutine(RandomRotate());
    }
}
