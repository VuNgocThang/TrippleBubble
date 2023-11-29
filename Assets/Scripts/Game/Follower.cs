using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 5f;
    float distacne;

    private void Start()
    {
        creator = LogicGame.instance.pathCreater;
    }
    private void Update()
    {
        distacne += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distacne);
        //transform.rotation = creator.path.GetRotationAtDistance(distance);
    }
}
