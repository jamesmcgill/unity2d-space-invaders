using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] GameObject livesIconPrefab = null;
    [SerializeField] float spacing = 0.5f;
    [SerializeField] float posX = -6.3f;
    [SerializeField] float posY = -4.65f;
    List<GameObject> lifeIcons;

    //--------------------------------------------------------------------------
    void Start()
    {
        lifeIcons = new List<GameObject>();

        int numLives = FindObjectOfType<GameSession>().GetInitialLives();
        for (int i = 0; i < numLives; ++i)
        {
            lifeIcons.Add(Instantiate(
                livesIconPrefab,
                new Vector3(posX + (i * spacing), posY, 0),
                Quaternion.identity));
        }
    }

    //--------------------------------------------------------------------------
    public void SetNumLives(int numLives)
    {
        int numIcons = lifeIcons.Count;
        if (numIcons > numLives)
        {
            for (int i = numLives; i < numIcons; ++i)
            {
                Destroy(lifeIcons[i]);
            }
            lifeIcons.RemoveRange(numLives, numIcons - numLives);
        }
    }

    //--------------------------------------------------------------------------
}
