using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public delegate void createGrid(); //Event for generating the tile map
    public static event createGrid genTiles;

    [SerializeField] private IsoGridgen mapManager;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Map;

    // Start is called before the first frame update
    void Start()
    {
        genTiles(); //Evoke event
    }

    void OnEnable() //Subscribe to Tile's call to move a player
    {
        Tile.move += placePlayer;
        Debug.Log("EncounterGenerator: Subscribed to Tiles, ready to select them.");
    }

    private void OnDisable() //Unsubscribe
    {
        Tile.move -= placePlayer;
        Debug.Log("EncounterGenerator: Unsubscribed to the Tiles.");
    }

    public void placePlayer(int x, int y)
    {
        GameObject newPlayer = Instantiate(Player, transform);
        newPlayer.name = "Player";
        newPlayer.transform.position = mapManager.getAt(x,y).transform.position;
    }

    void placePlayer(GameObject g)
    {
        Debug.Log("EncounterGenerator: The tile wants to move the player. Moving now.");
        if (GameObject.Find("Player") == null)
        {
            GameObject newPlayer = Instantiate(Player, transform);
            newPlayer.name = "Player";
            newPlayer.transform.position = g.transform.position;
            Debug.Log("Player was placed.");
        }
        else
        {
            GameObject.Find("Player").transform.position = g.transform.position;
            Debug.Log("Player was moved.");
        }
    }
    public void placeMap()
    {
        //GameObject map = Instantiate(Map, transform);
        //map.transform.position = Vector3.Lerp(GameObject.Find("leftBound").transform.position, GameObject.Find("rightBound").transform.position, 0.5f);
    }

}
