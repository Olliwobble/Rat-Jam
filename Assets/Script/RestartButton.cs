using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerButton : MonoBehaviour
{

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);



    }
}
