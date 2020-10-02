using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health;
    public GameObject Explosion;
    public Renderer rend;
    private Material origmat;
    public Material flashMaterial;
    public int ScoreValue;

    private UIAndScores UIAndScores;
    private SpawnController SpawnController;
    private GameObject powerup;
    private GameObject newWeapon;

    private void Awake()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        UIAndScores = GameControllerObject.GetComponent<UIAndScores>();
        SpawnController = GameControllerObject.GetComponent<SpawnController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (rend)
        { origmat = rend.material; }
        //Debug.Log(rend.material.GetColor("_Color"));
        powerup = SpawnController.Powerups[Random.Range(0, 3)];
        newWeapon = SpawnController.Powerups[Random.Range(3, 5)];
    }

    public void TakeDamage(int Amount)
    {

        Health -= Amount;
        if (Health <= 0)
        {
            if (Explosion != null)// its say if Explosion field in unity inspector in scripts IS not empty, so go on and if not, so skip this if.
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            UIAndScores.AddScore(ScoreValue);
            if (SpawnController.pTrigger==true)
            {
                if (Random.value <= 0.06)
                {
                    Instantiate(powerup, transform.position, Quaternion.identity);
                    SpawnController.startPowerUpSpawnWaitFunc();
                }
                else if (Random.value <= 0.2)
                {
                    Instantiate(newWeapon, transform.position, Quaternion.identity);
                    SpawnController.startPowerUpSpawnWaitFunc();
                }
            }
            Destroy(gameObject);
        }
        if (rend && flashMaterial)// this code is for error, when the rend or flashmaterial in unity is empty, error happends
        {
            rend.material = flashMaterial;//change the material of gameobject for seconds
            Invoke("Resetcolor", 0.04f);//enemy material flash for 0.04 second
        }
    }

    void Resetcolor()
    {
        rend.material = origmat;
    }
}
