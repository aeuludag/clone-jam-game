using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Referanslar�")]
    [SerializeField] private GameObject mainMenuPanel; // ANA MEN�Y� TUTAN OBJE (YEN�)
    [SerializeField] private GameObject settingsPanel; // AYARLAR PANEL�

    // --- SES VE EKRAN AYARLARI ---
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // --- PANEL GE���LER� (Buras� de�i�ti) ---
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);  // Ayarlar� a�
        mainMenuPanel.SetActive(false); // Ana men�y� kapat (Gizle)
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Ayarlar� kapat
        mainMenuPanel.SetActive(true);  // Ana men�y� geri a� (G�ster)
    }

    // --- OYUN KONTROLLER� ---
    public void PlayGame()
    {
        SceneManager.LoadScene("Home");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("��k�� Yap�ld�");
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