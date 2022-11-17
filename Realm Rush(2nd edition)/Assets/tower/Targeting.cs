using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField]float range=15f;
    [SerializeField]Transform weapon;
    [SerializeField]Transform target;
    [SerializeField]ParticleSystem projectile;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        findTheClosestTarget();
        AimAtTarget();
        
    }
    void findTheClosestTarget()
    {
        Transform closestTarget=null;
        Enemy[] enemies=FindObjectsOfType<Enemy>();
        float maxDistance=Mathf.Infinity;
        foreach(Enemy enemy in enemies)
        {
            float targetDistance=Vector3.Distance(transform.position,enemy.transform.position);
            if(targetDistance<maxDistance)
            {
                closestTarget=enemy.transform;
                maxDistance=targetDistance;
            }
            target=closestTarget.transform;
        }

    }
    void AimAtTarget()
    {
        float distance=Vector3.Distance(transform.position,target.transform.position);
        weapon.LookAt(target.position);
        if(distance<=range)
        {
            attack(true);
        }else{
            attack(false);
        }
        
    }
    void attack(bool isActive)
    {
        var emissionModule=projectile.emission;
        emissionModule.enabled=isActive;
    }
    
}
