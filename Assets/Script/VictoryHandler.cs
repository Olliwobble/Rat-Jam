using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryHandler : MonoBehaviour
{
    [Header("UI Victory Elements")]
    [Tooltip("Drag a hidden UI Panel or Text mesh object that says 'YOU WIN!' here.")]
    public GameObject victoryUIWindow;

    [Tooltip("Optional text element to display items or gold won.")]
    public Text rewardsText;

    [Header("Scene Reversion")]
    [Tooltip("The exact name of your Overworld Map scene.")]
    public string overworldSceneName = "OverworldScene";

    [Tooltip("How long the player stays on the win screen before loading the map.")]
    public float transitionDelay = 2.5f;

    void Start()
    {
        // Ensure the victory screen is hidden when the battle begins
        if (victoryUIWindow != null)
        {
            victoryUIWindow.SetActive(false);
        }
    }

    // This public method is called by the BattleSystem when the enemy dies
    public void TriggerVictory(int remainingPotions)
    {
        StartCoroutine(VictorySequence(remainingPotions));
    }

    private IEnumerator VictorySequence(int remainingPotions)
    {
        Debug.Log("Executing Victory Sequence...");

        // 1. Save data permanently to the Data Manager so your inventory choice persists

        }

        // 2. Show the "You Win!" layout screen
        if (victoryUIWindow != null)
        {
            victoryUIWindow.SetActive(true);
        }

        if (rewardsText != null)
        {
            rewardsText.text = "Battle Won!\nSaved Potion Inventory Profile.";
        }

        // 3. Pause for a few seconds so the player can celebrate
        yield return new WaitForSeconds(transitionDelay);

        // 4. Return to the exploring map
        SceneManager.LoadScene(overworldSceneName);
    }
}
