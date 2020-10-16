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
    public float moveSpeed;
    public float t = 0.0f;
    public float stagesDistance = 15.0f;

    private float firstStageShift;
    private List<Transform> stagesTransform = new List<Transform>();
    private Vector3 cameraT;
    private bool next,prev = false;
    private float currentStage;
    private float currentX;
    private float destination;


    private void Awake()
    {
        cameraT = transform.position;
        firstStageShift = transform.position.x * -1;
        currentX = transform.localPosition.x;
        //prevButton.SetActive(false);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentStage = (currentX+firstStageShift)/stagesDistance;
        //Debug.LogFormat("currentX: {0}, firstStage: {1}, stageDistance: {2}",currentX,firstStageShift,stagesDistance);
        if (currentStage == 0)
        {
            prevButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if(currentStage == stages.Count-1)
        {
            prevButton.SetActive(true);
            nextButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
            nextButton.SetActive(true);
        }
        
        if (next || prev)
        {
            StartMove();
        }
    }

    private void StartMove()
    {

        transform.localPosition = SmoothlyMove(currentX-destination, currentX);
        //Debug.Log(currentX+15.0f);
    }

    public void PrevClick()
    {
        
        destination = -stagesDistance;
        currentX = transform.localPosition.x+destination;
        prev = true;

    }

    public void NextClick()
    {
        next = true;
        
        destination = stagesDistance;
        currentX = transform.localPosition.x+destination;
        //Debug.LogFormat("NextClick   currenX: {0}, destination: {1}", currentX, destination);
    }

    private Vector3 SmoothlyMove( float startPos, float endPos)
    {
        Vector3 position = new Vector3(Mathf.Lerp(startPos, endPos, t += moveSpeed * Time.deltaTime), cameraT.y, cameraT.z);
        StopSwitching();
        //Debug.Log("return position");
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
