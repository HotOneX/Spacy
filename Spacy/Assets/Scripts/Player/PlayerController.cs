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
    public static int newPower;
    public int WeaponNumber, weaponLevel;
    private float NextFire;
    private Quaternion BoltRotation;

    [Header("Movement and Physics Settings")]
    public Rigidbody rb;
    public Boundary boundary;
    public float Speed;
    public float tiltspeed;

    private Vector3 distance;
    private Vector3 playerpos;
    private Vector3 mousepos;
    private bool k;
    private float tilt;

    void Awake()
    {
        foreach (var Bolt in ShotSpawns)
        {
            BoltRotation = Bolt.transform.rotation;
        }
    }

    void Start()
    {
        newPower = 0; weaponLevel = 1; WeaponNumber = 0;
        k = true;
    }
    void Update()//rahi nist motevaghefesh konim hatta baraye chand sania, Update() hamishar har frame yek bar ejra mishavad
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (WeaponNumber == 5)
            {
                transform.GetChild(6).gameObject.SetActive(true);
            }

            else if (WeaponNumber == 0 && Time.realtimeSinceStartup > NextFire)
            {
                NextFire = Time.realtimeSinceStartup + FireRate[WeaponNumber];
                Instantiate(Bullets.MainBullet[weaponLevel], ShotSpawns[WeaponNumber].GetChild(0).position, ShotSpawns[WeaponNumber].GetChild(0).rotation);
                WeponAudio[WeaponNumber].Play();
                transform.GetChild(4).gameObject.SetActive(false);
            }

            else if (WeaponNumber == 1 && Time.realtimeSinceStartup > NextFire)
            {
                NextFire = Time.realtimeSinceStartup + FireRate[WeaponNumber];
                Instantiate(Bullets.AcidBullet[weaponLevel], ShotSpawns[WeaponNumber].GetChild(0).position, ShotSpawns[WeaponNumber].GetChild(0).rotation);//this function makes a copy of an object in a smilar way to the Duplicate command in the editor.
                AcidShotStart.Play();
                WeponAudio[WeaponNumber].Play();
                transform.GetChild(4).gameObject.SetActive(false);
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
            /*if (newPower == 1)
            {
                Time.timeScale = TimeShift;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;//inja sorate khondane fixedUpdate haro avaz mikonim va dar zamane Timshift FPS ro batavajjoh be Timeshift avaz mikonim.
            }*/
            //Time.fixedDeltaTime = Time.timeScale * 0.02f;
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)) + distance, Speed * Time.fixedDeltaTime / Time.timeScale);//vali inja chon nemikhaim sorate spaceship hatta vaghti slowmotion mishe avaz she, pas taghirati ke dar yek khat balatar anjam dadim baraye sorate spaceship khonsa mikonim.
            tilt = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).x - distance.x;
            powers.transform.position = new Vector3(transform.position.x, powers.transform.position.y, transform.position.z);
        }
        else
        {
            k = true;
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            8f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
        );
        float currentRotation = -90 + tilt * tiltspeed;
        currentRotation = Mathf.Clamp(currentRotation, -135f, -45f);
        transform.rotation = Quaternion.Euler(currentRotation, 270f, -90f);
    }

    void OnTriggerEnter(Collider other)// for powers
    {
        if (other.CompareTag("Powers"))
        {
            if (other.name == ("2Gun(Clone)"))
            {
                WeaponNumber = 0;
                if (weaponLevel < Bullets.MainBullet.Length - 1)
                    weaponLevel++;
            }
            else if (other.name == "Acid(Clone)")
            {
                WeaponNumber = 1;
                if (weaponLevel < Bullets.AcidBullet.Length - 1)
                    weaponLevel++;
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
                transform.GetChild(3).gameObject.SetActive(true);
                gameObject.tag = "Shield";
                yield return new WaitForSeconds(10f);
                transform.GetChild(3).gameObject.SetActive(false);
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