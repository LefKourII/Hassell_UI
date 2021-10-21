using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject pavilion;
    public float rotationSpeed;
    private float rotationSpeedInternal;

    private bool rotate;
    
    void Start()
    {
        rotate = true;
        rotationSpeedInternal = rotationSpeed;
    }

    void Update()
    {
        TestForTouch();
        Rotate();
    }

    public void CheckButtonStatus(int index)
    {
        if (rotate == false)
            rotate = true;
        else
            rotate = false;

        switch (index)
        {
            case 1:
                rotationSpeedInternal = rotationSpeed;
                break;
            case -1:
                rotationSpeedInternal = rotationSpeed * -1;
                break;
        };
    }

    void TestForTouch()
    {
        if (Input.touchCount > 0)
        {
            rotate = false;
        }
    }

    void Rotate()
    {
        if (rotate)
        {
            pavilion.transform.Rotate(Vector3.up, rotationSpeedInternal * Time.deltaTime);
        }
    }
}
