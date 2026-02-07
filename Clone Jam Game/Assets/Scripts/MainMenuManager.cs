using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþleri için þart

public class MainMenuManager : MonoBehaviour
{
    [Header("Ayarlar Paneli")]
    // Unity Editör'den Settings Panelini buraya sürükleyeceðiz
    [SerializeField] private GameObject settingsPanel;

    // OYNA BUTONU
    public void PlayGame()
    {
        // "SampleScene" yerine oyun sahnenin adý neyse TAM OLARAK onu yaz.
        // File > Build Settings listesinde bu sahnenin olduðundan emin ol.
        SceneManager.LoadScene("SampleScene");
    }

    // AYARLAR BUTONU (Açar)
    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Paneli görünür yap
    }

    // AYARLARI KAPATMA BUTONU (Geri Dön)
    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Paneli gizle
    }

    // ÇIKIÞ BUTONU
    public void QuitGame()
    {
        Debug.Log("Oyundan Çýkýldý!"); // Editörde çalýþtýðýný görmek için
        Application.Quit();
    }
}