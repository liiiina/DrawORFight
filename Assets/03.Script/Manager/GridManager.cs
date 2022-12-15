using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] Tile _tilePrefab;
    [SerializeField] Text winner, myresult;

    Dictionary<string,int> colorDB = new Dictionary<string, int>();
    List<int> checkColor = new List<int>();

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

    void ResultGrid(Color mycolor)
    {
        
        for(int i = 1; i < _width; i++)
        {
            for(int j = 1; j < _height; j++)
            {
                var grid = GameObject.Find($"Tile {i} {j}");
                var gridrenderer = grid.GetComponent<SpriteRenderer>();
                var color = gridrenderer.color;
                if (colorDB.ContainsKey(color.ToString()))
                {
                    colorDB.Add(color.ToString(), 1);
                }
                else
                {
                    colorDB[color.ToString()]++;
                }
               
            }
        }
        


    }
}
