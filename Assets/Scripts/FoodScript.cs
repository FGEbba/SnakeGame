using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    Rigidbody2D body;
    PolygonCollider2D col;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
    }

}
