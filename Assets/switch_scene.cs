using UnityEngine;
using UnityEngine.SceneManagement;

public class moveScene : MonoBehaviour
{
    public string targetSceneName; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerPrefs.SetString("NextScene", targetSceneName);
            // Pindah ke scene loading
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
