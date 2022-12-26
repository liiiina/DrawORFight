using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] Tile _tilePrefab;
    [SerializeField] Text winner, myresult;

    void GenerateGrid()
    {
        for(int i = 1; i < _width; i++)
        {
            for(int j = 1; j < _height; j++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";
                spawnedTile.Init();
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

}
