using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public delegate void movePlayer(GameObject tileSelected); //Event for generating the tile map
    public static event movePlayer move;

    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject tileObj;
    [SerializeField] private Encounter encounter;

    private void Start()
    {
        //manager = GameObject.Find("EncounterGenerator");
        //encounter = manager.GetComponent<Encounter>();
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " was clicked.");
        if(move != null) move(gameObject);
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
