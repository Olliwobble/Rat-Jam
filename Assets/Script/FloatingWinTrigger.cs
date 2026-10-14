using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingWinTrigger : MonoBehaviour
{
    [Header("Prefab Reference")]
    [Tooltip("Drag your WorldSpace Floating Canvas Prefab here.")]
    public GameObject floatingTextPrefab;

    [Header("Scene Progression")]
    public string nextSceneName = "MainMenu";
    public float transitionDelay = 3f;

    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;

        if (other.CompareTag("Player"))
        {
            hasWon = true;
            SpawnFloatingWinText();
            StartCoroutine(VictorySequence());
        }
    }

    void SpawnFloatingWinText()
    {
        if (floatingTextPrefab != null)
        {
            // Spawns the text slightly above the goal object's center position
            Vector3 spawnPosition = transform.position + new Vector3(0, 2f, 0);
            Instantiate(floatingTextPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator VictorySequence()
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}
