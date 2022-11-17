using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;

    Vector2Int coordinate;

    GridManager gridManager;

    PathFinder pathFinder;

    public bool IsPlaceable{get{return isPlaceable;}}//IsPlaceable as a property

    void Awake() 
    {
        gridManager=FindObjectOfType<GridManager>();
        pathFinder=FindObjectOfType<PathFinder>();

        
    }
    void Start() 
    {
        if(gridManager!=null)
        {
            coordinate=gridManager.getCoordinateFromThePosition(transform.position);
            if(!isPlaceable)
            {
                gridManager.blockNode(coordinate);
            }
        }
        
    }


    // Start is called before the first frame update
    private void OnMouseDown() 
    {
        if(gridManager.GetNode(coordinate).isWalkable && !pathFinder.willBlockPath(coordinate))
        {
            bool isSuccessful = towerPrefab.createTower(towerPrefab,transform.position);
            
            isPlaceable=!isSuccessful;
            if(isSuccessful){
                gridManager.blockNode(coordinate);
                pathFinder.NotifyReceiver();
            }
            
            
        }
        
        
    }
}
