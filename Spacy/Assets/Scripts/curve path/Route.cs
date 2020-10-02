using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private Vector3 gizmosPosition;
    public int r;

    private void OnDrawGizmos()
    {
        if (r == 1)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                    Mathf.Pow(t, 3) * controlPoints[3].position;
                Gizmos.DrawSphere(gizmosPosition, 0.25f);
            }
            Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
            Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
        }
        

        else if (r == 2)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 4) * controlPoints[0].position +
                    4 * Mathf.Pow(1 - t, 3) * t * controlPoints[1].position +
                    6 * Mathf.Pow(1 - t, 2) * Mathf.Pow(t, 2) * controlPoints[2].position +
                    4 * (1 - t) * Mathf.Pow(t, 3) * controlPoints[3].position +
                    Mathf.Pow(t, 4) * controlPoints[4].position;
                Gizmos.DrawSphere(gizmosPosition, 0.25f);
            }
            Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
            Gizmos.DrawLine(controlPoints[3].position, controlPoints[4].position);
        }
    }

}
