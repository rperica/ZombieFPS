using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    public Vector3[] PathFinding(Vector3 start, Vector3 destination)
    {
        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>();

        Node startNode = grid.GetNodeFromWorldPoint(start);
        Node destinationNode = grid.GetNodeFromWorldPoint(destination);

        openNodes.Add(startNode);

        while (openNodes.Count > 0)
        {
            Node currentNode = openNodes[0];

            foreach (Node node in openNodes)
            {
                if (currentNode.fCost < node.fCost)
                {
                    continue;
                }
                else if (currentNode.fCost == node.fCost)
                {
                    if (currentNode.gCost > node.gCost)
                    {
                        currentNode = node;
                    }
                }
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == destinationNode)
            {
                Vector3[] path = RetracePath(startNode, destinationNode);

                return path;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.isWalkable || closedNodes.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + CalculateDistance(currentNode, neighbour);

                if (newCostToNeighbour < neighbour.gCost || !openNodes.Contains(neighbour))
                {
                    neighbour.previousNode = currentNode;
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = CalculateDistance(neighbour, destinationNode);
                    neighbour.CalculateFCost();

                    if (!openNodes.Contains(neighbour))
                    {
                        openNodes.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node current = endNode;
        while (current != startNode)
        {
            path.Add(current);
            current = current.previousNode;
        }

        path.Reverse();

        return PathToWayPoints(path);
    }

    private Vector3[] PathToWayPoints(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
            waypoints.Add(path[i].worldPosition);

        return waypoints.ToArray();
    }

    private int CalculateDistance(Node firstNode, Node secondNode)
    {
        int x = Mathf.Abs(secondNode.gridX - firstNode.gridX);
        int y = Mathf.Abs(secondNode.gridY - firstNode.gridY);

        int distanceX = Mathf.Max(x, y);
        int distanceY = Mathf.Min(x, y);

        int distance = 14 * distanceY + 10 * (distanceX - distanceY);

        return distance;
    }
}
