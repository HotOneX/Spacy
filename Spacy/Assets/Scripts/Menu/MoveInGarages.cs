/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveInGarages : MonoBehaviour
{
    public float speedH = 5.0f;
    public float speedV = 5.0f;
    public PointerEventData eventData;
    //public float force = 5;

    private float pitch = -800.0f;
    private float yaw = -35.0f;

    private Vector3 screenPoint;
    private Vector3 offset;
    private GameObject hittedGO;
    private RaycastHit hit;
    private Vector3 curRotation;

    /*private Transform cameraP;
    private Animator Animator;

    private void Awake()
    {
        /*cameraP = this.GetComponent<Transform>();
        Animator = this.GetComponent<Animator>();
        
    }
    void Start()
    {
        
    }

    void Update()
    {

        //transform.Rotate((Input.GetAxis("Mouse X") * speedH * Time.deltaTime), (Input.GetAxis("Mouse Y") * speedH * Time.deltaTime), 0, Space.World);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {

            if (hit.transform != null)
            {
                Debug.Log(hit.transform.gameObject.tag);
                Rigidbody rb;
                if (hit.transform.gameObject.tag == "Player")
                {
                    
                    //PrintName(hit.transform.gameObject);
                    hittedGO = hit.transform.gameObject;

                    if (Input.GetMouseButtonDown(0))
                    {
                        print("mouse pressed");
                        yaw = speedH * Input.GetAxis("Mouse X")*Time.deltaTime;
                        pitch = speedV * Input.GetAxis("Mouse Y")*Time.deltaTime;
                       // hit.transform.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(pitch, yaw, -45));
                        hit.transform.gameObject.transform.Rotate(pitch, yaw,-45, Space.World);
                    }
                }
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
        print(go.transform.localPosition);
    }

    /*private void LaunchIntoAir(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
    }
                    /*void OnMouseDown()
                    {
                        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));


                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out hit, 100.0f))
                        {

                            if (hit.transform != null)
                            {
                                Debug.Log(hit.transform.gameObject.tag);
                                Rigidbody rb;
                                if (hit.transform.gameObject.tag == "Player")
                                {
                                    //print("mouse pressed");
                                    PrintName(hit.transform.gameObject);
                                    hittedGO = hit.transform.gameObject;

                                    /*while (Input.GetMouseButtonDown(0))
                                    {
                                        yaw -= speedH * Input.GetAxis("Mouse X");
                                        pitch += speedV * Input.GetAxis("Mouse Y");

                                    }

                                    //Debug.Log(yaw + "  " + pitch);
                                }
                            }
                        }

                    }*/

                    /* void OnMouseDrag()
                     {
                         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

                         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

                         curRotation = Camera.main.ScreenToWorldPoint(curScreenPoint);
                         //transform.position = curPosition;
                         hit.transform.gameObject.transform.rotation = Quaternion.Euler(curRotation);

                     }

                }
*/