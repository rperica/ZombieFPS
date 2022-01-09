using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node previousNode;
    public Vector3 worldPosition;
    public bool isWalkable;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public int fCost;

    public void CalculateFCost()
    {
        fCost = hCost + gCost;
    }

    public Node(Vector3 _worldPosition, bool _isWalkable, int _gridX, int _gridY)
    {
        worldPosition = _worldPosition;
        isWalkable = _isWalkable;

        gridX = _gridX;
        gridY = _gridY;
    }
}
