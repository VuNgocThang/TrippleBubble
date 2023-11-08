using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    public int maxNetworks = 5;
    public float networkRechargeTime = 300; 

    private int currentNetworks;
    private DateTime lastNetworkLossTime;

    void Start()
    {
        currentNetworks = maxNetworks;
        lastNetworkLossTime = DateTime.Now; 
    }

    void Update()
    {
        TimeSpan timeSinceLoss = DateTime.Now - lastNetworkLossTime;

        if (currentNetworks < maxNetworks && timeSinceLoss.TotalSeconds >= networkRechargeTime)
        {
            currentNetworks++;
            lastNetworkLossTime = DateTime.Now;
        }
    }

    public void LoseNetwork()
    {
        if (currentNetworks > 0)
        {
            currentNetworks--;
            lastNetworkLossTime = DateTime.Now;
        }
    }

    public int GetCurrentNetworks()
    {
        return currentNetworks;
    }
}
