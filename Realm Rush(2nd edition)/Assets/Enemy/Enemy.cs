using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int reward=25;
    int penalty=25;
    Bank bank;
    
    // Start is called before the first frame update
    void Start()
    {
        bank=FindObjectOfType<Bank>();
    }

    public void rewardGold()
    {
        bank.deposit(reward);
    }
    public void stealGold()
    {
        bank.withDraw(penalty);
    }
   
}
