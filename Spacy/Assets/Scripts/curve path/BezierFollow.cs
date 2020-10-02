using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes1;
    [SerializeField]
    private Transform[] routes2;
    [SerializeField]
    private Transform[] routes3;
    [SerializeField]
    private Transform[] routes4;
    [SerializeField]
    private Transform[] routes5;

    private Transform[] routes;
    private int routeToGo;
    private SpawnController spawnController;
    private Vector3 objectPosition;
    public float speedModifier;
    private float t;
    private float n;

    private EvasiveManeuver evasiveManeuver;
    private Rigidbody rb;
    private float tilt;
    private int sign;

    private Vector3 p0, p1, p2, p3, p4;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject GameController = GameObject.FindWithTag("GameController");
        spawnController = GameController.GetComponent<SpawnController>();
        evasiveManeuver = GetComponent<EvasiveManeuver>();
        rb = this.GetComponent<Rigidbody>();
    }
    void Start()
    {
        routeToGo = 0;
        t = 0f;
        n = spawnController.n;
        StartCoroutine(GoByTheRoute(routeToGo));
    }
    private IEnumerator GoByTheRoute(int routeNumber=0)
    {
        switch (n)
        {
            case 1:
                routes = routes1;
                break;
            case 2:
                routes = routes2;
                break;
            case 3:
                routes = routes3;
                break;
            case 4:
                routes = routes4;
                objectPosition = new Vector3(3.2f * Mathf.Sign(transform.position.x), transform.position.y, transform.position.z);
                while (transform.position != objectPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 4f);
                    yield return null;
                }
                break;
            case 5:
                routes = routes5;
                objectPosition = new Vector3(3.2f * Mathf.Sign(transform.position.x), transform.position.y, transform.position.z);
                while (transform.position != objectPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 4f);
                    yield return null;
                }
                break;
            case 10:
                objectPosition = new Vector3(spawnController.temp * -Mathf.Sign(transform.position.x), transform.position.y, transform.position.z);
                Quaternion objectrotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, 0f);
                rb.rotation = Quaternion.Euler(0.0f, 0.0f, 40f * Mathf.Sign(transform.position.x));
                while (transform.position!=objectPosition)
                {
                    tilt = transform.position.x - objectPosition.x;
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 4f);
                    if (Mathf.Abs(tilt) < 2)
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, objectrotation, 5f);
                    yield return null;
                }
                yield break;

            case 11:
                sign = spawnController.sign;
                objectPosition = new Vector3(transform.position.x, transform.position.y, spawnController.temp);
                Quaternion objrotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 35f * sign);
                while (transform.position!=objectPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 7f);  //Vertical moving
                    yield return null;
                }
                objectPosition = new Vector3(3f * sign, transform.position.y, transform.position.z);
                while(transform.position != objectPosition)
                {
                    tilt = transform.position.x - objectPosition.x;
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 3f);  //Horizontal moving
                    if (Mathf.Abs(tilt) > 1.5)
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, objrotation, 5f);
                    else
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f), 5f);
                    yield return null;
                }
                yield break;

            case 12:
                objectPosition = new Vector3(spawnController.temp * -Mathf.Sign(transform.position.x), transform.position.y, transform.position.z);
                Quaternion objectrotation2 = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
                rb.rotation = Quaternion.Euler(0.0f, 0.0f, 40f * Mathf.Sign(transform.position.x));
                while (transform.position != objectPosition)
                {
                    tilt = transform.position.x - objectPosition.x;
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 7f);
                    if (Mathf.Abs(tilt) < 2)
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, objectrotation2, 5f);
                    yield return null;
                }
                yield return new WaitUntil(() => spawnController.booltemp);

//##################     #################     ####### Check kardane inke cheghadr bere bala #######    ######################
                if (Mathf.Abs(transform.position.x) == 5)
                    objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.4f);
                else if (Mathf.Abs(transform.position.x) == 3)
                {
                    yield return new WaitForSeconds(0.5f);
                    objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);
                }
                else if (Mathf.Abs(transform.position.x) == 1)
                {
                    yield return new WaitForSeconds(1f);
                    objectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.7f);
                }
//##################     #################     ####### Axare Check kardane inke cheghadr bere bala #######    ######################

                while (transform.position != objectPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 4f);  //Vertical moving
                    yield return null;
                }
                yield break;
            
            case 13:
                objectPosition = new Vector3(0f, transform.position.y, spawnController.temp2);
                while (transform.position != objectPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 8f);
                    yield return null;
                }
                yield return new WaitForSeconds(5f);
                enabled = false;
                evasiveManeuver.enabled = true;
                yield break;
            
            case 14:
                objectPosition = new Vector3(spawnController.temp2 * -Mathf.Sign(transform.position.x), transform.position.y, 10f);
                Quaternion objtrotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
                rb.rotation = Quaternion.Euler(0.0f, 0.0f, 40f * Mathf.Sign(transform.position.x));
                while (transform.position != objectPosition)
                {
                    tilt = transform.position.x - objectPosition.x;
                    transform.position = Vector3.MoveTowards(transform.position, objectPosition, Time.deltaTime * 8f);
                    if (Mathf.Abs(tilt) < 2)
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, objtrotation, 5f);
                    yield return null;
                }
                yield return new WaitForSeconds(3f);
                enabled = false;
                evasiveManeuver.enabled = true;

                yield break;

            default:
                evasiveManeuver.enabled = true;
                enabled = false;
                yield break;
        }

        while (routeNumber < routes.Length)
        {
            p0 = routes[routeNumber].GetChild(0).position;
            p1 = routes[routeNumber].GetChild(1).position;
            p2 = routes[routeNumber].GetChild(2).position;
            p3 = routes[routeNumber].GetChild(3).position;

            if ((n == 4 || n == 5))
            {
                p4 = routes[routeNumber].GetChild(4).position;
                while (t < 1)
                {
                    t += Time.deltaTime * speedModifier;

                    objectPosition = Mathf.Pow(1 - t, 4) * p0 +
                    4 * Mathf.Pow(1 - t, 3) * t * p1 +
                    6 * Mathf.Pow(1 - t, 2) * Mathf.Pow(t, 2) * p2 +
                    4 * (1 - t) * Mathf.Pow(t, 3) * p3 +
                    Mathf.Pow(t, 4) * p4;

                    transform.position = objectPosition;
                    yield return null;
                }
            }
            else
            {
                while (t < 1)
                {
                    t += Time.deltaTime * speedModifier;

                    objectPosition = Mathf.Pow(1 - t, 3) * p0 +
                        3 * Mathf.Pow(1 - t, 2) * t * p1 +
                        3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                        Mathf.Pow(t, 3) * p3;

                    transform.position = objectPosition;
                    yield return null;
                }
                routeNumber++;
            }
            t = 0f;
            
            //if (routeNumber >= routes.Length && loop == true)
                //routeNumber = 0;
        }
        enabled = false;
        evasiveManeuver.enabled = true;
    }
}
