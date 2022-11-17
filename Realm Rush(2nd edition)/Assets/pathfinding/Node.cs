using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
   public Vector2Int coordinates=new Vector2Int();
   public bool isWalkable;
   public bool isPath;
   public bool isExplored;

   public Node connectedTo;
   public Node (Vector2Int coordinate,bool isWalkable){
       this.coordinates=coordinate;
       this.isWalkable=isWalkable;
   }
}
