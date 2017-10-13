using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public int startingMoney = 10;
    public Text moneyText;
    public static MoneyManager moneyManager;

    public int money { get; private set;}

    private void Awake()
    {
        if (moneyManager == null)
        {
            moneyManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        money = startingMoney;
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyText.text = "Money: $" + money;
    }

    public void GainMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        UpdateUI();
    }

    public void Reset()
    {
        money = startingMoney;
        UpdateUI();
    }
}
