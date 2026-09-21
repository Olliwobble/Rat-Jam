using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    [Header("Fighter Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;


    [Header("Spawn Stations")]
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    [Header("UI Health Component Links")]
    public Slider playerHPSlider;
    public Slider enemyHPSlider;

    [Header("Inventory Interface Link")]
    public Text potionCountText;

    [Header("Scene Reversion")]
    public string overworldSceneName = "OverworldScene";

    private FighterStats playerStats;
    private FighterStats enemyStats;
    private int localPotionCount;
    public BattleState state;

    void Start()
    {
        state = BattleState.START;

        // Fetch remaining saved items from the global manager data profile
        if (BattleDataManager.Instance != null)
        {
            localPotionCount = BattleDataManager.Instance.globalPotionCount;
        }

        UpdateInventoryUI();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation.position, Quaternion.identity);
        playerStats = playerGO.GetComponent<FighterStats>();
        playerStats.hpSlider = playerHPSlider;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation.position, Quaternion.identity);
        enemyStats = enemyGO.GetComponent<FighterStats>();
        enemyStats.hpSlider = enemyHPSlider;

        if (BattleDataManager.Instance != null)
        {
            enemyStats.fighterName = BattleDataManager.Instance.StoredName;
            enemyStats.maxHP = BattleDataManager.Instance.StoredMaxHP;
            enemyStats.currentHP = BattleDataManager.Instance.StoredMaxHP;
            enemyStats.attackPower = BattleDataManager.Instance.StoredAttack;
        }

        playerStats.UpdateHPBar();
        enemyStats.UpdateHPBar();

        Debug.Log($"A wild {enemyStats.fighterName} appeared!");
        yield return new WaitForSeconds(1.5f);

        PlayerTurn();
    }

    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        playerStats.isDefending = false;
        playerStats.isDodging = false;
    }

    // --- UI CANVAS ACCESSIBLE BUTTON METHOD EXPORTS ---

    public void OnAttackButton() { if (state == BattleState.PLAYERTURN) StartCoroutine(PlayerAttack()); }
    public void OnDefendButton() { if (state == BattleState.PLAYERTURN) StartCoroutine(PlayerDefend()); }
    public void OnDodgeButton() { if (state == BattleState.PLAYERTURN) StartCoroutine(PlayerDodge()); }
    public void OnUsePotionButton()
    {
        if (state == BattleState.PLAYERTURN && localPotionCount > 0) StartCoroutine(PlayerUsePotion());
    }

    // --- LOGIC ROUTINES ---

    IEnumerator PlayerAttack()
    {
        if (Random.value < 0.15f) // 15% chance to miss
        {
            playerStats.SpawnFloatingText("MISS!", Color.gray);
        }
        else
        {
            bool isDead = enemyStats.TakeDamage(playerStats.attackPower);
            yield return new WaitForSeconds(0.6f);

            if (isDead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
                yield break;
            }
        }

        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerDefend()
    {
        playerStats.isDefending = true;
        playerStats.SpawnFloatingText("DEFEND!", Color.yellow);
        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerDodge()
    {
        playerStats.isDodging = true;
        playerStats.SpawnFloatingText("PREPARE DODGE!", Color.cyan);
        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerUsePotion()
    {
        localPotionCount--;
        UpdateInventoryUI();
        playerStats.Heal(35);

        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        enemyStats.isDefending = false;
        enemyStats.isDodging = false;

        if (Random.value > 0.25f) // Enemy actions
        {
            if (Random.value < 0.15f)
            {
                enemyStats.SpawnFloatingText("MISS!", Color.gray);
            }
            else
            {
                bool isDead = playerStats.TakeDamage(enemyStats.attackPower);
                yield return new WaitForSeconds(0.6f);
                if (isDead)
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());
                    yield break;
                }
            }
        }
        else
        {
            enemyStats.isDefending = true;
            enemyStats.SpawnFloatingText("DEFEND!", Color.yellow);
        }

        yield return new WaitForSeconds(1f);
        PlayerTurn();
    }

    void UpdateInventoryUI()
    {
        if (potionCountText != null)
            potionCountText.text = $"Potion ({localPotionCount})";
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            // Update the persistent manager before returning so used items stay gone
            if (BattleDataManager.Instance != null)
            {
                BattleDataManager.Instance.globalPotionCount = localPotionCount;
            }
            Debug.Log("You Win!");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(overworldSceneName);
        }
        else
        {
            Debug.Log("Game Over...");
        }
    }
}
