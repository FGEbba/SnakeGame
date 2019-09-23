using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Snake : MonoBehaviour
{
    public static GameMode instance;

    Rigidbody2D body;
    BoxCollider2D boxCollider;

    private Vector2Int gridPos;
    private Vector2Int gridMoveDir;

    private float gridMoveTimer = 0f;
    private float gridMoveWaitTime = 1f;

    public float speed = 1f;
    public float maxSpeed = 10f;

    private static bool eaten;

    private int snakeBodySize = 1;
    private LinkedList<Vector2Int> snakeMovePositionList;
    private LinkedList<SnakeBodyPart> snakeBodyList;



    private void Awake()
    {
        GameObject gameObject = GameObject.Find("GameMode");
        instance = gameObject.GetComponent<GameMode>();


        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        gridPos = new Vector2Int(1, 1);
        gridMoveDir = new Vector2Int(1, 0);

        snakeMovePositionList = new LinkedList<Vector2Int>();
        snakeBodyList = new LinkedList<SnakeBodyPart>();



        gridMoveTimer = gridMoveWaitTime;



    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            instance.LoseCondition();
        }
    }

    private void HandleInput()
    {
        // Check movement up
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDir.y != -1)
            {
                gridMoveDir.x = 0;
                gridMoveDir.y = 1;
            }

        }
        // Check movement down
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDir.y != 1)
            {
                gridMoveDir.x = 0;
                gridMoveDir.y = -1;
            }

        }
        // Check movement left
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDir.x != 1)
            {
                gridMoveDir.x = -1;
                gridMoveDir.y = 0;
            }

        }
        // Check movement right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDir.x != -1)
            {
                gridMoveDir.x = 1;
                gridMoveDir.y = 0;
            }

        }
    }


    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime * speed;
        if (gridMoveTimer >= gridMoveWaitTime)
        {

            gridMoveTimer -= gridMoveWaitTime;

            snakeMovePositionList.Insert(0, gridPos);
            gridPos += gridMoveDir;


            eaten = instance.SnakeEat(GetSnakeGridPos());

            if (eaten)
            {
                if (speed < maxSpeed)
                {
                    speed += .25f;
                }
                snakeBodySize++;
                CreateSnakeBody();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }
            CheckCollision();
        }

        transform.position = new Vector3(gridPos.x, gridPos.y, 0);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDir) - 90);

        UpdateSnakeBodyParts();
    }

    private void CreateSnakeBody()
    {
        snakeBodyList.Add(new SnakeBodyPart(snakeBodyList.Count));
        Debug.Log("Snake body size: " + snakeBodySize + "\nActual snake count: " + snakeBodyList.Count);
    }

    private void CheckCollision()
    {
        foreach (SnakeBodyPart snakeBodyPart in snakeBodyList)
        {
            Vector2Int snakeBodyPartGridPos = snakeBodyPart.GetSnakeGridPos();
            if (gridPos == snakeBodyPartGridPos)
            {
                instance.LoseCondition();
            }
        }
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyList.Count; i++)
        {
            snakeBodyList[i].SetGridPosition(snakeMovePositionList[i]);
        }
    }

    /// <summary>
    /// Method for calculating the way the snake head should be facing at all times.
    /// Returns angles in degrees.
    /// </summary>
    /// <param name="dir">The last known direction.</param>
    /// <returns>The new angle.</returns>
    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetSnakeGridPos()
    {
        return gridPos;
    }

    public LinkedList<Vector2Int> GetFullSnakeGridPos()
    {
        LinkedList<Vector2Int> gridPosList = new LinkedList<Vector2Int>() { gridPos };
        gridPosList.AddRange(snakeMovePositionList);
        return gridPosList;
    }


    private class SnakeBodyPart
    {
        private Vector2Int gridPosition;
        private Transform transform;
        private Color snakeColor = new Color(61, 173, 79);
        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/brick");
            snakeBodyObject.GetComponent<SpriteRenderer>().color = Color.green;
            snakeBodyObject.AddComponent<Rigidbody2D>();
            snakeBodyObject.GetComponent<Rigidbody2D>().angularDrag = 0;
            snakeBodyObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            snakeBodyObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            snakeBodyObject.AddComponent<BoxCollider2D>();

            snakeBodyObject.tag = "Snake";

            snakeBodyObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyObject.transform;

        }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }

        public Vector2Int GetSnakeGridPos()
        {
            return gridPosition;
        }
    }
}


