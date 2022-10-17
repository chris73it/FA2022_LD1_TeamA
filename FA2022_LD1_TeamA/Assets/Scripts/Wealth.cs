using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wealth : MonoBehaviour
{
    public int Money = 0;
    public int TotalMoney = 99;

    public int Deposit(int amount)
    {
        if (Money + amount > TotalMoney)
        {
            Money = TotalMoney;
        } else
        {
            Money += amount;
        }
        return Money;
    }

    public int Withdraw(int amount)
    {
        if (Money - amount <= 0)
        {
            Money = 0;
        } else
        {
            Money -= amount;
        }
        return Money;
    }
}
