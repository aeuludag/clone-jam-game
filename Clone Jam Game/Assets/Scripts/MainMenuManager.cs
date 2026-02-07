using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider ve Toggle kullanmak için bu KÜTÜPHANE ÞART!

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Referanslarý")]
    [SerializeField] private GameObject settingsPanel;

    // Unity ses sistemini en basit haliyle kontrol etmek için:
    public void SetVolume(float volume)
    {
        // AudioListener, oyundaki tüm seslerin duyulduðu "kulaktýr".
        // 0 (Sessiz) ile 1 (Tam Ses) arasýnda deðer alýr.
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Ekraný tam ekran yapar veya pencere moduna alýr.
        Screen.fullScreen = isFullscreen;
    }

    // --- DÝÐER FONKSÝYONLARIN ---
    public void PlayGame()
    {
        SceneManager.LoadScene("Room1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Çýkýþ Yapýldý");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}