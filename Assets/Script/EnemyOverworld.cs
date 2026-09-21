using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyOverworld : MonoBehaviour
{
    public string combatSceneName = "BattleScene";

    [Header("Enemy Encounter Configurations")]
    public string enemyName = "Dire Rat";
    public int enemyMaxHP = 75;
    public int enemyAttack = 14;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BattleDataManager.Instance.SaveEnemyData(enemyName, enemyMaxHP, enemyAttack);
            SceneManager.LoadScene(combatSceneName);
        }
    }
}
