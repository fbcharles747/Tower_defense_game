using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint=5;

    [SerializeField] int difficultyRamp=1;
    [SerializeField] int currentHitPoint;
    Enemy enemy;
    // Start is called before the first frame update

    void OnEnable()
    {
        currentHitPoint=maxHitPoint;
        
    }
    void Start() 
    {
        enemy=GetComponent<Enemy>();
    }
   

    private void OnParticleCollision(GameObject other)
    {
        processHit();
        
       
    }
    void processHit()
    {
        currentHitPoint--;
        if(currentHitPoint<=0)
        {
            gameObject.SetActive(false);
            enemy.rewardGold();
            maxHitPoint+=difficultyRamp;
            
        }

    }
}
