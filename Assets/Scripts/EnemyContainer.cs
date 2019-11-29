using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    //--------------------------------------------------------------------------
    // Enemies
    [SerializeField] List<GameObject> enemyTypeByRow = new List<GameObject>();
    [SerializeField] int numEnemiesPerRow = 11;
    [SerializeField] float spacingX = 0.8f;
    [SerializeField] float spacingY = 0.8f;
    private GameObject[,] enemies;

    // Horizontal Motion
    // Enemy Container moves as a whole, taking all enemies with it
    // Moves from side-to-side
    [SerializeField] float speed = 2.5f;
    [SerializeField] float initialXDirection = 1.0f;
    [SerializeField] float startXPos = 0.0f;
    [SerializeField] float xExtent = 2.2f;
    private float currentXDirection;

    // Vertical Motion
    // Enemies move down in steps (after when reaching the xExtent)
    // Round finished when reaching bottom (endYPos)
    [SerializeField] float yStep = 0.5f;
    [SerializeField] float startYPos = 4.0f;
    [SerializeField] float endYPos = 0.0f;

    //--------------------------------------------------------------------------
    void Start()
    {
        currentXDirection = initialXDirection;

        // Create and position enemy ships
        int numRows = enemyTypeByRow.Count;
        enemies = new GameObject[numRows, numEnemiesPerRow];
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numEnemiesPerRow; col++)
            {
                enemies[row, col] = Instantiate(enemyTypeByRow[row],
                    new Vector2(enemyPosX(col), enemyPosY(row)),
                    Quaternion.identity);
                enemies[row, col].transform.parent = this.transform;
            }
        }
        transform.position = new Vector2(startXPos, startYPos);
    }

    //--------------------------------------------------------------------------
    float enemyPosX(int column)
    {
        float halfShipCount = -(numEnemiesPerRow / 2)
            + ((numEnemiesPerRow % 2 == 0) ? 0.5f : 0.0f);
        float firstColumnPosX = halfShipCount * spacingX;
        return firstColumnPosX + (spacingX * column);
    }

    //--------------------------------------------------------------------------
    float enemyPosY(int row)
    {
        return -row * spacingY;
    }

    //--------------------------------------------------------------------------
    void Update()
    {
        var deltaX = Time.deltaTime * speed * currentXDirection;
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
}
