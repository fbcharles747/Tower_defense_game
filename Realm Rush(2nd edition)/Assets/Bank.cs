using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    int startingGold=300;
    [SerializeField] public int currentGold;
    [SerializeField] TextMeshProUGUI displayBalance;
    // Start is called before the first frame update
    void Start()
    {
        currentGold=startingGold;
        updateDisplay();
    }

    public void deposit(int amount)
    {
        currentGold+=Mathf.Abs(amount);
        updateDisplay();
    }
    public void withDraw(int amount)
    {
        currentGold-=Mathf.Abs(amount);
        if(currentGold<0)
        {
            reloadScene();
        }
        updateDisplay();
    }
    void updateDisplay()
    {
        displayBalance.text="Gold: "+currentGold;
    }

    void reloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
