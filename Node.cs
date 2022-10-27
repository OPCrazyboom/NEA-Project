using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isTraversible;
    public Vector2 worldPos;

    public Node(bool i, Vector2 w)
    {
        isTraversible = i;
        worldPos = w;
    }
}
