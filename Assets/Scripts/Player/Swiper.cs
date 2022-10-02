using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour
{

    private Touch initTouch = new Touch();
    GameObject Camera;
    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;

    public float rotSpeed = 1f;
    public float dir = -1;
    void Start()
    {
        Camera = PlayerController.instance.viewPoint.gameObject;
        origRot = Camera.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }
    void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = initTouch.position.x - touch.position.x;
                float deltaY = initTouch.position.y - touch.position.y;
                rotX -= deltaX * Time.deltaTime * rotSpeed * dir;
                rotY += deltaY * Time.deltaTime * rotSpeed * dir;
                Mathf.Clamp(rotX, -45f, 45f);
                Camera.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
            }
        }
    }

}