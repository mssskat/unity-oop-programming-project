using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
