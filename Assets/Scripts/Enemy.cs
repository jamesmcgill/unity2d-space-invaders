using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int pointsPerHit = 0;
    public EnemyContainer container;

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
        container.OnEnemyDestroyed();
    }

    //--------------------------------------------------------------------------
}
