using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOfNodes : MonoBehaviour
{
    public Vector2 gridSize;
    public float nodeDiameter;
    Node[,] Grid;
    // Start is called before the first frame update
    void Start()
    {
        int x = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        int y = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid(x, y);
    }

    void CreateGrid(int x1, int y1)
    {
        Grid = new Node[x1, y1];
        for (int x = 0; x < x1; x++)
        {
            for (int y = 0; y < y1; y++)
            {

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
