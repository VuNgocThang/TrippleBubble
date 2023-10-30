using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfigJointsScript : MonoBehaviour
{
    public List<ConfigurableJoint> joints = new List<ConfigurableJoint>();
    public void InitJoints()
    {
        List<Bubble> listBubbles = LogicGame.instance.level.bubbles;

        for (int i = 0; i < listBubbles.Count; i++)
        {
            InitJoint(listBubbles[i]);
        }
    }
    public void InitJoint(Bubble bubble)
    {
        ConfigurableJoint joint = transform.AddComponent<ConfigurableJoint>();
        joints.Add(joint);
        joint.connectedBody = bubble.GetComponent<Rigidbody>();
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;
    }
    public void RemoveBubbleFromJoints(Bubble bubble)
    {
        List<Bubble> listBubbles = LogicGame.instance.level.bubbles;

        int index = listBubbles.IndexOf(bubble);

        if (index >= 0 && index < joints.Count)
        {
            Destroy(joints[index]);
            joints.RemoveAt(index);
            listBubbles.RemoveAt(index);
        }
    }

}
