using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float panSpeed = 1.0f;
    private float rotSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        if (Input.GetKey(KeyCode.LeftShift))
            panSpeed = 2.0f;
        else
            panSpeed = 1.0f;
        
        if (Input.GetKey(KeyCode.UpArrow))
            gameObject.transform.Translate(Vector3.forward * panSpeed);
        if (Input.GetKey(KeyCode.DownArrow))
            gameObject.transform.Translate(Vector3.back * panSpeed);
        if (Input.GetKey(KeyCode.RightArrow))
            gameObject.transform.Rotate(Vector3.up, rotSpeed);
        if (Input.GetKey(KeyCode.LeftArrow))
            gameObject.transform.Rotate(Vector3.up, -rotSpeed);
    }
}
