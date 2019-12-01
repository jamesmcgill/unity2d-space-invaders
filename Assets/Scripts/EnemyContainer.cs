using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    //--------------------------------------------------------------------------
    [SerializeField] GameSession gameSession;

    [Header("Enemies")]
    [SerializeField] List<Enemy> enemyTypeByRow = new List<Enemy>();
    [SerializeField] int numEnemiesPerRow = 11;
    [SerializeField] float spacingX = 0.8f;
    [SerializeField] float spacingY = 0.8f;
    [SerializeField] GameObject shotPrefab = null;
    [SerializeField] GameObject explosionPrefab = null;
    [SerializeField] float explosionLifeTime = 4.0f;
    Enemy[,] enemies;
    int numActiveEnemies;

    [Header("Shot timer")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1.5f;
    [SerializeField] float shotSpeed = 10.0f;
    float shotCounter;

    [Header("Horizontal Motion")]
    // Enemy Container moves as a whole, taking all enemies with it
    // Moves from side-to-side
    [SerializeField] float initalSpeed = 1.5f;
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float initialXDirection = 1.0f;
    [SerializeField] float startXPos = 0.0f;
    [SerializeField] float xExtent = 2.2f;
    float currentSpeed;
    float currentXDirection;

    [Header("Vertical Motion")]
    // Enemies move down in steps (after when reaching the xExtent)
    // Round finished when reaching bottom (endYPos)
    [SerializeField] float yStep = 0.5f;
    [SerializeField] float startYPos = 4.0f;
    [SerializeField] float endYPos = 0.0f;

    //--------------------------------------------------------------------------
    public GameObject GetShotPrefab() { return shotPrefab; }
    public GameObject GetExplosionPrefab() { return explosionPrefab; }
    public float GetExplosionLifeTime() { return explosionLifeTime; }

    //--------------------------------------------------------------------------
    public void OnEnemyDestroyed(int points)
    {
        gameSession.AddPoints(points);

        --numActiveEnemies;

        // Adjust speed (speed up as less enemies available)
        int numRows = enemyTypeByRow.Count;
        float maxEnemies = numRows * numEnemiesPerRow;
        float t = 1.0f - (numActiveEnemies / maxEnemies);
        currentSpeed = Mathf.Lerp(initalSpeed, maxSpeed, t);

        if (numActiveEnemies <= 0)
        {
            // TODO: Handle clearing the round
            InitRound();
        }
    }

    //--------------------------------------------------------------------------
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        // Create and position enemy ships
        int numRows = enemyTypeByRow.Count;
        enemies = new Enemy[numRows, numEnemiesPerRow];
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numEnemiesPerRow; col++)
            {
                enemies[row, col] = Instantiate(enemyTypeByRow[row],
                    new Vector2(EnemyPosX(col), EnemyPosY(row)),
                    Quaternion.identity);
                enemies[row, col].container = this;
                enemies[row, col].transform.parent = this.transform;
            }
        }

        InitRound();
    }

    //--------------------------------------------------------------------------
    void InitRound()
    {
        ResetShotCounter();

        currentSpeed = initalSpeed;
        currentXDirection = initialXDirection;

        int numRows = enemyTypeByRow.Count;
        numActiveEnemies = numRows * numEnemiesPerRow;

        transform.position = new Vector2(startXPos, startYPos);
    }

    //--------------------------------------------------------------------------
    float EnemyPosX(int column)
    {
        float halfShipCount = -(numEnemiesPerRow / 2)
            + ((numEnemiesPerRow % 2 == 0) ? 0.5f : 0.0f);
        float firstColumnPosX = halfShipCount * spacingX;
        return firstColumnPosX + (spacingX * column);
    }

    //--------------------------------------------------------------------------
    float EnemyPosY(int row)
    {
        return -row * spacingY;
    }

    //--------------------------------------------------------------------------
    void Update()
    {
        TickShotCounter();

        var deltaX = Time.deltaTime * currentSpeed * currentXDirection;
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y;
        if (Mathf.Abs(newXPos) > xExtent)
        {
            currentXDirection *= -1.0f; // Invert movement direction
            newYPos -= yStep;
            if (newYPos < endYPos) // Trigger end of round
            {
                // TODO: Implement rounds, just looping for now
                newYPos = startYPos;
            }
        }
        transform.position = new Vector2(newXPos, newYPos);
    }

    //--------------------------------------------------------------------------
    void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    //--------------------------------------------------------------------------
    void TickShotCounter()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0.0f)
        {
            Shoot();
            ResetShotCounter();
        }
    }

    //--------------------------------------------------------------------------
    void Shoot()
    {
        Enemy shooter = FindEnemyThatCanShoot();
        if (!shooter) { return; }

        GameObject shot = Instantiate(
            GetShotPrefab(),
            shooter.transform.position,
            Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shotSpeed);
    }

    //--------------------------------------------------------------------------
    // Find a random enemy who is at the the head of a column and still alive
    // That is, if any are left alive. Otherwise returns null.
    //--------------------------------------------------------------------------
    Enemy FindEnemyThatCanShoot()
    {
        // Only the lead enemy of a column shoots, therefore it's effectively
        // as if the column shoots
        int column = Random.Range(0, numEnemiesPerRow - 1);

        // Search for an available leading enemy from that column, then try
        // the adjacent columns if not found
        for (int attempts = 0; attempts < numEnemiesPerRow; ++attempts)
        {
            var enemy = FindLeadingEnemyForColumn(column);
            if (enemy)
            {
                return enemy;
            }

            ++column;
            if (column >= numEnemiesPerRow)
            {
                column = 0;
            }
        }
        return null;
    }

    //--------------------------------------------------------------------------
    // Find the leading enemy of the given column
    // That is, if any are left alive. Otherwise returns null.
    //--------------------------------------------------------------------------
    Enemy FindLeadingEnemyForColumn(int column)
    {
        int numRows = enemyTypeByRow.Count;
        for (int row = numRows - 1; row >= 0; --row)
        {
            if (enemies[row, column])
            {
                return enemies[row, column];
            }
        }
        return null;
    }

    //--------------------------------------------------------------------------
}
