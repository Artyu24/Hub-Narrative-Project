using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    BeganTouch(ref touch);
                    break;

                case TouchPhase.Moved:
                    MovedTouch(ref touch);
                    break;

                case TouchPhase.Stationary:
                    StationaryTouch(ref touch);
                    break;

                case TouchPhase.Ended:
                    EndedTouch(ref touch);
                    break;
            }
        }
    }

    private void BeganTouch(ref Touch touch)
    {
        //Debug.Log("Began " + touch.fingerId + " : " + touch.position);
    }

    private void MovedTouch(ref Touch touch)
    {
        Debug.Log("Moved " + touch.fingerId + " : " + touch.position);
    }

    private void StationaryTouch(ref Touch touch)
    {
        Debug.Log("Stationary " + touch.fingerId + " : " + touch.deltaTime);
        MenuController.Instance.Validation = true;
    }

    private void EndedTouch(ref Touch touch)
    {
        //Debug.Log("Ended " + touch.fingerId + " : " + touch.position);

        if (OnSlideInput(touch))
        {
            MenuController.Instance.OnChangeStory((int)Mathf.Sign(touch.deltaPosition.x));
            Debug.Log("Slide : " + (int)touch.deltaPosition.x);
        }
        MenuController.Instance.Validation = false;
    }

    private bool OnSlideInput(Touch touch)
    {
        return Mathf.Abs(touch.deltaPosition.x) >= 2;
    }
}