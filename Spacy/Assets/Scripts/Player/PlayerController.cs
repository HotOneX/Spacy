using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    public AudioSource[] WeponAudio;
    [Header("PowerUp")]
    public float TimeShift;
    public UIAndScores UIAndScores;
    public GameObject powers;
    

    [Header("Shot Settings")]
    public float[] FireRate;
    public Transform[] ShotSpawns;
    public Properties Bullets;
    public ParticleSystem AcidShotStart;
    public int WeaponNumber;
    public Animator Anim;
    public static int newPower, weaponLevel;
    private float NextFire;
    private Quaternion BoltRotation;

    [Header("Movement and Physics Settings")]
    private Rigidbody rb;
    public Boundary boundary;
    public float Speed;
    public float tiltspeed;

    private Vector3 distance;
    private Vector3 PreviousFramePosition;
    public float SpeedMeasure;
    private Vector3 playerpos;
    public Vector3 mousepos;
    private bool k;
    private float tilt;

    void Awake()
    {
        foreach (var Bolt in ShotSpawns)
        {
            BoltRotation = Bolt.transform.rotation;
        }
        weaponLevel = 1;
    }

    void Start()
    {
        newPower = 0; WeaponNumber = 0;
        k = true;
        rb = GetComponent<Rigidbody>();
    }
    void Update()//rahi nist motevaghefesh konim hatta baraye chand sania, Update() hamishar har frame yek bar ejra mishavad
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            
            Anim.SetFloat("fireRate",1/FireRate[WeaponNumber] + 2);
            if (WeaponNumber == 5)
            {
                transform.GetChild(6).gameObject.SetActive(true);
            }

            else if (WeaponNumber == 0 && Time.realtimeSinceStartup > NextFire)
            {
                NextFire = Time.realtimeSinceStartup + FireRate[WeaponNumber];
                Instantiate(Bullets.MainBullet[weaponLevel], ShotSpawns[WeaponNumber].GetChild(0).position, ShotSpawns[WeaponNumber].GetChild(0).rotation);
                WeponAudio[WeaponNumber].Play();
                Anim.Play("shooting",1);
                //transform.GetChild(4).gameObject.SetActive(false);
            }

            else if (WeaponNumber == 1 && Time.realtimeSinceStartup > NextFire)
            {
                NextFire = Time.realtimeSinceStartup + FireRate[WeaponNumber];
                Instantiate(Bullets.AcidBullet[weaponLevel], ShotSpawns[WeaponNumber].GetChild(0).position, ShotSpawns[WeaponNumber].GetChild(0).rotation);//this function makes a copy of an object in a smilar way to the Duplicate command in the editor.
                AcidShotStart.Play();
                WeponAudio[WeaponNumber].Play();
                Anim.Play("shooting", 1);
                //transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    #region movment methods that we dont need they here

    /*void CalibrateAccelerometer()//in va tabe paeeni vaseye touchpad nistan, vaseye code harekate safine ba estefade az charxeshe gooshian
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.5f, 0.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration(Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}*/

    /*float x = Input.GetAxis ("Horizontal");
    float z = Input.GetAxis ("Vertical");
    Vector3 movement = new Vector3 (x, 0.0f, z);*/

    /*Vector3 accelerationRaw = Input.acceleration;
    Vector3 acceleration = FixAcceleration (accelerationRaw);
    Vector3 movement = new Vector3 (accelerationRaw.x, 0.0f, accelerationRaw.z);*/

    //Vector2 direction = touchPad.GetDirection();
    //Vector3 direction=Input.mousePosition;
    //Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
    //rb.AddForce(movement*Speed*Time.deltaTime);
    //rb.transform.position=movement * Speed * Time.deltaTime;
    //rb.transform.Translate(direction.x * Speed * Time.deltaTime,0.0f,direction.y * Speed * Time.deltaTime);
    //rb.velocity=movement*Speed;
    //rb.velocity= new Vector3(direction.x, 0.0f, direction.y);
    //rb.MovePosition(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)));
    //rb.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));

    #endregion

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())//the event system is for stop using getmouseButton when pressing a UI button or text
        {
            if (k)
            {
                mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
                playerpos = transform.position;
                k = false;
                distance = new Vector3(playerpos.x - mousepos.x, playerpos.y - mousepos.y, playerpos.z - mousepos.z);
            }
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)) + distance, Speed * Time.fixedDeltaTime / Time.timeScale);//vali inja chon nemikhaim sorate spaceship hatta vaghti slowmotion mishe avaz she, pas taghirati ke dar yek khat balatar anjam dadim baraye sorate spaceship khonsa mikonim.
            //Vector3 movment = new Vector3(,transform.position.y,)
            //rb.velocity=Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)) + distance, Speed * Time.fixedDeltaTime / Time.timeScale);
            //tilt = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).x - distance.x;
            powers.transform.position = new Vector3(transform.position.x, powers.transform.position.y, transform.position.z);
            //float distanceforSpeed = Vector3.Distance(PreviousFramePosition, transform.position);
            SpeedMeasure = (PreviousFramePosition.x - transform.position.x)/Time.deltaTime;
            
        }
        else
        {
            //Anim.SetBool("jolajol", false);
            k = true;
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            8f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
        );
        float currentRotation = SpeedMeasure*10;
        currentRotation = Mathf.Clamp(currentRotation, -45f, 45f);
        //transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        PreviousFramePosition = transform.position;
        Quaternion euler = Quaternion.Euler(transform.rotation.x,transform.rotation.y,currentRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,euler,tiltspeed);
    }

    void OnTriggerEnter(Collider other)// for powers
    {
        Debug.Log(weaponLevel);
        if (other.CompareTag("Powers"))
        {
            if (other.name == "2Gun(Clone)")
            {
                WeaponNumber = 0;
                if (weaponLevel < Bullets.MainBullet.Length - 1 || weaponLevel <= 5)
                    weaponLevel++;
            }
            else if (other.name == "Acid(Clone)")
            {
                Debug.Log("YES");
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.velocity= Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                
                WeaponNumber = 1;
                if (weaponLevel < Bullets.AcidBullet.Length - 1 || weaponLevel <= 5)
                    weaponLevel++;
                Destroy(other.gameObject, 2f);
            }
            else if (other.name == "HolyLaser(Clone)")
            {
                WeaponNumber = 5;
            }

            else if (other.name == "Shield(Clone)" || other.name == "Shield2(Clone)")
            {
                UIAndScores.powerCounts[0]++;
                UIAndScores.PowerUpText[0].text = "" + UIAndScores.powerCounts[0];
            }
            else if (other.name == "TimeShift(Clone)")
            {
                UIAndScores.powerCounts[1]++;
                UIAndScores.PowerUpText[1].text = "" + UIAndScores.powerCounts[1];
            }
            if(other.name!= "Acid(Clone)")
                Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            return;
        }
    }
    private IEnumerator UsePWU(int p)
    {
        switch (p)
        {
            case 0:
                transform.GetChild(2).gameObject.SetActive(true);
                gameObject.tag = "Shield";
                yield return new WaitForSeconds(10f);
                transform.GetChild(2).gameObject.SetActive(false);
                gameObject.tag = "Player";
                yield break;
            case 1:
                newPower = 1;
                yield return new WaitForSecondsRealtime(5f);
                Time.timeScale = 1;
                newPower = 0;
                yield break;

            default:
                yield break;
        }
    }
    public void UsePoweup(int i)
    {
        if (UIAndScores.powerCounts[i] > 0)
        {
            UIAndScores.powerCounts[i]--;
            UIAndScores.PowerUpText[i].text = "" + UIAndScores.powerCounts[i];
            StartCoroutine(UsePWU(i));
        }
        else return;
    }

    void LateUpdate()
    {
        foreach (var Bolt in ShotSpawns)
        {
            Bolt.transform.rotation = BoltRotation;
        }
    }
}