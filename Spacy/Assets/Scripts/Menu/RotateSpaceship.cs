using System.Collections;
using UnityEngine;

public class RotateSpaceship : MonoBehaviour
{
    public float RotateSpeed = 10f;
    public float angleAutoRotate = 12f;
    public float tSpaceship = 0;
    public float dragSpeed = 5;

    [System.Obsolete]
    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * RotateSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.up, -rotX);
    }
}
