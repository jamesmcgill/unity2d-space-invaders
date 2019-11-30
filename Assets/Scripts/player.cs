using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20.0f;
    [SerializeField] float padding = 1.0f;
    [SerializeField] GameObject shotPrefab;
    [SerializeField] float shotSpeed = 10.0f;
    float xMax;
    float xMin;

    //--------------------------------------------------------------------------
    void Start()
    {
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
}
