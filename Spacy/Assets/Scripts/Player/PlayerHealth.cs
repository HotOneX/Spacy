using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private bool damaged;
    public Animator Anim;
    public int life;
    public Image[] LifeImages;
    private int i = 0;

    private PlayerController PlayerController;
    public GameObject PlayerExplosion;
    public UIAndScores UIAndScores;


    private void Awake()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        damaged = false;
        PlayerController = GetComponent<PlayerController>();
        UIAndScores = GameControllerObject.GetComponent<UIAndScores>();
        //Anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player"))
        {
            if (other.CompareTag("Laser"))
            {
                PlayerTakeDamage();
                return;
            }
            if (other.CompareTag("EnemyBolt"))
            {
                Destroy(other.gameObject);
                PlayerTakeDamage();
            }
            else if (other.CompareTag("Enemy"))
            {
                EnemyHealth EnemyHealth = other.GetComponent<EnemyHealth>();
                EnemyHealth.TakeDamage(20);
                PlayerTakeDamage();
            }
        }
    }

    IEnumerator WaitTimeflashing()
    {
        Anim.SetBool("isFading", true);
        yield return new WaitForSeconds(3f);
        Anim.SetBool("isFading", false);
        damaged = false;
        //Player.GetComponent<Animation>().enabled = false;
    }


    public void PlayerTakeDamage()
    {
        if (damaged == false)
        {
            damaged = true;
            life--;
            Destroy(LifeImages[i]);
            i++;
            if (PlayerController.weaponLevel > 0)
                PlayerController.weaponLevel--;

            if (life <= 0)
            {
                Instantiate(PlayerExplosion, transform.position, transform.rotation);
                UIAndScores.GameOver();
                gameObject.SetActive(false);
            }
            else
                StartCoroutine(WaitTimeflashing());
        }
    }

}
