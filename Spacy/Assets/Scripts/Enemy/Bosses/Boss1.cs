using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject[] Bullets;
    public Vector3 movpos;
    public float Speed;
    private bool check;
    private BossHealth BossHealth;
    // Start is called before the first frame update
    void Start()
    {
        BossHealth = GetComponent<BossHealth>();
        check = true;
        StartCoroutine(BossControl());
    }
    
    IEnumerator BossControl()
    {
        movpos = new Vector3(transform.position.x, transform.position.y, 14.5f);
        while (transform.position != movpos)
        {
            transform.position = Vector3.MoveTowards(transform.position, movpos, Time.deltaTime * Speed);
            if (Vector3.Distance(transform.position, movpos) < 2.5 && check == true)
            {
                StartCoroutine(SlowDownTheSpeed());
                check = false;
            }
            yield return null;
        }
        while(true)
        {
            if (BossHealth.Health > 1350)
            {
                StartCoroutine(Phase1());
            }
            else if(BossHealth.Health > 650)
            {
                StopCoroutine(Phase1());
                StartCoroutine(Phase2());
            }
            else
            {
                StopCoroutine(Phase2());
                StartCoroutine(Phase3());
            }
            yield return null;
        }
    }
    private IEnumerator SlowDownTheSpeed()
    {
        while (Speed > 0.1)
        {
            Speed -= (0.22f);
            if (Speed < 0.1)
            {
                Speed = 0.1f;
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
    private IEnumerator Phase1()
    {
        yield return null;
    }
    private IEnumerator Phase2()
    {
        yield return null;
    }
    private IEnumerator Phase3()
    {
        yield return null;
    }
}
