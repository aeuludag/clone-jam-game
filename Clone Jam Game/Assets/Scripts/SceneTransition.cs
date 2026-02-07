using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Gidilecek Sahne Adý")]
    [SerializeField] private string sceneName; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Sahne Deðiþiyor...");
            SceneManager.LoadScene(sceneName);
        }
    }
}