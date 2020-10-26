﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class RotateInGarage : MonoBehaviour
{

    public List<GameObject> spaceshipsToRotate = new List<GameObject>();
    public Camera Camera;
    public float sRotateSpeed = 10f;
    public float cRotateSpeed = 1.5f;
    public float cBackToPositonSpeed = 1f;
    public float sAutomaticRotateSpeed = 1f;

    private bool backCamera = false;
    private float tCamera = 0;
    public float tSpaceship = 0;
    private Vector3 prevPos = Vector3.zero;
    private bool hittedPlayer = false;
    private GameObject spaceship;
    private Quaternion cameraR;
    private bool canHit = true;
    private int curStage;  
    private float x;
    private float y;
    private float maxRotateS = 0.707f;
    private float minRotateS= -0.707f;


    private void Awake()
    {
        cameraR = Camera.transform.rotation;
        

    }

    void Update()
    {
        
        DetectHit();
        if (!hittedPlayer)
        {
            SlideStagesGarage SlideStagesGarage = Camera.GetComponent<SlideStagesGarage>();
            curStage = SlideStagesGarage.currentStage;
            
            AutoRotate();
        }
        if (hittedPlayer)
        {
            //Debug.LogFormat("Y_Hitted: {0}, maxR: {1}, minR: {2}", -spaceshipsToRotate[0].transform.localRotation.y, maxRotateS, minRotateS);
            //t2 = 0;
            tSpaceship = (-spaceshipsToRotate[0].transform.localRotation.y + maxRotateS) / (-minRotateS + maxRotateS);
            //StartRotateSpaceship();
        }
        else if (Input.GetMouseButton(0))
        {
            canHit = false;
            backCamera = false;
            RotateCamera();
            prevPos = Input.mousePosition;
            tCamera = 0;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //t2 = (-spaceshipsToRotate[0].transform.localRotation.y + maxRotateS) / (minRotateS + maxRotateS);
            backCamera = true;
        }
        else
        {

            canHit = true;
        }

        if (Input.GetMouseButtonUp(0)) hittedPlayer = false;
        if(backCamera) Camera.transform.rotation = Quaternion.Euler(SmoothlyMove(x, cameraR.eulerAngles.x, y, cameraR.eulerAngles.y));
    }

    private Vector3 SmoothlyMove(float startPosx, float endPosx, float startPosy, float endPosy)
    {
        if (startPosx > 180) startPosx -= 360;
        Vector3 comingBackRotate = new Vector3(Mathf.Lerp(startPosx, endPosx, tCamera += cBackToPositonSpeed * Time.deltaTime), Mathf.Lerp(startPosy, endPosy, tCamera += cBackToPositonSpeed * Time.deltaTime), 0);
        tCamera = StopSwitching(tCamera);
        return comingBackRotate;
    }

    private float StopSwitching(float _t)
    {
        if (_t > 1)
        {
            backCamera = false;
            _t = 0;
        }

        return _t;
    }

    private void RotateCamera()
    {
        Vector2 cameraRotateBoundary = new Vector2(15.8f, 6.7f);
        Camera.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * cRotateSpeed, -Input.GetAxis("Mouse X") * cRotateSpeed, 0));
        x = Camera.transform.rotation.eulerAngles.x;
        y = Camera.transform.localRotation.eulerAngles.y;

        x = CheckRotateBoundary(cameraR.eulerAngles.x, cameraRotateBoundary.x, x);
        y = CheckRotateBoundary(cameraR.eulerAngles.y, cameraRotateBoundary.y, y);

        Camera.transform.rotation = Quaternion.Euler(x, y, 0);
    }

    private void StartRotateSpaceship()
    {
        if (sRotateSpeed < 1) sRotateSpeed = 1f;
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 deltaPos = (Input.mousePosition - prevPos) / sRotateSpeed;
            if (Vector3.Dot(spaceship.transform.up, Vector3.up) >= 0)
            {
                spaceship.transform.Rotate(spaceship.transform.up, -Vector3.Dot(deltaPos, Camera.main.transform.right), Space.World);
            }
            else
            {
                spaceship.transform.Rotate(spaceship.transform.up, Vector3.Dot(deltaPos, Camera.main.transform.right), Space.World);
            }

            //hit.transform.gameObject.transform.Rotate(Camera.main.transform.right, Vector3.Dot(deltaPos, Camera.main.transform.up), Space.World);
        }
        prevPos = Input.mousePosition;
    }

    private void DetectHit()
    {
        if (canHit)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
                {
                    if (hit.transform != null)
                    {
                        if (hit.transform.gameObject.CompareTag("Player"))
                        {
                            hittedPlayer = true;
                            spaceship = hit.transform.gameObject;
                        }
                    }
                }
            }
        }
    }
    private float CheckRotateBoundary(float cameraAngle, float RotateBoundary, float finalXY)
    {
        if (cameraAngle - RotateBoundary <0)
        {
            float middle = (2 * cameraAngle + 360f) / 2;
            if (finalXY > cameraAngle +RotateBoundary && x < middle) finalXY = cameraAngle + RotateBoundary;
            else if (finalXY < 360f + (cameraAngle - RotateBoundary) && finalXY >= middle) finalXY = 360f + (cameraAngle - RotateBoundary);
        }
        else
        {
            if (finalXY > cameraAngle+RotateBoundary) finalXY = cameraAngle + RotateBoundary;
            else if (finalXY < cameraAngle-RotateBoundary) finalXY = cameraAngle - RotateBoundary;
        }
        return finalXY;
    }

    public void SlidingClick()
    {
        Camera.transform.rotation = cameraR;
    }

    private void AutoRotate()
    {
        Vector3 rSapceship = new Vector3(spaceshipsToRotate[curStage - 1].transform.rotation.x, Mathf.Lerp(spaceshipsToRotate[curStage - 1].transform.rotation.y, spaceshipsToRotate[curStage - 1].transform.rotation.y+360f, tSpaceship += 0.1f * Time.deltaTime), 0);
        spaceshipsToRotate[curStage-1].transform.rotation = Quaternion.Euler(rSapceship);
        tSpaceship = StopSwitching(tSpaceship);
        if (spaceshipsToRotate[curStage - 1].transform.localRotation.y > maxRotateS) maxRotateS = spaceshipsToRotate[curStage - 1].transform.localRotation.y;
        else if (minRotateS > spaceshipsToRotate[curStage - 1].transform.localRotation.y) minRotateS = spaceshipsToRotate[curStage - 1].transform.localRotation.y;
        //Debug.LogFormat("y: {0}", spaceshipsToRotate[curStage - 1].transform.localRotation.y);
    }
}