using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Sprite bgOn;
    [SerializeField] Sprite bgOff;
    [SerializeField] Image bgImage;

    Toggle toggle;

    Vector2 handlePosition;

    bool canToggle;

    private void Awake()
    {
        canToggle = true;

        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    void OnSwitch(bool on)
    {
        Image bgImg = bgImage.GetComponent<Image>();
        if (canToggle)
        {
            canToggle = false;

            uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, 0.2f)
                .SetEase(Ease.InOutBack)
                .OnComplete(() =>
                {
                    bgImg.sprite = on ? bgOn : bgOff;
                    canToggle = true;
                });
        }

        //uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, 0.4f)
        //       .SetEase(Ease.InOutBack);



    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
