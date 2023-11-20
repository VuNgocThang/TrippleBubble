using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BodyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private GameObject placeHolderPrefab;
    private List<GameObject> cells;
    public List<ButtonManager> buttonsManager;

    public void Initialize(int year, int month)
    {
        var dateTime = new DateTime(year, month, 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);

        var dayOfWeek = (int)dateTime.DayOfWeek;
        var size = (dayOfWeek + daysInMonth) / 7;

        if ((dayOfWeek + daysInMonth) % 7 > 0)
            size++;

        var arr = new int[size * 7];

        for (var i = 0; i < daysInMonth; i++)
            arr[dayOfWeek + i] = i + 1;

        if (cells == null)
            cells = new List<GameObject>();

        foreach (var c in cells)
            Destroy(c);

        cells.Clear();

        foreach (var a in arr)
        {
            var instance = Instantiate(a == 0 ? placeHolderPrefab : buttonPrefab, transform);
            var buttonManager = instance.GetComponent<ButtonManager>();

            if (buttonManager != null)
            {
                buttonManager.Initialize(a.ToString());
                buttonsManager.Add(buttonManager);
            }
            cells.Add(instance);
        }
    }

    public void OnClickButton()
    {
        foreach (var button in buttonsManager)
        {
            button.button.onClick.AddListener(() =>
            {
                OnClickState();
                SetSelected(button, true);
            });
        }
    }
    public void Test()
    {
        buttonsManager[0].isDone = true;
        buttonsManager[0].label.color = Color.red;
    }
    public void OnClickState()
    {
        foreach (var button in buttonsManager)
        {
            if (!button.isDone)
            {
                SetSelected(button, false);
            }
        }
    }

    public void SetSelected(ButtonManager btn, bool selected)
    {
        btn.isSelected = selected;

        if (btn.isSelected)
        {
            btn.mark.SetActive(true);
        }
        else
        {
            btn.mark.SetActive(false);
        }
    }

}
