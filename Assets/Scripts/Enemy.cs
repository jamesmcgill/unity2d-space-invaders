using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int points = 0;
    public EnemyContainer container;

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        OnDestroyed();
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
