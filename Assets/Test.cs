using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform ballon;
    public HingeJoint joint;
    LineRenderer lineRenderer;
    LineRenderer LineRenderer
    {
        get
        {
            if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
            return lineRenderer;
        }
    }
    private void Update()
    {
        CreateLine();
        joint.connectedBody = LogicGame.instance.gift.GetComponent<Rigidbody>();
    }
    public void CreateLine()
    {
        LineRenderer.positionCount = 2;

        LineRenderer.SetPosition(0, LogicGame.instance.gift.position );

        LineRenderer.SetPosition(1, ballon.position);
       
    }

}
