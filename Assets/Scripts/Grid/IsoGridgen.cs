using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoGridgen : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private int gridWidth = 10;
    [SerializeField] private float tileSize = 1f;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, GameObject> _tiles;

    void OnEnable() //Subscribe to Encounter manager's call to generate a map
    {
        Encounter.genTiles += GenerateGrid;
        Debug.Log("GridManager: Now subscribed to the EncounterGenerator. Awaiting orders.");
    }

    private void OnDisable() //Unsubscribe
    {
        Encounter.genTiles -= GenerateGrid;
        Debug.Log("GridManager: Now unsubscribing to the EncounterGenerator.");
    }




    void GenerateGrid()
    {
        Debug.Log("GridManager: The encounter manager wants to generate a map. Generating now.");
        //Event 
        _tiles = new Dictionary<Vector2, GameObject>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject newTile = Instantiate(tile, transform);

                float posX = (x * tileSize + y * tileSize) / 2f;    //isometric shift
                float posY = (x * tileSize - y * tileSize) / 4f;

                newTile.transform.position = new Vector2(posX, posY);
                newTile.name = x + ", " + y;

                _tiles[new Vector2(x, y)] = newTile;
                if(x == 0 && y == 0 && GameObject.Find("leftBound") == null) //leftmost tile, set bound
                {
                    GameObject leftBound = new GameObject("leftBound");
                    leftBound.transform.position = new Vector3(posX - 2, posY);
                }
                else if (x == 0 && y == gridHeight - 1 && GameObject.Find("bottomBound") == null) //bottommost tile, set bound
                {
                    GameObject bottomBound = new GameObject("bottomBound");
                    bottomBound.transform.position = new Vector3(posX, posY - 2);
                }
                else if (x == gridWidth - 1 && y == 0 && GameObject.Find("topBound") == null) //topmost tile, set bound
                {
                    GameObject topBound = new GameObject("topBound");
                    topBound.transform.position = new Vector3(posX, posY + 2);
                }
                else if (x == gridWidth - 1 && y == gridHeight - 1 && GameObject.Find("rightBound") == null) //rightmost tile, set bound
                {
                    GameObject rightBound = new GameObject("rightBound");
                    rightBound.transform.position = new Vector3(posX + 2, posY);
                }

                
            }
            
        }
        GameObject.Find("Main Camera").GetComponent<CameraDrag>().setBounds();
        GameObject.Find("EncounterGenerator").GetComponent<Encounter>().placeMap();
        //_cam.transform.position = new Vector3((float)gridWidth, (float)gridHeight, -10);


    }

    public GameObject GetTileAtPos(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        else Debug.Log("Tile at " + pos.x + ", " + pos.y + " doesn't exist!");
        return null;
    }

    public GameObject getAt(int x, int y)
    {
        Vector2 result = new Vector2();
        result.x = x;
        result.y = y;
        return GetTileAtPos(result);
    }
}
