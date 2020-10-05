﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public bool isOn;
    public GameObject background;
    public GameObject checkMark;
    public float t = 0.0f;
    public float moveSpeed;
    //public RectTransform toggle;

    private RectTransform checkMarkTransform;
    private Color backgroundColorON = new Color(110f / 255f, 214f / 255f, 119f / 255f, 1f);
    private Color backgroundColorOff = new Color(254f / 255f, 101f / 255f, 70f / 255f, 1f);
    private Image backgroundColor;

    private float checkMarkSize;
    private bool switching;

    private void Awake()
    {
        checkMarkTransform = checkMark.GetComponent<RectTransform>();
        checkMarkSize = checkMarkTransform.sizeDelta.x;
        backgroundColor = background.GetComponent<Image>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        if (isOn)
        {
            checkMarkTransform.localPosition = new Vector3(-27, 0, 0);
            backgroundColor.color = backgroundColorON;
            
        }
        else
        {
            checkMarkTransform.localPosition = new Vector3(27, 0, 0);
            backgroundColor.color = backgroundColorOff;
        }

        Debug.Log(backgroundColor.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (switching)
        {
            StartToggling(isOn);
        }

        //Debug.Log(switching);
        //Debug.Log(checkMarkTransform.localPosition);
    }

    private void StartToggling(bool tStatus)
    {
        if (tStatus)
        {
            checkMarkTransform.localPosition = SmoothlyMove(checkMark,-27,27);
            backgroundColor.color = backgroundColorOff;
        }
        else
        {
            checkMarkTransform.localPosition = SmoothlyMove(checkMark, 27, -27);
            backgroundColor.color = backgroundColorON;
        }

        //Debug.Log(checkMarkTransform.localPosition);
    }

    private Vector3 SmoothlyMove(GameObject checkMark, float startPos, float endPos)
    {
        Vector3 position = new Vector3(Mathf.Lerp(startPos, endPos, t += moveSpeed * Time.deltaTime),0,0);
        StopSwitching();
        return position;
    }

    private void StopSwitching()
    {
        if(t > 1)
        {
            switching = false;
            t = 0;

            switch (isOn)
            {
                case true:
                    isOn = false;
                    break;
                case false:
                    isOn = true;
                    break;
            }
        }
    }

    public void Switch()
    {
        switching = true;
        Debug.Log(switching);
    }
}