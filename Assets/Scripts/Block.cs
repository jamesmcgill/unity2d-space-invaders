using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Sprite[] damageSprites = null;
    [SerializeField] int timesHit = 0;

    //--------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "shot")
        {
            Destroy(other.gameObject);

            int maxHits = damageSprites.Length + 1;
            if (++timesHit >= maxHits)
            {
                Destroy(gameObject);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = damageSprites[timesHit - 1];
            }
        }
    }

    //--------------------------------------------------------------------------
}
