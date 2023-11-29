using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public EasyController easyController;
    public Transform nX;
    private void Update()
    {
        Vector3 dv = easyController.dV;
        if (dv != Vector3.zero)
        {
            nX.localEulerAngles += new Vector3(0, -dv.x, 0) * 30f * Time.deltaTime;
            float f = Mathf.Clamp(transform.localEulerAngles.x  -dv.y * 30f * Time.deltaTime, 0, 50);
            transform.localEulerAngles = new Vector3(f, 0, 0);
            LogicGame.instance.UpdateLine();
        }
    }
}
