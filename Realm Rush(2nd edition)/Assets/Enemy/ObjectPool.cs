using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0.1f,30f)]float spawnTime=1f;
    [SerializeField] [Range(0f,50f)]int poolSize=5;
    GameObject[]pool;

    // Start is called before the first frame update
    void Start()
    {

        populateEnemy();
        StartCoroutine(spawnEnemy());
        
    }
    void populateEnemy()
    {
        pool=new GameObject[poolSize];
        for(int i=0;i<poolSize;i++)
        {
            pool[i]=Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }

    }
    IEnumerator spawnEnemy()
    {
        while(true)
        {
            
            enableEnemy();
            
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void enableEnemy()
    {

        for(int i=0;i<poolSize;i++)
            {
            
                if(!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    return;
                   
                }

            }
        
    }

   
}
