using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    [SerializeField] HeaderManager headerManager;

    private DateTime targetDateTime;
    private CultureInfo cultureInfo;
    [SerializeField] Button btnPrev;
    [SerializeField] Button btnNext;

}
