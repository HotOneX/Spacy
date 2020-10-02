using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour 
{
	public float lifetime;//we need lifetime because without it the explosions Destroys as soon as possible but the explosions and all particle systems need some time to show what they have and because of that we need destroy this after some second or time.
	void Start ()
	{
		Destroy (gameObject , lifetime);
	}
}
