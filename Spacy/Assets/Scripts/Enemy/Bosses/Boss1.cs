using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform[] routes;
    public GameObject[] Guns;
    private Vector3 p0,p1,p2,p3;

    private BossHealth BossHealth;
    public GameObject[] Bullets;
    public Transform ShotSpawn;
    

    private Vector3 movtoCenter;
    private float BossMovetoX;
    private Vector3 LeftGunMovPos;
    private Vector3 RightGunMovPos;
    private float timer;
    public float weakGunFireRate;

    public float Speed;

    private bool check;
    private float n;
    

    private GameObject Player;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        BossHealth = GetComponent<BossHealth>();
        check = true;
        StartCoroutine(BossControl());
    }

    IEnumerator BossControl()
    {
        Coroutine lastRoutine = null;
        movtoCenter = new Vector3(transform.position.x, transform.position.y, 14.5f);
        while (transform.position != movtoCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, movtoCenter, Time.deltaTime * Speed);
            if (Vector3.Distance(transform.position, movtoCenter) < 2.5 && check == true)
            {
                StartCoroutine(SlowDownTheSpeed());
                check = false;
            }
            yield return null;
        }
        lastRoutine = StartCoroutine(Phase1());
        yield return new WaitUntil(() => BossHealth.Health < 6000);
        StopCoroutine(lastRoutine);
        lastRoutine = StartCoroutine(Phase2());
        yield return new WaitUntil(() => BossHealth.Health < 3000);
        StopCoroutine(lastRoutine);
        StartCoroutine(Phase3());
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
        yield return StartCoroutine(MidWeapon(10));
        yield return StartCoroutine(ReturnToCenter(4f, 3));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(WeakWeapon(0f));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(PowerWeapon());
        n = 0;
    }
    private IEnumerator Phase2()
    {
        yield return null;
    }
    private IEnumerator Phase3()
    {
        yield return null;
    }

    private IEnumerator MidWeapon(int n)
    {
        while (n > 0)
        {
            BossMovetoX = Player.transform.position.x;
            while (transform.position.x != BossMovetoX)
            {
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, BossMovetoX, 4f * Time.deltaTime), transform.position.y, transform.position.z);
                yield return null;
                if(Mathf.Abs(transform.position.x-BossMovetoX)>2)
                    BossMovetoX = Player.transform.position.x;
            }
            Instantiate(Bullets[1], ShotSpawn.position, ShotSpawn.rotation);
            n--;
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator WeakWeapon(float n)
    {
        LeftGunMovPos = new Vector3(-3f, 8.2f,10.4f);
        while(Guns[0].transform.position!=LeftGunMovPos)
        {
            Guns[0].transform.position = Vector3.MoveTowards(Guns[0].transform.position, LeftGunMovPos, 3f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        timer = weakGunFireRate;
        #region curve points
        p0 = routes[0].GetChild(0).position;
        p1 = routes[0].GetChild(1).position;
        p2 = routes[0].GetChild(2).position;
        p3 = routes[0].GetChild(3).position;
        #endregion
        while (n < 1)
        {
            timer += Time.deltaTime;
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                timer = 0;
            }
            n += Time.deltaTime * 0.5f;

            Guns[0].transform.position = Mathf.Pow(1 - n, 3) * p0 +
                3 * Mathf.Pow(1 - n, 2) * n * p1 +
                3 * (1 - n) * Mathf.Pow(n, 2) * p2 +
                Mathf.Pow(n, 3) * p3;
            Quaternion euler = Quaternion.Euler(transform.rotation.x, -210, transform.rotation.z);
            Guns[0].transform.rotation = Quaternion.RotateTowards(Guns[0].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        n = 0;
        while (n < 1)
        {
            timer += Time.deltaTime;
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                timer = 0;
            }
            n += Time.deltaTime * 0.5f;

            Guns[0].transform.position = Mathf.Pow(1 - n, 3) * p3 +
                3 * Mathf.Pow(1 - n, 2) * n * p2 +
                3 * (1 - n) * Mathf.Pow(n, 2) * p1 +
                Mathf.Pow(n, 3) * p0;
            Quaternion euler = Quaternion.Euler(transform.rotation.x, -150, transform.rotation.z);
            Guns[0].transform.rotation = Quaternion.RotateTowards(Guns[0].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator PowerWeapon(int n=2)
    {
        while (n > 0)
        {
            transform.GetChild(4).gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            transform.GetChild(4).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(TransformtoX(5.2f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(4).gameObject.SetActive(true);
            yield return StartCoroutine(TransformtoX(-3f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(4).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(TransformtoX(-5.2f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(4).gameObject.SetActive(true);
            yield return StartCoroutine(TransformtoX(3f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(4).gameObject.SetActive(false);
            yield return StartCoroutine(ReturnToCenter(4f, 3));
            n--;
        }
    }
    IEnumerator ReturnToCenter(float Speed,float distance)
    {
        check = true;
        while (transform.position != movtoCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, movtoCenter, Time.deltaTime * Speed);
            if (Vector3.Distance(transform.position, movtoCenter) < distance && check == true)
            {
                StartCoroutine(SlowDownTheSpeed());
                check = false;
            }
            yield return null;
        }
    }
    IEnumerator TransformtoX(float x)
    {
        while (transform.position.x != x)
        {
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, x, 3f * Time.deltaTime), transform.position.y, transform.position.z);
            yield return null;
        }
    }
}
