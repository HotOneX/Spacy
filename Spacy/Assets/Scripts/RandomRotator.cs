using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour 
{
	public Rigidbody rb;
	public float tumble;
	void Start ()
	{
		//rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;//angularVelocity and insideUnitSphere they are both in vector3 class but rotation is not in the vector3 class and it is in Quaternion class and because of that we cant use rotation instead of angularVelocity
	
	}
}//well we can change both angularVelocity and insideUnitSphere to rotation for convert vector3 to Quaternion class but Quaternion class is not work with float like tumble!! so we must use vector3 classes for random 
