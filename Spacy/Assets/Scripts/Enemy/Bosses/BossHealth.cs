using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int Health;
    public GameObject Explosion;
    public Renderer rend;
    private Material origmat;
    public Material flashMaterial;
    public int ScoreValue;
    public Slider healthSlider;

    private UIAndScores UIAndScores;

    private void Awake()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        UIAndScores = GameControllerObject.GetComponent<UIAndScores>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rend)
        { origmat = rend.material; }
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = Health;
        healthSlider.value = Health;
        //Debug.Log(rend.material.GetColor("_Color"));
    }

    public void TakeDamage(int Amount)
    {
        UIAndScores.BulletLevelupSlider.value += Amount;
        Health -= Amount;
        healthSlider.value = Health;
        if (Health <= 0)
        {
            if (Explosion != null)// its say if Explosion field in unity inspector in scripts IS not empty, so go on and if not, so skip this if.
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            UIAndScores.AddScore(ScoreValue);
            Destroy(gameObject);
            UIAndScores.BulletLevelupSlider.value += Amount * 2;
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

    public void SetHealth(int h)
    {
        Health = h;
    }
}
