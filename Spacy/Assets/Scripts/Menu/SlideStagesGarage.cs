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
    public GameObject garage;
    public float moveSpeed;
    public float t = 0.0f;
    public float stagesDistance = 15.0f;
    public int currentStage = 1;

    private Vector3 cameraT;
    private bool next,prev = false;
    private float initialX;
    private float destination;
    private bool BTM = false;
    private Animator ToMenu;

    private void Awake()
    {
        cameraT =this.transform.localPosition;
        initialX = cameraT.x;
        destination = initialX;
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

        if (BTM)
        {
            NextMove();
            if (this.transform.localPosition.x == destination) BTM = false;
            else if (Mathf.Abs(this.transform.localPosition.x - destination) < 1)
            {
                
                ToMenu = garage.GetComponent<Animator>();
                ToMenu.enabled = true;
                ToMenu.Play("BackToMenu");
            }
        }
        else if (next || prev) NextMove();
    }

    private void SetSlideButtons(bool _prev, bool _next)
    {
        prevButton.SetActive(_prev);
        nextButton.SetActive(_next);
    }

    private void NextMove()
    {
        this.transform.localPosition = SmoothlyMove(this.transform.localPosition.x, destination);
    }

    public void PrevClick()
    {
        if (currentStage > 1)
        {
            prev = true;
            currentStage--;
            destination -= stagesDistance;
            t = 0;
        }
    }

    public void NextClick()
    {
        if (currentStage < stages.Count)
        {
            garage.GetComponent<Animator>().enabled = false;
            next = true;
            currentStage++;
            destination += stagesDistance;
            t = 0;
        }
    }

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

   public void ResetStage()
    {
        destination = initialX;
    }

    public void BackToMenu()
    {
        destination = initialX;
        currentStage = 1;
        t = 0;
        BTM = true;
        
    }
}
