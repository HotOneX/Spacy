using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    public int Health;
    public GameObject Explosion;
    private UIAndScores UIAndScores;
    private Material shieldmat;
    private Collider col;

    private void Awake()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        UIAndScores = GameControllerObject.GetComponent<UIAndScores>();
        shieldmat = GetComponent<Renderer>().material;
    }
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }
    public void TakeDamage(int Amount)
    {
        UIAndScores.BulletLevelupSlider.value += Amount;
        Health -= Amount;
        if (Health <= 0)
        {
            shieldmat.SetVector("_Position", new Vector3(8f, 0f, 0f));
            col.enabled = false;
            if (Explosion != null)// its say if Explosion field in unity inspector in scripts IS not empty, so go on and if not, so skip this if.
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            UIAndScores.BulletLevelupSlider.value += Amount * 2;
            Health = 200;
        }
    }
}
