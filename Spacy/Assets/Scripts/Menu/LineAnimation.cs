using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineAnimation : MonoBehaviour
{
    public GameObject targetPanel;
    public GameObject startPoint;
    public float lineDrawSpeed = 6f;
    //public float initialSpeed = 3f;
    public Vector3 offsetPanel;

    private LineRenderer lineRenderer;
    private int positionIndex = 0;
    private Vector3[] positions = new Vector3[3];
    private Vector3 curPosition;
    private Vector3 t = Vector3.zero;
    private int back = 0;
    private bool onWay = true;
    private bool onBack = false;
    public GameObject garageView;
    private bool spaceshipHitted;
    private RotateInGarage rotateInGarage;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPoint.transform.position);
        //lineRenderer.SetWidth(0.45f, 0.45f);
        positions[0] = startPoint.transform.position;
        positions[1] = new Vector3(targetPanel.transform.position.x-offsetPanel.x,startPoint.transform.position.y, startPoint.transform.position.z);
        positions[2] = targetPanel.transform.position - offsetPanel;
        // Debug.Log(positions[0] + "  " + positions[1] + "  " + positions[2]);
        rotateInGarage = garageView.GetComponent<RotateInGarage>();
        Debug.Log(rotateInGarage.hittedPlayer);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) spaceshipHitted = rotateInGarage.hittedPlayer;
        //Debug.Log("line hitted: " + rotateInGarage.hittedPlayer);
        if (Input.GetMouseButtonUp(0))
        {
            spaceshipHitted = false;
            back = 0;
            onWay = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
                back = 1;
                onBack = true;
        }

        if (back == 0)
        {
            if (lineRenderer.positionCount <= positions.Length)
            {
                //print("Pos index" + positionIndex);
                //print("creating line");
                //print("onback " +onBack);
                //print("maxPosCount " + (lineRenderer.positionCount == positions.Length));
                positions[0] = startPoint.transform.position;
                lineRenderer.SetPosition(0, startPoint.transform.position);
                positions[1] = new Vector3(targetPanel.transform.position.x - offsetPanel.x, startPoint.transform.position.y, startPoint.transform.position.z);
                if (onBack)
                {
                    //print("reset t");
                    t = Vector3.one - t;
                    onBack = false;
                    positionIndex--;
                }
                curPosition = SmoothlyMove(positions[positionIndex], positions[positionIndex + 1]);
                if (lineRenderer.positionCount < 3 && lineRenderer.GetPosition(positionIndex + 1) == Vector3.zero)
                    lineRenderer.positionCount++;

                lineRenderer.SetPosition(positionIndex + 1, curPosition);
            }
            else
            {
                onWay = false;
            }
        }
        else if(rotateInGarage.hittedPlayer)
        {
            if (lineRenderer.positionCount > 1)
            {
                //print("getting back");
                //print("Pos index" + positionIndex);
                //print("PoIndex: " + positionIndex);
                positions[0] = startPoint.transform.position;
                positions[1] = positions[1] = new Vector3(targetPanel.transform.position.x - offsetPanel.x, startPoint.transform.position.y, startPoint.transform.position.z);
                lineRenderer.SetPosition(0, startPoint.transform.position);
                if (onWay)
                {
                    //print("reset t");
                    if (t != Vector3.zero)
                        t = Vector3.one - t;
                    onWay = false;
                    if (positionIndex < positions.Length - 1)
                        positionIndex++;
                }
                //lineRenderer.SetPosition(1, new Vector3(lineRenderer.GetPosition(1).x, startPoint.transform.position.y, startPoint.transform.position.z));
                curPosition = SmoothlyMove(positions[positionIndex], positions[positionIndex - 1]);
                lineRenderer.SetPosition(positionIndex, curPosition);
            }
            else
            {
                onBack = false;
            }
        }
    }

    private bool GetHittedPlayer()
    {
        
        return spaceshipHitted;
    }

    private Vector3 SmoothlyMove(Vector3 startPos, Vector3 endPos)
    {
        Vector3 position = new Vector3(Mathf.Lerp(startPos.x, endPos.x, t.x += lineDrawSpeed * Time.deltaTime),
                                       Mathf.Lerp(startPos.y, endPos.y, t.y += lineDrawSpeed * Time.deltaTime),
                                       Mathf.Lerp(startPos.z, endPos.z, t.z += lineDrawSpeed * Time.deltaTime));
        StopSwitching();
        return position;
    }

    private void StopSwitching()
    {
        if (t.x > 1 || t.y > 1 || t.z > 1)
        {
            t = Vector3.zero;
            if (positionIndex < positions.Length - 1 && back == 0)
            {
                positionIndex++;
            }
            else if (/*positionIndex > 1 && */lineRenderer.positionCount > 1 && back == 1)
            {
                lineRenderer.positionCount--;
                positionIndex--;
            }
            else if (Input.GetMouseButton(0))
            {
                positionIndex = 0;
                back = -1;
            }
        }
    }
}
