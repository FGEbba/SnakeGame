using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapScript : MonoBehaviour
{
    private static GameMode instance;
    TilemapCollider2D tCollider;
    Rigidbody2D body;


    void Start()
    {
        GameObject gameObject = GameObject.Find("GameMode");
        instance = gameObject.GetComponent<GameMode>();

        body = GetComponent<Rigidbody2D>();
        tCollider = GetComponent<TilemapCollider2D>();
    }

}
