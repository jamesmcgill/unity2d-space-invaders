using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Entrance Animation")]
    [SerializeField] float startYPos = 4.0f;
    [SerializeField] float endYPos = 0.0f;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float deathTimeInSeconds = 0.5f;

    //--------------------------------------------------------------------------
    public float GetDeathTimeInSeconds() { return deathTimeInSeconds; }

    //--------------------------------------------------------------------------
    void Start()
    {
        transform.position = new Vector2(transform.position.x, startYPos);
    }

    //--------------------------------------------------------------------------
    void Update()
    {
        if (transform.position.y != endYPos)
        {
            var deltaY = Time.deltaTime * speed;
            var newYPos = transform.position.y - deltaY;
            if (newYPos < endYPos)
            {
                newYPos = endYPos;
            }
            transform.position = new Vector2(transform.position.x, newYPos);
        }
    }

    //--------------------------------------------------------------------------
}
