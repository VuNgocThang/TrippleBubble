using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHuongTam : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] Bubble bubble;
    private void FixedUpdate()
    {
        if (bubble.canMoveHT)
        {
            rb.velocity += (LogicGame.instance.targetThis.position - transform.position);
        }
    }
}
