using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform[] routes;
    public GameObject[] Guns;
    public GameObject GunsParent;
    public GameObject GunsParent2;
    public GameObject GunsParent3;

    public GameObject shield;
    private Material shieldmat;
    private Coroutine checkshield;

    private BossHealth BossHealth;
    public GameObject[] Bullets;
    public Transform ShotSpawn;
    

    private Vector3 movtoCenter;
    private float BossMovetoX;
    private float timer;
    public float weakGunFireRate;

    public float Speed;
    public Animator anim;

    private bool check;
    

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
        if (BossHealth.Health > 9000)
        {
            lastRoutine = StartCoroutine(Phase1());
            yield return new WaitUntil(() => BossHealth.Health < 9000);
            StopAll();
        }
        else if (BossHealth.Health > 4000)
        {
            lastRoutine = StartCoroutine(Phase2());
            yield return new WaitUntil(() => BossHealth.Health < 4000);
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
            yield return StartCoroutine(ReturnToCenter(3, 4f));
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(WeakWeaponLefttoRight(1));
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(PowerWeapon());
        }
    }
    private IEnumerator Phase2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Guns[2].transform.localPosition.z != 4.1f)
                anim.Play("GunsPowerOn", 0);
            yield return new WaitForSeconds(2f);
            checkshield = StartCoroutine(Shield());
            yield return new WaitForSeconds(6f);
            yield return StartCoroutine(MidWeapon(5));
            yield return StartCoroutine(ReturnToCenter(3, 4f));
            yield return new WaitForSeconds(4f);
            yield return StartCoroutine(PowerGuns());
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator Phase3()
    {
        while (true)
        {
            yield return StartCoroutine(GunsMidWeapon());
            checkshield = StartCoroutine(Shield());
            yield return new WaitForSeconds(3f);
            StartCoroutine(CircleGuns(10,5f));
            yield return StartCoroutine(PowerWeapon(2,5f));
            yield return new WaitForSeconds(3f);
            if (shieldmat.GetVector("_Position").x != 0)
                yield return new WaitForSeconds(1f);
            if(anim.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
                StopCoroutine(checkshield);
            if (Guns[1].transform.position != new Vector3(3, 0.55f, -0.3f))
            {
                anim.Play("Left", 1);
                yield return new WaitForSeconds(2f);
            }
            if (Guns[0].transform.localPosition.z != 1)
                anim.Play("Intro", 2);
            if (Guns[1].transform.localPosition.z != 0)
                anim.Play("Intro", 1);
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(DoubleGuns());
            yield return null;
        }
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
        if(Guns[0].transform.localPosition.z!=1)
            anim.Play("Intro", 2);
        yield return new WaitForSeconds(2f);
        float timer;
        anim.Play("MainGun1",2);
        yield return null;
        for (timer = weakGunFireRate; anim.GetCurrentAnimatorStateInfo(2).IsName("MainGun1"); timer += Time.deltaTime)
        {
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                timer = 0;
            }
            yield return null;
        }
        yield return new WaitForSeconds(wait);
        anim.Play("MainGun2",2);
        yield return null;
        //euler = Quaternion.Euler(transform.rotation.x, -150, transform.rotation.z);
        for (timer = weakGunFireRate; anim.GetCurrentAnimatorStateInfo(2).IsName("MainGun2"); timer += Time.deltaTime)
        {
            if (timer >= weakGunFireRate)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                timer = 0;
            }
            //Guns[0].transform.rotation = Quaternion.RotateTowards(Guns[0].transform.rotation, euler, 40f * Time.deltaTime);
            yield return null;
        }
        
    }
    private IEnumerator DoubleGuns()
    {
        anim.Play("doubleGuns", 2);
        yield return null;
        float timer;
        for(timer=0.3f; anim.GetCurrentAnimatorStateInfo(2).IsName("doubleGuns"); timer+=Time.deltaTime)
        {
            if (timer >= 0.18)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                Instantiate(Bullets[0], Guns[1].transform.position, Guns[1].transform.rotation);
                timer = 0;
            }
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        anim.Play("doubleGunsRev", 2);
        yield return null;
        for (timer = 0.3f; anim.GetCurrentAnimatorStateInfo(2).IsName("doubleGunsRev"); timer += Time.deltaTime)
        {
            if (timer >= 0.18)
            {
                Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                Instantiate(Bullets[0], Guns[1].transform.position, Guns[1].transform.rotation);
                timer = 0;
            }
            yield return null;
        }
    }
    private IEnumerator CircleGuns(int n=1,float wait=1)
    {
        while (n > 0)
        {
            anim.Play("MainGun1Circle", 2);
            yield return null;
            float timer;
            for (timer = 0.3f; anim.GetCurrentAnimatorStateInfo(2).IsName("MainGun1Circle"); timer += Time.deltaTime)
            {
                if (timer >= 0.1)
                {
                    Instantiate(Bullets[0], Guns[0].transform.position, Guns[0].transform.rotation);
                    timer = 0;
                }
                yield return null;
            }
            yield return new WaitForSeconds(wait);
            n--;
        }
    }
    private IEnumerator PowerWeapon(int n = 2, float speed = 3f)
    {
        while (n > 0)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(7f);
            transform.GetChild(3).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(TransformtoX(5.2f, speed));
            yield return new WaitForSeconds(1f);
            transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(TransformtoX(-3f, speed));
            yield return new WaitForSeconds(1f);
            transform.GetChild(3).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(TransformtoX(-5.2f, speed));
            yield return new WaitForSeconds(1f);
            transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            yield return StartCoroutine(TransformtoX(3f, speed));
            yield return new WaitForSeconds(1f);
            transform.GetChild(3).gameObject.SetActive(false);
            yield return StartCoroutine(ReturnToCenter(3, 4f));
            n--;
        }
    }
    private IEnumerator PowerGuns()
    {
        if (Guns[2].transform.localPosition.z != 0)
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
        StartCoroutine(WeakWeaponLefttoRight(3));
        yield return StartCoroutine(ShootandRotateLaser());
        pos = new Vector3(0.0f, GunsParent.transform.localPosition.y, 0.0f);
        anim.Play("Guns");
        while (GunsParent.transform.localPosition != pos)
        {
            GunsParent.transform.localPosition = Vector3.MoveTowards(GunsParent.transform.localPosition, pos, Time.deltaTime * 2.1f);
            yield return null;
        }
        anim.Play("GunsIntroRev");
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator Shield()
    {
        if (Guns[1].transform.localPosition.z != 0)
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
    private IEnumerator GunsMidWeapon(int n=5)
    {
        
        Guns[2].transform.SetParent(GunsParent2.transform);
        Guns[3].transform.SetParent(GunsParent2.transform);
        Guns[4].transform.SetParent(GunsParent3.transform);
        Guns[5].transform.SetParent(GunsParent3.transform);
        anim.Rebind();
        anim.Play("GunsMidWeapon");
        while (GunsParent2.transform.localPosition.x > -7)
        {
            GunsParent2.transform.localPosition = Vector3.MoveTowards(GunsParent2.transform.localPosition, new Vector3(-7, 0, 0), Time.deltaTime * 5f);
            GunsParent2.transform.localRotation = Quaternion.RotateTowards(GunsParent2.transform.localRotation, Quaternion.Euler(0f, 135f, 0f), 100f * Time.deltaTime);
            GunsParent3.transform.localPosition = Vector3.MoveTowards(GunsParent3.transform.localPosition, new Vector3(7, 0, 0), Time.deltaTime * 5f);
            GunsParent3.transform.localRotation = Quaternion.RotateTowards(GunsParent3.transform.localRotation, Quaternion.Euler(0f, -135f, 0f), 100f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        float timer = 0;
        while (n > 0)
        {
            while (timer < 3)
            {
                GunsParent2.transform.rotation = Quaternion.RotateTowards(GunsParent2.transform.rotation, Quaternion.LookRotation(Player.transform.position-GunsParent2.transform.position), Time.deltaTime * 15f);
                GunsParent3.transform.rotation = Quaternion.RotateTowards(GunsParent3.transform.rotation, Quaternion.LookRotation(Player.transform.position-GunsParent3.transform.position), Time.deltaTime * 15f);
                //GunsParent2.transform.LookAt(Player.transform);
                timer += Time.deltaTime;
                yield return null;
            }
            Instantiate(Bullets[1], GunsParent2.transform.position, GunsParent2.transform.rotation);
            Instantiate(Bullets[1], GunsParent3.transform.position, GunsParent3.transform.rotation);
            timer = 0;
            n--;
            yield return null;
        }
    }
    IEnumerator ReturnToCenter(float distance,float Speed)
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
    IEnumerator TransformtoX(float x, float speed)
    {
        while (transform.position.x != x)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, x, speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
                );
            yield return null;
        }
    }
    IEnumerator ShootandRotateLaser()
    {
        GunsParent.transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        GunsParent.transform.GetChild(7).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Quaternion euler = Quaternion.Euler(GunsParent.transform.rotation.x, -180f, GunsParent.transform.rotation.z);
        bool turnedOn = true;
        while (GunsParent.transform.rotation != euler)
        {
            timer += Time.deltaTime;
            GunsParent.transform.rotation = Quaternion.RotateTowards(GunsParent.transform.rotation, euler, 20f * Time.deltaTime);
            //GunsParent.transform.RotateAround(Vector3.zero, Vector3.up, 360f * Time.deltaTime / 8f);
            if (timer > 2.5 && turnedOn == true)
            {
                GunsParent.transform.GetChild(7).gameObject.SetActive(false);
                turnedOn = false;
                timer = 0;
            }
            else if (timer > 1 && turnedOn == false)
            {
                GunsParent.transform.GetChild(7).gameObject.SetActive(true);
                turnedOn = true;
                timer = 0;
            }

            yield return null;
        }
        euler = Quaternion.Euler(GunsParent.transform.rotation.x, -270f, GunsParent.transform.rotation.z);
        while (GunsParent.transform.rotation != euler)
        {
            timer += Time.deltaTime;
            GunsParent.transform.rotation = Quaternion.RotateTowards(GunsParent.transform.rotation, euler, 20f * Time.deltaTime);
            if (timer > 2.5 && turnedOn == true)
            {
                GunsParent.transform.GetChild(7).gameObject.SetActive(false);
                turnedOn = false;
                timer = 0;
            }
            else if (timer > 1 && turnedOn == false)
            {
                GunsParent.transform.GetChild(7).gameObject.SetActive(true);
                turnedOn = true;
                timer = 0;
            }

            yield return null;
        }
        GunsParent.transform.GetChild(7).gameObject.SetActive(false);
        GunsParent.transform.GetChild(6).gameObject.SetActive(false);
        GunsParent.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    void StopAll()
    {
        StopAllCoroutines();
        transform.GetChild(3).gameObject.SetActive(false);
        GunsParent.transform.GetChild(7).gameObject.SetActive(false);
        GunsParent.transform.GetChild(6).gameObject.SetActive(false);
        StartCoroutine(BossControl(4f));
    }
}
