using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int pointsPerHit = 0;
    public EnemyContainer container;

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        OnDestroyed();
        Destroy(gameObject);
        Destroy(other.gameObject);
        container.OnEnemyDestroyed();
    }

    //--------------------------------------------------------------------------
    void OnDestroyed()
    {
        GameObject explosion = Instantiate(
            container.GetExplosionPrefab(),
            transform.position,
            transform.rotation);
        Destroy(explosion, container.GetExplosionLifeTime());
    }

    //--------------------------------------------------------------------------

}
