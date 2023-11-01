using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarHandler : MonoBehaviour
{
    public int star;
    public TextMeshProUGUI txtStar;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("NumStar"))
        {
            star = 0;
            PlayerPrefs.SetInt("NumStar", star);
            PlayerPrefs.Save();
        }
        else
        {
            star = PlayerPrefs.GetInt("NumStar");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            star += 1;
            PlayerPrefs.SetInt("NumStar", star);
            PlayerPrefs.Save();
        }
    }

    private void OnGUI()
    {
        txtStar.text = star.ToString();
    }
}
