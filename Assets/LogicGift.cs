using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LogicGift : MonoBehaviour
{
    private Vector3 defaultPosition;

    void Start()
    {
        defaultPosition = transform.position;
        StartCoroutine(ContinuousShake());
    }

    IEnumerator ContinuousShake()
    {
        while (true)
        {
            transform.DOShakePosition(4f, new Vector3(0.1f, 0f, 0f), 1, 10, false, true);

            yield return new WaitForSeconds(4f);
        }
    }
}
