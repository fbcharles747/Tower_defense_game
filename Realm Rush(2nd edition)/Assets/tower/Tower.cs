using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost=75;
    [SerializeField] float buildDelay=1f;

    private void Start() {
        StartCoroutine(Build());
        
    }
    public bool createTower(Tower tower,Vector3 position)
    {
        Bank bank=FindObjectOfType<Bank>();
        if(bank==null){
            return false;
        }

        if(bank.currentGold>=cost)
        {
            Instantiate(gameObject,position,Quaternion.identity);
            bank.withDraw(cost);
            return true;
        }
        return false;
    }
    IEnumerator Build()
    {
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
            foreach(Transform grantchild in child){
                grantchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grantchild in child){
                grantchild.gameObject.SetActive(true);
            }
        }
    }
}
