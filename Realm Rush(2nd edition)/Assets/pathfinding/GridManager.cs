using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Dictionary<Vector2Int,Node> grid=new Dictionary<Vector2Int, Node>();
    [SerializeField]Vector2Int gridsize=new Vector2Int();

    [SerializeField]int gridScale=10;

    public Dictionary<Vector2Int,Node> Grid{get{return grid;}}
    
    void Awake() 
    {
        createGrid();
        
    }
    public Node GetNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }
        return null;
    }

    public void resetNodes()
    {
        foreach(KeyValuePair<Vector2Int,Node> entry in grid){
            entry.Value.connectedTo=null;
            entry.Value.isExplored=false;
            entry.Value.isPath=false;
        }
    }
    void createGrid(){
        for(int x=0;x<gridsize.x;x++){

            for(int y=0;y<gridsize.y;y++){

                Vector2Int coordinate=new Vector2Int(x,y);
                grid.Add(coordinate,new Node(coordinate,true));



            }
        }
    }
    public void blockNode(Vector2Int position)
    {
        if(grid.ContainsKey(position))
        {
            grid[position].isWalkable=false;
        }

    }
    public Vector2Int getCoordinateFromThePosition(Vector3 position)
    {
        Vector2Int coordinate=new Vector2Int();
        coordinate.x=Mathf.RoundToInt(position.x/gridScale);
        coordinate.y=Mathf.RoundToInt(position.z/gridScale);
        return coordinate;

    }
    public Vector3 getPositionFromTheCoordinate(Vector2Int coordinate)
    {
        Vector3 position=new Vector3();
        position.x=coordinate.x*gridScale;
        position.z=coordinate.y*gridScale;
        return position;
    }
}
