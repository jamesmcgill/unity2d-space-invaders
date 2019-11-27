using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    // Enemies
    private List<GameObject> enemies;
    private int numEnemiesPerRow = 11;
    private int numRows = 5;

    // Horizontal Motion
    // Enemy Container moves as a whole, taking all enemies with it
    // Moves from side-to-side
    [SerializeField] float speed = 1.1f;
    [SerializeField] float initialXDirection = 1.0f;
    [SerializeField] float startXPos = 0.0f;
    [SerializeField] float xExtent = 5.0f;
    private float currentXDirection;

    // Vertical Motion
    // Enemies move down in steps (after when reaching the xExtent)
    // Round finished when reaching bottom (endYPos)
    [SerializeField] float yStep = 0.5f;
    [SerializeField] float startYPos = 1.0f;
    [SerializeField] float endYPos = -3.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(startXPos, startYPos);
        currentXDirection = initialXDirection;

        // TODO: Create list of enemies
    }

    // Update is called once per frame
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
}
