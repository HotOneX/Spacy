using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineAnimation : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter = float.MaxValue;
    private float dist;
    private int positionIndex = 0;
    private Vector3[] positions = new Vector3[3];

    public GameObject targetPanel;
    public GameObject startPoint;
    public float lineDrawSpeed = 6f;
    public float initialSpeed = 3f;
    public Vector3 offsetPanel;
    private Vector3 curPosition;
    private Vector3 t = Vector3.zero;
    private int back = 0;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPoint.transform.position);
        //lineRenderer.SetWidth(0.45f, 0.45f);
        positions[0] = startPoint.transform.position;
        positions[1] = new Vector3(targetPanel.transform.position.x-offsetPanel.x,startPoint.transform.position.y, startPoint.transform.position.z);
        positions[2] = targetPanel.transform.position - offsetPanel;
       // Debug.Log(positions[0] + "  " + positions[1] + "  " + positions[2]);

    }

    // Update is called once per frame
    void Update()
    {
        if (positionIndex < positions.Length - 1 && back == 0)
        {
            print("creating line");
            curPosition = SmoothlyMove(positions[positionIndex], positions[positionIndex + 1]);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPosition);
        }
        else back = 1;
        if (Input.GetMouseButton(0) && back == 1)
        {
            print("getting back");
            lineRenderer.SetPosition(0, startPoint.transform.position);
            lineRenderer.SetPosition(1, new Vector3(lineRenderer.GetPosition(1).x, startPoint.transform.position.y, startPoint.transform.position.z));
            GetBack();
            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            back = 0;
            positionIndex = 0;

        }
        else if(Input.GetMouseButton(0) && back == -1)
        {
            lineRenderer.SetPosition(1, startPoint.transform.position);
        }

        //print("PoIndex: "+positionIndex);
        //print("back: "+back);
    }

    private void GetBack()
    {
        
        //if (Input.GetMouseButtonDown(0)) positionIndex = 0;
        curPosition = SmoothlyMove(positions[positionIndex], positions[positionIndex - 1]);
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPosition);
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
        if (t.x > 1 || t.y>1 || t.z>1)
        {
            t = Vector3.zero;
            if (positionIndex < positions.Length - 1 && back == 0)
            {
                if (positionIndex < positions.Length - 2)
                {
                    //print("count++");
                    lineRenderer.positionCount++;

                }

                positionIndex++;
                //print("pIndex++");
            }
            else if (positionIndex > 1)
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
