using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

//Just an override
[System.Serializable]
public class GameModeEvent : UnityEvent<int>
{

}

public class GameMode : MonoBehaviour
{
    public static GameMode instance;
    public GameModeEvent scoreEvent;


    [Tooltip("The tilemap where the snake will be moving.")]
    public Tilemap groundTileMap;

    public GameObject ApplePrefab;

    GameObject foodGameObject;

    Snake snake;
    FoodScript food;
    TileMapScript tileMap;

    //Score System
    private int score = 0;
    //Timer related
    private float timer = 0f;
    private float waitTime = .5f;

    private Vector2Int foodPosition;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Assert.IsNotNull(groundTileMap, "No ground tilemap found!");
        Assert.IsNotNull(ApplePrefab, "No food prefab found!");

        GameObject tileMapObject = GameObject.Find("GroundTilemap");
        tileMap = tileMapObject.GetComponent<TileMapScript>();

        GameObject snakeObject = GameObject.Find("Snake");
        snake = snakeObject.GetComponent<Snake>();

        SpawnFood();

        GameObject foodObject = GameObject.Find("ApplePrefab(Clone)");
        food = foodObject.GetComponent<FoodScript>();




        if (scoreEvent == null)
        {
            scoreEvent = new GameModeEvent();
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!GameObject.Find("ApplePrefab(Clone)"))
        {
            SpawnFood();
        }

        if (timer > waitTime)
        {
            score++;
            timer -= waitTime;
            scoreEvent.Invoke(score);
        }
    }

    public bool SnakeEat(Vector2Int snakeGridPos)
    {
        if (snakeGridPos == foodPosition)
        {
            score += 15;
            scoreEvent.Invoke(score);
            Object.Destroy(foodGameObject);

            SpawnFood();

            return true;
        }
        else { return false; }

    }

    public void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(-15, 16), Random.Range(-7, 8));
        } while (snake.GetSnakeGridPos() == foodPosition);

        foodGameObject = Instantiate(ApplePrefab, new Vector3(foodPosition.x, foodPosition.y, 0), Quaternion.identity);
    }

    public void LoseCondition()
    {
        Debug.Log("Loading scene: LoseScene");
        SceneManager.LoadScene("LoseScene");
    }
}
