using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AnimationPopup : MonoBehaviour
{
    public static AnimationPopup instance;
    public AnimationCurve curveZoomIn;
    public AnimationCurve curveZoomOut;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AnimScaleZoom(Transform trans)
    {
        trans.DOScale(new Vector3(1f, 1f, 1f), 0.5f)
            .SetEase(curveZoomIn);
    }

    public void AnimScaleZero(GameObject panel, Transform trans)
    {
        trans.DOScale(new Vector3(0f, 0f, 0f), 0.5f)
            .SetEase(curveZoomOut)
            .OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
    }

    public void AnimMoveIn(GameObject obj)
    {
        DoTween_Button(obj, 200, 200, 0.5f);
    }


    public void DoTween_Button(GameObject Obj, int posX, int posY, float timer)
    {
        Vector3 pos = Obj.GetComponent<RectTransform>().anchoredPosition;
        Obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(posX, posY);
        Obj.GetComponent<RectTransform>().DOAnchorPos(pos, timer);
    }
}
