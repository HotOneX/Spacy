using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateSpaceship : MonoBehaviour
{
    public float sRotateSpeed = 10f;
    public float angleAutoRotate = 12f;
    public float tSpaceship = 0;
    public float dragSpeed = 5;

    private bool drag = false;
    private bool autoRotating = true;
    private Vector3 prevPos = Vector3.zero;
    private float maxRotateS = 0.0751f;
    private float minRotateS = -0.0753561f;
    private bool isCoroutineExecuting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drag)
        {
            float x = Input.GetAxis("Mouse X");
            transform.RotateAround(transform.position, new Vector3(0, 1f, 0) * Time.deltaTime * x * -1, dragSpeed);
        }
        else if (autoRotating)
        {
            AutoRotate();
        }
        else
        {
            StartCoroutine(nameof(waitAfterMouseUp));
        }

    }

    IEnumerator waitAfterMouseUp()
    {
        
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        
        yield return new WaitForSeconds(3);
        autoRotating = true;
        tSpaceship = (transform.localRotation.y - minRotateS) / (maxRotateS - minRotateS);
        isCoroutineExecuting = false;
    }

    private void OnMouseDrag()
    {
        //Debug.Log("Dragging Mouse");
        drag = true;
        autoRotating = false;
    }

    private void OnMouseUp()
    {
        drag = false;        
    }
    private void AutoRotate()
    {
        float _y = Mathf.Lerp(transform.rotation.y + (3 * angleAutoRotate), transform.rotation.y + 360f + (3 * angleAutoRotate), tSpaceship += sRotateSpeed * Time.deltaTime);
        Vector3 rSapceship = new Vector3(angleAutoRotate, _y, 0f);
        transform.rotation = Quaternion.Euler(rSapceship);
        tSpaceship = StopSwitching(tSpaceship);
        if (transform.localRotation.y > maxRotateS) maxRotateS = transform.localRotation.y;
        else if (minRotateS > transform.localRotation.y) minRotateS = transform.localRotation.y;
        //Debug.Log(transform.localRotation.y + "  " + maxRotateS + "   " + minRotateS);
    }

    private float StopSwitching(float _t)
    {
        if (_t > 1)
        {
            //backCamera = false;
            _t = 0;
        }

        return _t;
    }
}
