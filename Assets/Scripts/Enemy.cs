using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int points = 0;
    public EnemyContainer container;

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        // Enemies only get destroyed by shots
        if (other.tag == "shot")
        {
            OnDestroyed();
        }
        Destroy(other.gameObject);
    }

    //--------------------------------------------------------------------------
    void OnDestroyed()
    {
        GameObject explosion = Instantiate(
            container.GetExplosionPrefab(),
            transform.position,
            transform.rotation);
        Destroy(explosion, container.GetExplosionLifeTime());

        Destroy(gameObject);
        container.OnEnemyDestroyed(points);
    }

    //--------------------------------------------------------------------------

}
