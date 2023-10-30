using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollector : MonoBehaviour
{
    public static StartCollector ins;
    public List<DataButton> listDataBtn = new List<DataButton>();
    public List<DataReward> listDataRw = new List<DataReward>();
    public ButtonSelector prefab;
    public int totalCoin = 1200;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        for (int i = 0; i < listDataBtn.Count; i++)
        {
            listDataBtn[i].id = i;
            listDataRw[i].id = i;

            ButtonSelector btn = Instantiate(prefab, transform);
            btn.Init(i, listDataBtn[i].cost, listDataRw[i].value, listDataRw[i].icon);
        }
    }

}
