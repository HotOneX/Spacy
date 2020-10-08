using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Control : MonoBehaviour
{
    private SpawnController spawnController;
    private Vector3 objectPosition;
    public float Speed;
    private bool check;
    // Start is called before the first frame update
    void Start()
    {
        check = true;
        GameObject GameController = GameObject.FindWithTag("GameController");
        spawnController = GameController.GetComponent<SpawnController>();
        objectPosition = new Vector3(transform.position.x, 8.4f, spawnController.Enemy2ForwardPosition);
        StartCoroutine(Enemy2Movement());
    }

    private IEnumerator Enemy2Movement()
    {
        
        while(transform.position!=objectPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * Speed);
            if (Vector3.Distance(transform.position, objectPosition) < 3 && check==true)
            {
                StartCoroutine(SlowDownTheSpeed());
                check = false;
            }
            yield return null;
        }
    }
    private IEnumerator SlowDownTheSpeed()
    {
        while (Speed > 0.1)
        {
            Speed -= (0.2f);
            if (Speed < 0.1)
            {
                Speed = 0.2f;
                yield break;
            }
            yield return new WaitForSeconds(0.12f);
        }
    }
}
