using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpin : MonoBehaviour
{
    void Start()
    {

    }


    Vector3 v;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) v = new Vector3(0, 60, 0) * Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.A)) v = new Vector3(0, 0, 0) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.D)) v = new Vector3(0, -60, 0) * Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.D)) v = new Vector3(0, 0, 0) * Time.deltaTime;

        transform.eulerAngles += v;
    }
}
