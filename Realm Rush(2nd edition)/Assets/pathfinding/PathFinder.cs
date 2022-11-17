using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    public Vector2Int StartCoordinates{get{return startCoordinate;}}
    [SerializeField] Vector2Int destinateCoordinate;
    public Vector2Int DestinationCoordinates{get{return destinateCoordinate;}}
    Dictionary<Vector2Int,Node> reached;
    Queue<Node> frontier;
    Node startNode;
    Node destinateNode;

   
   
   Node currentSearchNode;
   Vector2Int[]directions={Vector2Int.up,Vector2Int.right,Vector2Int.down,Vector2Int.left};
   GridManager gridManager;
   Dictionary<Vector2Int,Node> grid;

   void Awake() 
   {
       gridManager=FindObjectOfType<GridManager>();
        if(gridManager!=null)
        {
            grid=gridManager.Grid;
        }
        frontier=new Queue<Node>();
        reached=new Dictionary<Vector2Int, Node>();
       
        
   }

    void Start()
    {
        startNode=grid[startCoordinate];
        destinateNode=grid[destinateCoordinate];
        GetNewPath();

       
    }

    public List<Node> GetNewPath()
    {
        
        return GetNewPath(startCoordinate);  

    }

    public List<Node> GetNewPath(Vector2Int coordinate)
    {
        gridManager.resetNodes();
        BreadthFirstSearch(coordinate);
        return buildPath();
    }

    void exploreNeighbor()
    {
        List<Node> neighbors=new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoords=currentSearchNode.coordinates+direction;
            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
                        
        }
        foreach(Node neighbor in neighbors)
        {
            if(!(reached.ContainsKey(neighbor.coordinates)) && neighbor.isWalkable)
            {
                neighbor.connectedTo=currentSearchNode;
                reached.Add(neighbor.coordinates,neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }
    void BreadthFirstSearch(Vector2Int coordinate)
    {
        startNode.isWalkable=true;
        destinateNode.isWalkable=true;
        reached.Clear();
        frontier.Clear();
        bool isRunning=true;
        frontier.Enqueue(grid[coordinate]);
        reached.Add(coordinate,grid[coordinate]);
        while(frontier.Count>0&&isRunning)
        {
            currentSearchNode=frontier.Dequeue();
            currentSearchNode.isExplored=true;
            exploreNeighbor();
            if(currentSearchNode.coordinates.Equals(destinateCoordinate))
            {
                isRunning=false;
            }
        }
    }

    List<Node> buildPath()
    {
        List<Node> path=new List<Node>();
        Node currentNode=destinateNode;
        path.Add(currentNode);
        currentNode.isPath=true;

        while(currentNode.connectedTo!=null)
        {
            currentNode=currentNode.connectedTo;
            currentNode.isPath=true;
            path.Add(currentNode);
        }
        path.Reverse();
        return path;
    }
    public bool willBlockPath(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate)){
            bool previousState=grid[coordinate].isWalkable;
            grid[coordinate].isWalkable=false;
            
            List<Node> newPath=GetNewPath();
            grid[coordinate].isWalkable=previousState;
            if(newPath.Count<=1){
                GetNewPath();
                return true;
            }
        }
        return false;
    }
    public void NotifyReceiver()
    {
        BroadcastMessage("RecalculatePath",false,SendMessageOptions.DontRequireReceiver);

    }
}
