using UnityEngine;

public class PlayerBoltMover : MonoBehaviour {

    public float speed;
    public Rigidbody Rig;
    private void FixedUpdate()
    {
        //GetComponent<Rigidbody>().velocity = transform.forward * speed / Time.timeScale;
        Rig.velocity = transform.forward * speed / Time.timeScale;
    }
}
