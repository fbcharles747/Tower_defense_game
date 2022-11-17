using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class coordinateLabeler : MonoBehaviour
{
    
    TextMeshPro label;
    Vector2Int coordinate=new Vector2Int();
    [SerializeField] Color defaultColor=Color.white;
    [SerializeField] Color blockColor=Color.grey;

    [SerializeField] Color exploredColor=Color.yellow;
    [SerializeField] Color pathColor=Color.red;

    [SerializeField] int gridScale=10;

    GridManager gridManager;

    
    private void Awake() 
    {
        label=GetComponent<TextMeshPro>();
        displayCoordinate();
        gridManager=FindObjectOfType<GridManager>();
        label.enabled=false;

    }
    void Update()
    {
        if(!Application.isPlaying)
        {
            displayCoordinate();
            updateName();
            label.enabled=true;
        }
        colorCoordinate();
        toggleWayPoint();
        
    }
    void toggleWayPoint()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled=!label.enabled;
        }
    }
    void colorCoordinate()
    {
        if(gridManager.Equals(null)){ return; }

        Node node=gridManager.GetNode(coordinate);

        if(node==null){return;}

        if(!node.isWalkable)
        {
            label.color=blockColor;
        }
        else if(node.isPath)
        {
            label.color=pathColor;
        }
        else if(node.isExplored)
        {
            label.color=exploredColor;
        }
        else{
            label.color=defaultColor;
        }

    }

    void displayCoordinate()
    {
        coordinate.x=Mathf.RoundToInt(transform.parent.position.x/gridScale);
        coordinate.y=Mathf.RoundToInt(transform.parent.position.z/gridScale);
        label.text=coordinate.ToString();
        
    }
    void updateName()
    {
        transform.parent.name=coordinate.ToString();
    }
    
}
