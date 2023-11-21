using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    [SerializeField] HeaderManager headerManager;
    [SerializeField] BodyManager bodyManager;

    private DateTime targetDateTime;
    private CultureInfo cultureInfo;
    [SerializeField] Button btnPrev;
    [SerializeField] Button btnNext;


    private void Start()
    {
        targetDateTime = DateTime.Now;
        Debug.Log(targetDateTime.Day);
        cultureInfo = new CultureInfo("en-US");
        Refresh(targetDateTime.Year, targetDateTime.Month);
        bodyManager.SetSelected(bodyManager.buttonsManager[targetDateTime.Day - 1], true);
        bodyManager.SetStateBeforeNow();
        bodyManager.OnClickButton();

        btnPrev.onClick.AddListener(OnGoToPreviousMonthButtonClicked);
        btnNext.onClick.AddListener(OnGoToNextMonthButtonClicked);
    }
    public void OnGoToPreviousMonthButtonClicked()
    {
        if (targetDateTime.Month < 2) return;

        targetDateTime = targetDateTime.AddMonths(-1);
        Refresh(targetDateTime.Year, targetDateTime.Month);
        bodyManager.SetStateBeforeNow();
        bodyManager.OnClickButton();

    }
    public void OnGoToNextMonthButtonClicked()
    {
        if (targetDateTime.Month > 11) return;

        targetDateTime = targetDateTime.AddMonths(+1);
        Refresh(targetDateTime.Year, targetDateTime.Month);
        bodyManager.SetStateBeforeNow();
        bodyManager.OnClickButton();
    }

    private void Refresh(int year, int month)
    {
        headerManager.SetTitle($"{year} {cultureInfo.DateTimeFormat.GetMonthName(month)}");
        bodyManager.buttonsManager.Clear();
        bodyManager.Initialize(year, month);
    }

}
