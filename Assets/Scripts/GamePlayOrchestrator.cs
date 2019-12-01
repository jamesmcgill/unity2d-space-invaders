using System.Collections;
using UnityEngine;

public class GamePlayOrchestrator : MonoBehaviour
{
    [SerializeField] EnemyContainer enemyContainer = null;
    [SerializeField] Boss bossPrefab = null;
    [SerializeField] float delayToRestartS = 0.5f;
    Boss boss = null;

    //--------------------------------------------------------------------------
    void Start()
    {
        FindObjectOfType<GameSession>().ResetGame();
        StartEnemyWaves();
    }

    //--------------------------------------------------------------------------
    public void StartEnemyWaves()
    {
        enemyContainer.gameObject.SetActive(true);
        enemyContainer.InitRound();
    }

    //--------------------------------------------------------------------------
    public void OnWaveDefeated()
    {
        enemyContainer.gameObject.SetActive(false);
        StartEnemyBoss();
    }

    //--------------------------------------------------------------------------
    public void StartEnemyBoss()
    {
        boss = Instantiate(bossPrefab);
    }

    //--------------------------------------------------------------------------
    public void OnBossDefeated()
    {
        float deathDelay = boss.GetDeathTimeInSeconds();
        foreach (Transform child in boss.transform)
        {
            GameObject.Destroy(child.gameObject, deathDelay);
        }
        Destroy(boss, deathDelay);

        StartCoroutine(WaitThenRestartWaves(deathDelay + delayToRestartS));
    }

    //--------------------------------------------------------------------------
    IEnumerator WaitThenRestartWaves(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartEnemyWaves();
    }

    //--------------------------------------------------------------------------
}
