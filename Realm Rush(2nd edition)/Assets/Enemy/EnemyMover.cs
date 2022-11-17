using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField]List<Node> path=new List<Node>();
    [SerializeField][Range(0,5f)] float travelSpeed=0.4f;
    Enemy enemy;

    GridManager gridManager;

    PathFinder pathFinder;

    private void Awake()
    {
        gridManager=FindObjectOfType<GridManager>();
        pathFinder=FindObjectOfType<PathFinder>();

        
    }

     
    // Start is called before the first frame update
    void OnEnable()
    {
        
       
        returnToStart();
        RecalculatePath(true);
       
        
    }
    void Start()
    {
        enemy=GetComponent<Enemy>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinate=new Vector2Int();

        
        if(resetPath){
            coordinate=pathFinder.StartCoordinates;
        }else{
            coordinate=gridManager.getCoordinateFromThePosition(transform.position);
        }
        path.Clear();
        StopAllCoroutines();
        path=pathFinder.GetNewPath(coordinate);
        StartCoroutine(followPath());
        
    }
    void returnToStart()
    {
        transform.position=gridManager.getPositionFromTheCoordinate(pathFinder.StartCoordinates);
    }

    IEnumerator followPath()
    {
        Vector3 startPosition;
        Vector3 endPosition;
        float travelPercentage;
        for (int i=1;i<path.Count;i++)
        {
            startPosition=transform.position;
            endPosition=gridManager.getPositionFromTheCoordinate(path[i].coordinates);
            travelPercentage=0f;
            transform.LookAt(endPosition);//facing the end point

            //walking from point to point
            while(travelPercentage<1f)
            {
                travelPercentage+=Time.deltaTime*travelSpeed;
                transform.position=Vector3.Lerp(startPosition,endPosition,travelPercentage);
                yield return new WaitForEndOfFrame();
            }
            
            
        }
        enemy.stealGold();
        gameObject.SetActive(false);
        
        
    }

    
}
