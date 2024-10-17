using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public int gridSize = 7;
    public CellScript[,] grid;
    public float spacing = 1.0f;
    private bool gameRunning = true;

    void Start()
    {
        grid = new CellScript[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                CellScript cellScript = cell.GetComponent<CellScript>();
                cellScript.xIndex = x;
                cellScript.yIndex = y;
                cellScript.gameManager = this;
                grid[x, y] = cellScript;
            }
        }
    }
}