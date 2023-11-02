using UnityEngine;
using DG.Tweening;
using System.Collections;
using PathCreation;

public class LogicGift : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 0.2f;
    float distacne;

    private void Update()
    {
        distacne += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distacne);
        //transform.rotation = creator.path.GetRotationAtDistance(distacne);
    }
}
