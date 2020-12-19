using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform[] routes;
    public GameObject[] Guns;
    public GameObject GunsParent;
    private Vector3 p0,p1,p2,p3;

    public GameObject shield;
    private Material shieldmat;

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
        shieldmat = shield.GetComponent<Renderer>().material;
    }
    void Start()
    {
        movtoCenter = new Vector3(transform.position.x, transform.position.y, 14.5f);
        BossHealth = GetComponent<BossHealth>();
        check = true;
        shieldmat.SetVector("_Position", new Vector3(8f, 0f, 0f));
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
            yield return StartCoroutine(WeakWeaponLefttoRight(1));
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(PowerWeapon());
        }
    }
    private IEnumerator Phase2()
    {
        yield return new WaitForSeconds(1f);
        anim.Play("GunsPowerOn", 0);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Shield());
        yield return new WaitForSeconds(8f);
        yield return StartCoroutine(MidWeapon(5));
        yield return StartCoroutine(ReturnToCenter(4f, 3));
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(PowerGuns());
        yield return new WaitForSeconds(1f);
        //yield return StartCoroutine(MidWeapon(5));
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
    private IEnumerator WeakWeaponLefttoRight(float wait)
    {
        anim.Play("Intro", 0);
        yield return new WaitForSeconds(3f);
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
        yield return new WaitForSeconds(wait);
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
        
    }
    private IEnumerator WeakWeaponRighttoLeft(float t)
    {
        anim.Play("Intro", 1);
        yield return new WaitForSeconds(2f);
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
        yield return StartCoroutine(ShootandRotateLaser());
        Vector3 pos = new Vector3(-3.5f, GunsParent.transform.localPosition.y, 2.5f);
        anim.Play("Guns");
        while(GunsParent.transform.localPosition!=pos)
        {
            GunsParent.transform.localPosition = Vector3.MoveTowards(GunsParent.transform.localPosition, pos, Time.deltaTime * 2.5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(WeakWeaponLefttoRight(3));
        yield return StartCoroutine(ShootandRotateLaser());
        pos = new Vector3(3.5f, GunsParent.transform.localPosition.y, 2.5f);
        anim.Play("Guns");
        while (GunsParent.transform.localPosition != pos)
        {
            GunsParent.transform.localPosition = Vector3.MoveTowards(GunsParent.transform.localPosition, pos, Time.deltaTime * 4f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ShootandRotateLaser());
        pos = new Vector3(0.0f, GunsParent.transform.localPosition.y, 0.0f);
        anim.Play("Guns");
        while (GunsParent.transform.localPosition != pos)
        {
            GunsParent.transform.localPosition = Vector3.MoveTowards(GunsParent.transform.localPosition, pos, Time.deltaTime * 2.1f);
            yield return null;
        }
    }
    private IEnumerator Shield()
    {
        anim.Play("Intro", 1);
        yield return new WaitForSeconds(2f);
        while (true)
        {
            shield.GetComponent<Collider>().enabled = false;
            if (shieldmat.GetVector("_Position").x == 0)
            {
                while (shieldmat.GetVector("_Position").x < 8)
                {
                    shieldmat.SetVector("_Position", new Vector3(shieldmat.GetVector("_Position").x + 0.2f, 0f, 0f));
                    yield return null;
                }
                shieldmat.SetVector("_Position", new Vector3(8f, 0f, 0f));
                yield return new WaitForSeconds(2f);
            }
            shieldmat.SetVector("_Position", new Vector3(8f, 0f, 0f));
            shield.GetComponent<BossShield>().Health = 400;
            shield.transform.localRotation = Quaternion.Euler(90f, transform.rotation.x, transform.rotation.z);
            anim.Play("Right",1);
            while (shieldmat.GetVector("_Position").x > 0)
            {
                shieldmat.SetVector("_Position", new Vector3(shieldmat.GetVector("_Position").x - 0.15f, 0f, 0f));
                yield return null;
            }
            shieldmat.SetVector("_Position", new Vector3(0f, 0f, 0f));
            Quaternion euler = Quaternion.Euler(135f, transform.rotation.x, transform.rotation.z);
            while(shield.transform.localRotation!=euler)
            {
                shield.transform.localRotation = Quaternion.RotateTowards(shield.transform.localRotation, euler, 150f * Time.deltaTime);
                yield return null;
            }
            shield.GetComponent<Collider>().enabled = true;

            yield return new WaitForSeconds(10f);

            if(shieldmat.GetVector("_Position").x == 0)
            {
                shield.GetComponent<Collider>().enabled = false;
                while (shieldmat.GetVector("_Position").x > -8)
                {
                    shieldmat.SetVector("_Position", new Vector3(shieldmat.GetVector("_Position").x - 0.2f, 0f, 0f));
                    yield return null;
                }
                shieldmat.SetVector("_Position", new Vector3(-8f, 0f, 0f));
                yield return new WaitForSeconds(2f);
            }
            shieldmat.SetVector("_Position", new Vector3(-8f, 0f, 0f));
            shield.GetComponent<BossShield>().Health = 400;
            shield.transform.localRotation = Quaternion.Euler(90f, transform.rotation.x, transform.rotation.z);
            anim.Play("Left",1);
            while (shieldmat.GetVector("_Position").x < 0)
            {
                shieldmat.SetVector("_Position", new Vector3(shieldmat.GetVector("_Position").x + 0.15f, 0f, 0f));
                yield return null;
            }
            shieldmat.SetVector("_Position", new Vector3(0f, 0f, 0f));
            euler = Quaternion.Euler(135f, transform.rotation.x, transform.rotation.z);
            while (shield.transform.localRotation != euler)
            {
                shield.transform.localRotation = Quaternion.RotateTowards(shield.transform.localRotation, euler, 150f * Time.deltaTime);
                yield return null;
            }
            shield.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(10f);
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
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, x, 3f * Time.deltaTime),
                transform.position.y,
                transform.position.z
                );
            yield return null;
        }
    }
    IEnumerator ShootandRotateLaser()
    {
        GunsParent.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        GunsParent.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Quaternion euler = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
        bool turnedOn = true;
        while (GunsParent.transform.rotation != euler)
        {
            timer += Time.deltaTime;
            GunsParent.transform.rotation = Quaternion.RotateTowards(GunsParent.transform.rotation, euler, 20f * Time.deltaTime);
            //GunsParent.transform.RotateAround(Vector3.zero, Vector3.up, 360f * Time.deltaTime / 8f);
            if (timer > 2.5 && turnedOn == true)
            {
                GunsParent.transform.GetChild(5).gameObject.SetActive(false);
                turnedOn = false;
                timer = 0;
            }
            else if (timer > 1 && turnedOn == false)
            {
                GunsParent.transform.GetChild(5).gameObject.SetActive(true);
                turnedOn = true;
                timer = 0;
            }

            yield return null;
        }
        euler = Quaternion.Euler(transform.rotation.x, -360f, transform.rotation.z);
        while (GunsParent.transform.rotation != euler)
        {
            timer += Time.deltaTime;
            GunsParent.transform.rotation = Quaternion.RotateTowards(GunsParent.transform.rotation, euler, 20f * Time.deltaTime);
            if (timer > 2.5 && turnedOn == true)
            {
                GunsParent.transform.GetChild(5).gameObject.SetActive(false);
                turnedOn = false;
                timer = 0;
            }
            else if (timer > 1 && turnedOn == false)
            {
                GunsParent.transform.GetChild(5).gameObject.SetActive(true);
                turnedOn = true;
                timer = 0;
            }

            yield return null;
        }
        GunsParent.transform.GetChild(5).gameObject.SetActive(false);
        GunsParent.transform.GetChild(4).gameObject.SetActive(false);
    }
    void StopAll()
    {
        StopAllCoroutines();
        transform.GetChild(4).gameObject.SetActive(false);
        StartCoroutine(BossControl(4f));
    }
}
