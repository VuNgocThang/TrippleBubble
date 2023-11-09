using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    public TextMeshProUGUI rewardToShow;
    public Transform hand;
    public Vector3 startPos;
    public Vector3 endPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rewardNo"))
        {
            var multiplier = other.gameObject.name;
            rewardToShow.text = (50 * float.Parse(multiplier)).ToString();
            PlayerPrefs.SetFloat("reward", float.Parse(rewardToShow.text));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Move();
            StartCoroutine(StopMove());
        }
    }

    void Move()
    {
        hand.DOLocalMove(endPos, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
        {
            hand.DOLocalMove(startPos, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(Move);
        });
    }

    IEnumerator StopMove()
    {
        float t = Random.Range(0f, 4f);
        Debug.Log(t);
        yield return new WaitForSeconds(t);
        DOTween.Kill(hand);
    }
}
