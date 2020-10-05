using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSettings : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsPanel;
    public bool inSettings;
    public float t;
    public float moveSpeed;

    private bool switching = false;
    private RectTransform mainMenuTransform;
    private float mLeft, mRight;
    private RectTransform settingsPanelTransform;
    private float sLeft, sRight;

    private void Awake()
    {
        mainMenuTransform = mainMenu.GetComponent<RectTransform>();
        settingsPanelTransform = settingsPanel.GetComponent<RectTransform>();
        mLeft = mainMenuTransform.rect.xMin;
        mRight = mainMenuTransform.rect.xMax;
        sLeft = settingsPanelTransform.rect.xMin;
        sRight = settingsPanelTransform.rect.xMax;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        /*if (!inSettings)
        {

        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (switching)
        {
            StartToggling(inSettings);
        }
    }

    private void StartToggling(bool menuStatus)
    {
        if (menuStatus)
        {
            //mainMenuTransform.rect.xMin = SmoothlyMove(0f, -720f);
            //mainMenuTransform.rect.xMax = SmoothlyMove(0f, 720f);

        }
        else
        {
            //mainMenuTransform.localPosition = SmoothlyMove(-720f, 720f, 0f, 0f);

        }
    }

    private float SmoothlyMove(float start, float end)
    {
        float position = Mathf.Lerp(start, end, t += moveSpeed * Time.deltaTime);
        StopSwitching();
        return position;
    }

    private void StopSwitching()
    {
        throw new NotImplementedException();
    }

    public void SettingsTouched()
    {
        switching = true;
        Debug.Log(settingsPanelTransform.localPosition);
        Debug.Log(settingsPanelTransform);
    }
}
