using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationControll : MonoBehaviour
{
    private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        StartCoroutine(animationPlay());
    }

    IEnumerator animationPlay()
    {
        while(true)
        {
            if (Random.value <= 0.2)
            {
                Anim.SetTrigger("1");
                //yield return new WaitForSeconds(Random.Range(3f, 6f));
            }
            else if (Random.value <= 0.2)
            {
                Anim.SetTrigger("2");
                //yield break;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(3f, 6f));
            }
        }
    }
}
