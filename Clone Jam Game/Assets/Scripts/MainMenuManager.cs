using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Referanslarý")]
    [SerializeField] private GameObject mainMenuPanel; // ANA MENÜYÜ TUTAN OBJE (YENÝ)
    [SerializeField] private GameObject settingsPanel; // AYARLAR PANELÝ

    // --- SES VE EKRAN AYARLARI ---
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // --- PANEL GEÇÝÞLERÝ (Burasý deðiþti) ---
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);  // Ayarlarý aç
        mainMenuPanel.SetActive(false); // Ana menüyü kapat (Gizle)
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Ayarlarý kapat
        mainMenuPanel.SetActive(true);  // Ana menüyü geri aç (Göster)
    }

    // --- OYUN KONTROLLERÝ ---
    public void PlayGame()
    {
        SceneManager.LoadScene("Home");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Çýkýþ Yapýldý");
    }
}