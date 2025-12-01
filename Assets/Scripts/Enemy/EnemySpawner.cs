using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    private float spawnTimer = 0;
    private float spawnInterval = 1f;

    private Transform player;
    private EnemyManager manager;

    private int mobCap = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>().transform;
        manager = GameObject.FindWithTag("GameController").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager.data.timerValue < 2) // Give player a few seconds of peace
        {
            return;
        }
        if (SceneManager.GetActiveScene().name == "Temporary")
        {
            spawnTimer += Time.deltaTime;
            if ((spawnTimer >= spawnInterval) && (manager.enemyList.Count <= mobCap))
            {
                if (0 < manager.data.timerValue &&  manager.data.timerValue < 15)
                {
                    SpawnWaveOne();
                }
                if (15 < manager.data.timerValue && manager.data.timerValue < 45)
                {
                    SpawnWaveTwo();
                }
                if (manager.data.timerValue < 45 && manager.data.timerValue < 60)
                {
                    SpawnWaveThree();
                }
                spawnTimer = 0;
            }
        }
    }

    // 0-15 seconds long, spawns goblins which should be one shot by first attack
    private void SpawnWaveOne()
    {
        Enemy temp = EntityFactory.createGoblin(0.5f);
        temp.transform.position = GetRandomPosition();
        manager.enemyList.Add(temp);
    }

    private void SpawnWaveTwo()
    {
        for (int i = 0; i < 1; i++)
        {
            Enemy temp = EntityFactory.createDog(0.5f);
            temp.transform.position = GetRandomPosition();
            manager.enemyList.Add(temp);
        }
    }

    private void SpawnWaveThree()
    {
        for (int i = 0; i < 2; i++)
        {
            Enemy temp = EntityFactory.createGoblin(2);
            temp.transform.position = GetRandomPosition();
            manager.enemyList.Add(temp);
        }
    }

    private void SpawnWaveFour()
    {

    }

    private void SpawnWaveFive()
    {

    }

    private Vector2 GetRandomPosition()
    {
        //Vector2 vpr = new Vector2(Screen.width, Screen.height) * Random.Range(1.1f, 1.4f); // Define viewport, (distance could use tweaking)
        Vector2 vpr = new Vector2(24, 20) * Random.Range(1.1f, 1.4f);
        // Get values of corners
        Vector2 topLeft = new Vector2(player.position.x - vpr.x / 2, player.position.y - vpr.y / 2);
        Vector2 topRight = new Vector2(player.position.x + vpr.x / 2, player.position.y - vpr.y / 2);
        Vector2 bottomLeft = new Vector2(player.position.x - vpr.x / 2, player.position.y + vpr.y / 2);
        Vector2 bottomRight = new Vector2(player.position.x + vpr.x / 2, player.position.y + vpr.y / 2);

        string[] possibleSides = { "up", "down", "right", "left" };
        string selectedSide = possibleSides[Random.Range(0, possibleSides.Length)];

        Vector2 x = Vector2.zero;
        Vector2 y = Vector2.zero;

        switch (selectedSide)
        {
            case "up":
                x = topLeft;
                y = topRight;
                break;
            case "down":
                x = bottomLeft;
                y = bottomRight;
                break;
            case "right":
                x = topRight;
                y = bottomRight;
                break;
            case "left":
                x = topLeft;
                y = bottomLeft;
                break;
            default:
                break;
        }

        return new Vector2(Random.Range(x.x, y.x), Random.Range(x.y, y.y));
    }
}