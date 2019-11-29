using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int pointsPerHit = 0;
    public EnemyContainer container;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
        container.OnEnemyDestroyed();
    }
}
