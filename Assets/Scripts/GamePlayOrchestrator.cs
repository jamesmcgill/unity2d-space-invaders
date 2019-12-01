using UnityEngine;

public class GamePlayOrchestrator : MonoBehaviour
{
    [SerializeField] EnemyContainer enemyContainer = null;
    [SerializeField] Boss boss = null;

    //--------------------------------------------------------------------------
    void Start()
    {
        StartEnemyWaves();
    }

    //--------------------------------------------------------------------------
    public void StartEnemyWaves()
    {
        enemyContainer.gameObject.SetActive(true);
        enemyContainer.InitRound();
        boss.gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------------
    public void StartEnemyBoss()
    {
        enemyContainer.gameObject.SetActive(false);
        boss.gameObject.SetActive(true);
    }

    //--------------------------------------------------------------------------
}
