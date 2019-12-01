using UnityEngine;

public class BossKernel : MonoBehaviour
{
    [Header("Horizontal Motion")]
    [SerializeField] float speed = 5.0f;
    [SerializeField] float initialXDirection = 1.0f;
    [SerializeField] float startXPos = 0.0f;
    [SerializeField] float xExtent = 4.0f;
    float currentXDirection;

    [Header("Shot timer")]
    [SerializeField] GameObject shotPrefab = null;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1.5f;
    [SerializeField] float shotSpeed = 10.0f;
    float shotCounter;

    //--------------------------------------------------------------------------
    void Start()
    {
        currentXDirection = initialXDirection;
        transform.position = new Vector2(startXPos, transform.position.y);
    }

    //--------------------------------------------------------------------------
    void Update()
    {
        TickShotCounter();

        var deltaX = Time.deltaTime * speed * currentXDirection;
        var newXPos = transform.position.x + deltaX;
        if (Mathf.Abs(newXPos) >= xExtent)
        {
            newXPos = transform.position.x; // Don't move out of extent
            currentXDirection *= -1.0f; // Invert movement direction
        }
        transform.position = new Vector2(newXPos, transform.position.y);
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
        GameObject shot = Instantiate(
            shotPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shotSpeed);
    }

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        // Enemies only get destroyed by shots
        if (other.tag == "shot")
        {
            FindObjectOfType<GamePlayOrchestrator>()?.StartEnemyWaves();
        }
        Destroy(other.gameObject);
    }
    //--------------------------------------------------------------------------
}
