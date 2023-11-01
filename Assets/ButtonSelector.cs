using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public int id;
    public int idBought;
    public int cost;
    public int value;
    public TextMeshProUGUI txtCost;
    public TextMeshProUGUI txtValue;
    public Image icon;
    public Button btnBuy;
    public bool interac;
    public GameObject check;
    private void Start()
    {
        //btnBuy.onClick.AddListener(Buy);
    }
    public void Init(int id, int cost, int value, Sprite icon)
    {
        this.id = id;
        this.cost = cost;
        this.value = value;
        this.icon.sprite = icon;
        txtCost.text = cost.ToString();
        txtValue.text = value.ToString();

        //if (cost > StartCollector.ins.totalStar)
        //{
        //    btnBuy.interactable = false;
        //    interac = btnBuy.interactable;
        //}
    }
    //public void Buy()
    //{
    //    // xử lý logic trừ tiền vàng, tăng icon v.v
    //    Debug.Log(id + " - " + cost + " - " + value);
    //}
}
