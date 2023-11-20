using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public EasyController easyController;
    private void Update()
    {
        Vector3 dv = easyController.dV;
        if (dv != Vector3.zero)
        {
            Vector3 v = transform.eulerAngles + new Vector3(-dv.y, dv.x, 0);
            v.x = Mathf.Clamp(v.x, 20,55);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, v, 0.075f);
        }
    }
}
