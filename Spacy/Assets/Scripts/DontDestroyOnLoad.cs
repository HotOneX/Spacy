using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontdestroyOnLoad : MonoBehaviour
{
    static GameObject instance=null;
    public static int lastcheckpoint;
    private void Awake()
    {
        lastcheckpoint = 0;
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
