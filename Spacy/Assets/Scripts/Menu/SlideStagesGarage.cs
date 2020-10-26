using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlideStagesGarage : MonoBehaviour
{
    public List<GameObject> stages = new List<GameObject>();
    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject mainCamera;
    public GameObject garage;
    public float moveSpeed;
    public float t = 0.0f;
    public float stagesDistance = 15.0f;

    private Vector3 cameraT;
    private bool next,prev = false;
    public int currentStage = 1;
    private float destination;


    private void Awake()
    {
        cameraT =this.transform.localPosition;
        destination = cameraT.x;
}

    private void Start()
    {
        
    }

    void Update()
    {
        switch (currentStage)
        {
            case 1:
                SetSlideButtons(false, true);
                break;
            default:
                if (currentStage == stages.Count) SetSlideButtons(true, false);
                else SetSlideButtons(true, true);
                break;
        }

        if (next) NextMove();
        else if (prev) PrevMove();

        /*if (mainCamera.activeSelf)
        {
            SetSlideButtons(false, true);
            this.transform.localPosition = cameraT;        
            StartCoroutine(nameof(WaitForMenu));
        }*/
    }

    private void SetSlideButtons(bool _prev, bool _next)
    {
        prevButton.SetActive(_prev);
        nextButton.SetActive(_next);
    }

    IEnumerator WaitForMenu()
    {
        yield return new WaitForSeconds(1);
        garage.SetActive(false);
    }

    private void NextMove()
    {
        this.transform.localPosition = SmoothlyMove(transform.position.x, destination);
    }

    private void PrevMove()
    {
        this.transform.localPosition = SmoothlyMove(this.transform.localPosition.x, destination);

    }

    public void PrevClick()
    {
        prev = true;
        currentStage--;
        destination -= stagesDistance;
        t = 0;
        
    }

    public void NextClick()
    {
        next = true;
        currentStage++;
        destination += stagesDistance;
        t = 0;
    }
    
   /* public void MenuClick()
    {
        /*for(int i = currentStage; i > 1; i--)
        {
            PrevClick();
        }

        SetSlideButtons(false, true);
        this.transform.localPosition = cameraT;
    }*/

    private Vector3 SmoothlyMove( float startPos, float endPos)
    {
        Vector3 position = new Vector3(Mathf.Lerp(startPos, endPos, t += moveSpeed * Time.deltaTime), cameraT.y, cameraT.z);
        StopSwitching();
        return position;
    }

    private void StopSwitching()
    {
        if (t > 1)
        {
            next = false;
            prev = false;
            t = 0;
        }
    }
}
