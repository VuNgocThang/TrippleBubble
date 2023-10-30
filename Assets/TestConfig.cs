using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestConfig : MonoBehaviour
{
    public List<Transform> listBallon = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < listBallon.Count; i++)
        {
            ConfigurableJoint joints =  transform.AddComponent<ConfigurableJoint>();
            joints.connectedBody = listBallon[i].GetComponent<Rigidbody>();
            joints.xMotion = ConfigurableJointMotion.Limited;
            joints.yMotion = ConfigurableJointMotion.Limited;
            joints.zMotion = ConfigurableJointMotion.Limited;
            //joints.angularXMotion = ConfigurableJointMotion.Limited;
            //joints.angularYMotion = ConfigurableJointMotion.Limited;
            //joints.angularZMotion = ConfigurableJointMotion.Limited;
        }
    }
}
