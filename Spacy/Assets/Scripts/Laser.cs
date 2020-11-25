using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    private BoxCollider col;
    public Transform HitEffect;
    public Transform HitCore;
    private Transform Parent;
    private bool Checked;
    private float eDistance;
    private float zAxis;

    public int Damage;

    void Start()
    {
        Checked = false;
        lr = GetComponent<LineRenderer>();
        col = GetComponent<BoxCollider>();
        Parent = GetComponentInParent<Transform>();
    }
    private void OnEnable()
    {
        StartCoroutine(StartShoot());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        lr.SetPosition(1, new Vector3(0, 0, 0));
    }
    IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(4f);
        RaycastHit hit;
        for (float i = 0; i <= 30; i++)
        {
            lr.SetPosition(1, new Vector3(0, 0, i));
            HitCore.localPosition = new Vector3(HitCore.localPosition.x, HitCore.localPosition.y, lr.GetPosition(1).z);
            HitEffect.localPosition = new Vector3(HitEffect.localPosition.x, HitEffect.localPosition.y, lr.GetPosition(1).z);
            yield return new WaitForFixedUpdate();
            if (Physics.Raycast(transform.position, transform.forward, out hit, 30f))
            {
                if (i>=(-(hit.point.z - Parent.position.z)))
                   break;
            }
        }
        Checked = true;
        while (true)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 30f))
            {
                Debug.DrawRay(transform.position, hit.point, Color.red);
                zAxis = hit.point.z - Parent.position.z;//injori lazer ro mabdae mokhtasat darnazar migirim na safharo va bar asase makane lazer, makane Player ro behemon mide.
                if (Parent.CompareTag("Player"))
                {
                    lr.SetPosition(1, new Vector3(0, 0, zAxis));
                    HitCore.position = new Vector3(HitCore.position.x, HitCore.position.y, lr.GetPosition(1).z + Parent.position.z);
                    HitEffect.position = new Vector3(HitEffect.position.x, HitEffect.position.y, lr.GetPosition(1).z + Parent.position.z);
                    EnemyHealth EnemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (EnemyHealth)
                        EnemyHealth.TakeDamage(Damage);
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    eDistance = Mathf.Sqrt((zAxis * zAxis) + (hit.point.x * hit.point.x));
                    lr.SetPosition(1, new Vector3(0, 0, (eDistance )));
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
                HitCore.localPosition = new Vector3(HitCore.localPosition.x, HitCore.localPosition.y, lr.GetPosition(1).z);
                HitEffect.localPosition = new Vector3(HitEffect.localPosition.x, HitEffect.localPosition.y, lr.GetPosition(1).z);
            }

            col.center = lr.GetPosition(1);
            yield return null;
        }
    }
}
