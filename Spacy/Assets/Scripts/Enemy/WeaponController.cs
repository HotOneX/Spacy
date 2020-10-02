using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour 
{
	public GameObject shot;
	public Transform[] shotSpawns;

	public float delay;
	private float fireRate;
    [Header("FireRate")]
    public float min;
    public float max;

	private AudioSource audioSource;
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
        fireRate = Random.Range(min, max);
		InvokeRepeating ("Fire", fireRate, fireRate);//in here we can use loop or courotine and IEnumerator like in GameController script.S
	}
	void Fire()
	{
		foreach (var shotSpawn in shotSpawns)
		{
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
		audioSource.Play ();
	}
}
