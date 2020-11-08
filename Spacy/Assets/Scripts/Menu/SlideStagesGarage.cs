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
    private float initialX;
    private float destination;
    private bool getBack = false;
    private float rotateT;
    private float backT;

    private void Awake()
    {
        cameraT =this.transform.localPosition;
        initialX = cameraT.x;
        destination = initialX;
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

        if (next || prev) NextMove();
        //else if (prev) PrevMove();
        //else if (getBack) GetBack();
        //if ( backT == 1) getBack = false;
        //Debug.Log(getBack);
    }

   /* private void GetBack()
    {
        Tuple<Vector3, float> smooth = SmoothlyMove(this.transform.localPosition.x, initialX, backT);
        this.transform.localPosition = smooth.Item1;
        backT = smooth.Item2;
    }*/

    private void SetSlideButtons(bool _prev, bool _next)
    {
        prevButton.SetActive(_prev);
        nextButton.SetActive(_next);
    }

   /* IEnumerator WaitForMenu()
    {
        yield return new WaitForSeconds(1);
        garage.SetActive(false);
    }*/

    private void NextMove()
    {
        this.transform.localPosition = SmoothlyMove(this.transform.localPosition.x, destination);
    }

    /*private void PrevMove()
    {
        this.transform.localPosition = SmoothlyMove(this.transform.localPosition.x, destination, rotateT).Item1;
        
    }*/

    public void PrevClick()
    {
        prev = true;
        currentStage--;
        destination -= stagesDistance;
        t = 0;
        //Debug.Log(Vector3.up+"   "+Vector3.forward);

    }

    public void NextClick()
    {
        garage.GetComponent<Animator>().enabled = false;
        next = true;
        currentStage++;
        destination += stagesDistance;
        t = 0;
        //Debug.Log(this.transform.localPosition.x);
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
            getBack = false;
            t = 0;
        }
    }

   /* public void BackToFirstStage()
    {
        getBack = true;
        currentStage = 1;
        destination = initialX;
        //this.transform.localPosition = SmoothlyMove(this.transform.localPosition.x, cameraT.x);
    }*/
   public void resetStage()
    {
        destination = initialX;
        //currentStage = 1;
    }
}
