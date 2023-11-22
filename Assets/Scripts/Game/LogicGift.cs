using UnityEngine;
using DG.Tweening;
using System.Collections;
using PathCreation;
using UnityEngine.UIElements;

public class LogicGift : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 0.2f;
    float distacne;
    bool checkOut;

    private void Update()
    {
        distacne += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distacne);

        if (IsObjectOutOfEyes() && !checkOut)
        {
            checkOut = true;
        }
    }

    bool IsObjectOutOfEyes()
    {
        Vector3 pos = transform.position;

        return (pos.y < -1f);
    }
}
