using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    static GameObject instance=null;
    public int lastcheckpoint;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
