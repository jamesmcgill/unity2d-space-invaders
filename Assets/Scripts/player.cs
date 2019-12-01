using UnityEngine;

public class player : MonoBehaviour
{
    GameSession gameSession;

    [SerializeField] float moveSpeed = 20.0f;
    [SerializeField] float padding = 1.0f;
    [SerializeField] GameObject shotPrefab = null;
    [SerializeField] float shotSpeed = 10.0f;
    [SerializeField] GameObject explosionPrefab = null;
    [SerializeField] float explosionLifeTime = 4.0f;
    [SerializeField] float respawnDelayInSeconds = 1.0f;
    float xMax;
    float xMin;

    //--------------------------------------------------------------------------
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        SetupBoundaries();
    }

    //--------------------------------------------------------------------------
    private void SetupBoundaries()
    {
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }

    //--------------------------------------------------------------------------
    void Update()
    {
        Move();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //--------------------------------------------------------------------------
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    //--------------------------------------------------------------------------
    private void Shoot()
    {
        GameObject shot = Instantiate(
            shotPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
    }

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        OnDestroyed();
    }

    //--------------------------------------------------------------------------
    void OnDestroyed()
    {
        int numLifes = gameSession.DecrementLivesLeft();
        FindObjectOfType<LivesDisplay>().SetNumLives(numLifes);

        if (numLifes == 0)
        {
            FindObjectOfType<Level>().LoadGameOver();
        }
        else
        {
            gameSession.WaitThenActivate(respawnDelayInSeconds, gameObject);
        }
        gameObject.SetActive(false);

        GameObject explosion = Instantiate(
            explosionPrefab,
            transform.position,
            transform.rotation);
        Destroy(explosion, explosionLifeTime);
    }

    //--------------------------------------------------------------------------
}
