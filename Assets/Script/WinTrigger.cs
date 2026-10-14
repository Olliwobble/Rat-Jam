using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinTrigger3D : MonoBehaviour
{
    [Header("UI Victory Elements")]
    [Tooltip("Drag a hidden UI Canvas Panel that says 'YOU WIN!' here.")]
    public GameObject victoryUIWindow;

    [Header("Scene Reversion / Progression")]
    [Tooltip("The exact name of the Main Menu or Next Level scene to load.")]
    public string nextSceneName = "MainMenu";

    [Tooltip("How long the 'You Win' text stays on screen before loading the new scene.")]
    public float transitionDelay = 3f;

    private bool hasWon = false;

    void Start()
    {
        // Make sure the win screen layout is hidden when the game starts
        if (victoryUIWindow != null)
        {
            victoryUIWindow.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent triggering the win multiple times if the player bounces in the zone
        if (hasWon) return;

        // Check if the object entering the finish zone is tagged as the Player
        if (other.CompareTag("Player"))
        {
            hasWon = true;
            StartCoroutine(VictorySequence());
        }
    }

    private IEnumerator VictorySequence()
    {
        Debug.Log("Player reached the goal! triggering Victory Screen...");

        // 1. Show the "You Win!" panel interface
        if (victoryUIWindow != null)
        {
            victoryUIWindow.SetActive(true);
        }

        // 2. Wait for the player to celebrate
        yield return new WaitForSeconds(transitionDelay);

        // 3. Load the next scene (e.g., Main Menu or next world)
        SceneManager.LoadScene(nextSceneName);
    }
}
