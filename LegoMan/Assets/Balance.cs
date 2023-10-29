using System;
using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private int balance;
    [SerializeField] private TextMeshProUGUI balanceUI;
    [SerializeField] private GameEvent updateDisabledShop;

    private void Start()
    {
        UpdateUI();
    }

    public void Trade(Component sender, object data)
    {
        var amount = (int)data;
        if (balance + amount >= 0)
        {
            balance += amount;
        }
        else
        {
            balance = 0;
        }

        UpdateUI();
    }

    public int GetBalance()
    {
        return balance;
    }

    private void UpdateUI()
    {
        balanceUI.text = balance.ToString();
        updateDisabledShop.Raise();
    }
}