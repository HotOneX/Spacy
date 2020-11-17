using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    private BoxCollider col;
    private Transform HitEffect;
    private Transform HitCore;
    private Transform Parent;

    public int Damage;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        col = GetComponent<BoxCollider>();
        HitCore= transform.GetChild(3);
        HitEffect= transform.GetChild(4);
        Parent = GetComponentInParent<Transform>();
    }

    void FixedUpdate()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30f))
        {
             Debug.DrawRay(transform.position, hit.point, Color.red);
            
            if (Parent.CompareTag("Player"))
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.point.z - Parent.position.z));
                HitCore.position = new Vector3(HitCore.position.x, HitCore.position.y, lr.GetPosition(1).z + Parent.position.z);
                HitEffect.position = new Vector3(HitEffect.position.x, HitEffect.position.y, lr.GetPosition(1).z + Parent.position.z);
                EnemyHealth EnemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (EnemyHealth)
                    EnemyHealth.TakeDamage(Damage);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                lr.SetPosition(1, new Vector3(0, 0, -(hit.point.z - Parent.position.z)));
                HitCore.localPosition = new Vector3(HitCore.localPosition.x, HitCore.localPosition.y, (lr.GetPosition(1).z));
                HitEffect.localPosition = new Vector3(HitEffect.localPosition.x, HitEffect.localPosition.y, (lr.GetPosition(1).z));
                PlayerHealth PlayerHealth = hit.collider.GetComponent<PlayerHealth>();
                //if (PlayerHealth)
                  //  PlayerHealth.PlayerTakeDamage();
            }
            //Debug.Log(hit.point);
        }
        else
        {
            lr.SetPosition(1, new Vector3(0, 0, 30f));
            HitCore.position = new Vector3(HitCore.position.x, HitCore.position.y, lr.GetPosition(1).z );
            HitEffect.position = new Vector3(HitEffect.position.x, HitEffect.position.y, lr.GetPosition(1).z);
        }

        col.center = lr.GetPosition(1);
        
    }
}
