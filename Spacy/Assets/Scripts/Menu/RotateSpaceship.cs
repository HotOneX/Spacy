using System.Collections;
using UnityEngine;

public class RotateSpaceship : MonoBehaviour
{
    public float RotateSpeed = 10f;
    public float angleAutoRotate = 12f;
    public float tSpaceship = 0;
    public float dragSpeed = 5;

    private bool drag = false;
    private bool autoRotating = true;
    //private Vector3 prevPos = Vector3.zero;
    private float maxRotateS = 0.0751f;
    private float minRotateS = -0.0753561f;
    private bool isCoroutineExecuting;


    IEnumerator WaitAfterMouseUp()
    {
        
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        
        yield return new WaitForSeconds(3);
        autoRotating = true;
        tSpaceship = (transform.localRotation.y - minRotateS) / (maxRotateS - minRotateS);
        isCoroutineExecuting = false;
    }

    [System.Obsolete]
    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.up, -rotX);
    }


    private void OnMouseUp()
    {
        drag = false;        
    }
    private void AutoRotate()
    {
        float _y = Mathf.Lerp(transform.rotation.y + (3 * angleAutoRotate), transform.rotation.y + 360f + (3 * angleAutoRotate), tSpaceship += RotateSpeed * Time.deltaTime);
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
