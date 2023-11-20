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
        cultureInfo = new CultureInfo("en-US");
        Refresh(targetDateTime.Year, targetDateTime.Month);
        bodyManager.OnClickButton();
        bodyManager.Test();

        btnPrev.onClick.AddListener(OnGoToPreviousMonthButtonClicked);
        btnNext.onClick.AddListener(OnGoToNextMonthButtonClicked);
    }
    public void OnGoToPreviousMonthButtonClicked()
    {
        targetDateTime = targetDateTime.AddMonths(-1);
        Refresh(targetDateTime.Year, targetDateTime.Month);
    }
    public void OnGoToNextMonthButtonClicked()
    {
        targetDateTime = targetDateTime.AddMonths(+1);
        Refresh(targetDateTime.Year, targetDateTime.Month);
    }

    private void Refresh(int year, int month)
    {
        headerManager.SetTitle($"{year} {cultureInfo.DateTimeFormat.GetMonthName(month)}");
        bodyManager.Initialize(year, month);
    }

}
