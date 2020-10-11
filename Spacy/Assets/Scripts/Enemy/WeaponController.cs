using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour 
{
	public GameObject shot;
	public Transform[] shotSpawns;
    public ParticleSystem StartShoot;
	private float fireRate;
    [Header("FireRate")]
    public float min;
    public float max;

	private AudioSource audioSource;
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
        fireRate = Random.Range(min, max);
        //invoke is worked but we need waitforseconds for StartShoot particle System, so we use coroutine instead.
        //InvokeRepeating ("Fire", fireRate, fireRate);
        StartCoroutine(StartShooting());
	}
    private IEnumerator StartShooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireRate);
            if (Random.value <= 0.2)
            {
                if(StartShoot)
                    StartShoot.Play(true);
                yield return new WaitForSeconds(2f);
                foreach (var shotSpawn in shotSpawns)
                {
                    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                }
                audioSource.Play();
            }
        }
    }
}
