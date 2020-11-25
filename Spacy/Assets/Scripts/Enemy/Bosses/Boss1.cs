using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform[] routes;
    public GameObject[] Guns;
    public GameObject GunsParent;
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
    public Animator anim;

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
        movtoCenter = new Vector3(transform.position.x, transform.position.y, 14.5f);
        BossHealth = GetComponent<BossHealth>();
        check = true;
        StartCoroutine(BossControl(Speed));
    }

    IEnumerator BossControl(float Speed)
    {
        Coroutine lastRoutine = null;
        //movtoCenter = new Vector3(transform.position.x, transform.position.y, 14.5f);
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
        if (BossHealth.Health > 8000)
        {
            lastRoutine = StartCoroutine(Phase1());
            yield return new WaitUntil(() => BossHealth.Health < 8000);
            StopAll();
        }
        else if (BossHealth.Health > 3000)
        {
            lastRoutine = StartCoroutine(Phase2());
            yield return new WaitUntil(() => BossHealth.Health < 3000);
            StopAll();
        }
        else
        {
            lastRoutine = StartCoroutine(Phase3());
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
        while (true)
        {
            yield return StartCoroutine(MidWeapon(5));
            yield return StartCoroutine(ReturnToCenter(4f, 3));
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(WeakWeaponLefttoRight());
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(PowerWeapon());
            n = 0;
        }
    }
    private IEnumerator Phase2()
    {
        yield return new WaitForSeconds(1f);
        /*while (Guns[5].transform.localPosition != new Vector3(2f, 0.2f, 3f))
        {
            Guns[2].transform.localPosition = Vector3.MoveTowards(Guns[2].transform.localPosition, new Vector3(-2f, 0.2f, 4.1f), 3f * Time.deltaTime);
            Guns[3].transform.localPosition = Vector3.MoveTowards(Guns[3].transform.localPosition, new Vector3(2f, 0.2f, 4.1f), 3f * Time.deltaTime);
            Guns[4].transform.localPosition = Vector3.MoveTowards(Guns[4].transform.localPosition, new Vector3(-2f, 0.2f, 3f), 3f * Time.deltaTime);
            Guns[5].transform.localPosition = Vector3.MoveTowards(Guns[5].transform.localPosition, new Vector3(2f, 0.2f, 3f), 3f * Time.deltaTime);
            yield return null;
        }*/
        yield return StartCoroutine(PowerGuns());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(MidWeapon(5));
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
                if(Mathf.Abs(transform.position.x-BossMovetoX)>1)
                    BossMovetoX = Player.transform.position.x;
            }
            Instantiate(Bullets[1], ShotSpawn.position, ShotSpawn.rotation);
            n--;
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator WeakWeaponLefttoRight()
    {
        anim.enabled = false;
        LeftGunMovPos = new Vector3(-3f, 0.2f,2.5f);
        while(Guns[0].transform.localPosition!=LeftGunMovPos)
        {
            Guns[0].transform.localPosition = Vector3.MoveTowards(Guns[0].transform.localPosition, LeftGunMovPos, 3f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        anim.enabled = true;
        timer = weakGunFireRate;
        anim.Play("MainGun1");
        yield return null;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MainGun1"))
        {
            timer += Time.deltaTime;
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                timer = 0;
            }
            Quaternion euler = Quaternion.Euler(transform.rotation.x, 150, transform.rotation.z);
            Guns[0].transform.rotation = Quaternion.RotateTowards(Guns[0].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        anim.Play("MainGun2");
        yield return null;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MainGun2"))
        {
            timer += Time.deltaTime;
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                timer = 0;
            }
            Quaternion euler = Quaternion.Euler(transform.rotation.x, -150, transform.rotation.z);
            Guns[0].transform.rotation = Quaternion.RotateTowards(Guns[0].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
        /*
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
            Quaternion euler = Quaternion.Euler(transform.rotation.x, 150, transform.rotation.z);
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
        }*/
    }
    private IEnumerator WeakWeaponRighttoLeft()
    {
        anim.enabled = false;
        RightGunMovPos = new Vector3(3f, 0.2f, 2.5f);
        while (Guns[1].transform.localPosition != RightGunMovPos)
        {
            Guns[1].transform.localPosition = Vector3.MoveTowards(Guns[1].transform.localPosition, RightGunMovPos, 3f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        anim.enabled = true;
        timer = weakGunFireRate;
        anim.Play("MainGun3");
        yield return null;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MainGun3"))
        {
            timer += Time.deltaTime;
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[1].transform.position, Guns[1].transform.rotation);
                timer = 0;
            }
            Quaternion euler = Quaternion.Euler(transform.rotation.x, 150, transform.rotation.z);
            Guns[1].transform.rotation = Quaternion.RotateTowards(Guns[1].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        timer = weakGunFireRate;
        anim.Play("MainGun4");
        yield return null;
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MainGun4"))
        {
            timer += Time.deltaTime;
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[1].transform.position, Guns[1].transform.rotation);
                timer = 0;
            }
            Quaternion euler = Quaternion.Euler(transform.rotation.x, -150, transform.rotation.z);
            Guns[1].transform.rotation = Quaternion.RotateTowards(Guns[1].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator PowerWeapon(int n=2)
    {
        while (n > 0)
        {
            transform.GetChild(5).gameObject.SetActive(true);
            yield return new WaitForSeconds(7f);
            transform.GetChild(5).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(TransformtoX(5.2f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(5).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(TransformtoX(-3f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(5).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(TransformtoX(-5.2f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(5).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(TransformtoX(3f));
            yield return new WaitForSeconds(1f);
            transform.GetChild(5).gameObject.SetActive(false);
            yield return StartCoroutine(ReturnToCenter(4f, 3));
            n--;
        }
    }
    private IEnumerator PowerGuns()
    {
        anim.Play("GunsIntro");
        yield return new WaitForSeconds(2f);
        GunsParent.transform.GetChild(4).gameObject.SetActive(true);
        GunsParent.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Quaternion euler = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
        Debug.Log(GunsParent.transform.rotation);
        Debug.Log(euler);
        while (GunsParent.transform.rotation!=euler)
        {
            GunsParent.transform.rotation = Quaternion.RotateTowards(GunsParent.transform.rotation, euler, 20f * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator shield()
    {
        anim.enabled = false;
        LeftGunMovPos = new Vector3(3f, 0.2f, 2.5f);
        while (Guns[1].transform.localPosition != LeftGunMovPos)
        {
            Guns[1].transform.localPosition = Vector3.MoveTowards(Guns[1].transform.localPosition, LeftGunMovPos, 3f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        anim.enabled = true;
        anim.Play("MainGun3");
        yield return new WaitForSeconds(1f);
        anim.Play("MainGun4");
        yield return null;
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
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, x, 3f * Time.deltaTime),
                transform.position.y,
                transform.position.z
                );
            yield return null;
        }
    }
    void StopAll()
    {
        StopAllCoroutines();
        transform.GetChild(4).gameObject.SetActive(false);
        StartCoroutine(BossControl(4f));
    }
}
