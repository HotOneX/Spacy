using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour 
{
	public float speed;
    public Rigidbody Rig;
	private void Start ()
	{
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Rig.velocity = transform.forward * speed;
	}
}
