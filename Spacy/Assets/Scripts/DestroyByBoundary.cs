using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerExit(Collider other)
	{
        if(!other.CompareTag("Laser"))
		    Destroy (other.gameObject);
	}
}//this code is destroy any object that leaves the triggers value
