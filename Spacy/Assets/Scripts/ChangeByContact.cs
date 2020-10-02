using UnityEngine;

public class ChangeByContact : MonoBehaviour
{
    private static int newPower;
    void Start()
    {
        newPower = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powers"))
        {
            if (other.name == "2Gun")
            {
                newPower = 1;
                Debug.Log(newPower);
            }
            else if (other.name == "TimeShift")
            {
                newPower = 2;
            }
            Destroy(other.gameObject);
        }
    }
    public int ChangeBolt()
    {
        return newPower;
    }
}
