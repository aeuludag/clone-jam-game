using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþi için þart

public class PauseMenuManager : MonoBehaviour
{
    [Header("Gerekli Baðlantýlar")]
    [SerializeField] private GameInput gameInput;    // GameInput scriptinin olduðu objeyi sürükle
    [SerializeField] private GameObject pausePanel;  // Canvas'taki Panel objesini sürükle

    private bool isGamePaused = false;

    private void Start()
    {
        // 1. Oyun baþladýðýnda panel kapalý olsun
        Hide();

        // 2. GameInput'taki "Pause" olayýna abone ol (Dinlemeye baþla)
        if (gameInput != null)
        {
            gameInput.OnPauseToggle += GameInput_OnPauseToggle;
        }
    }

    // ESC tuþuna basýlýnca GameInput bu fonksiyonu tetikler
    private void GameInput_OnPauseToggle(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void TogglePauseGame()
    {
        isGamePaused = !isGamePaused; // Durumu tersine çevir (True ise False, False ise True yap)

        if (isGamePaused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        pausePanel.SetActive(true); // Paneli göster
        Time.timeScale = 0f;        // ZAMANI DURDUR (Oyun donar)
    }

    private void Hide()
    {
        pausePanel.SetActive(false); // Paneli gizle
        Time.timeScale = 1f;         // ZAMANI BAÞLAT (Oyun devam eder)
    }

    // --- BUTONLARIN ÇALIÞTIRACAÐI FONKSÝYONLAR ---

    public void ResumeGame()
    {
        // Devam Et butonuna basýnca toggle fonksiyonunu çaðýrýrýz
        TogglePauseGame();
    }

    public void LoadMainMenu()
    {
        // ÖNEMLÝ: Sahne deðiþtirmeden önce zamaný normale döndürmeliyiz!
        // Yoksa Ana Menü sahnesi de donmuþ (TimeScale = 0) þekilde açýlýr.
        Time.timeScale = 1f;

        // "MainMenu" yerine kendi ana menü sahnenin adýný yaz
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan Çýkýlýyor...");
        Application.Quit();
    }

    // Script sahneden silinirken (Menüye dönünce) aboneliði iptal et
    // Bunu yapmazsan "MissingReferenceException" hatasý alýrsýn.
    private void OnDestroy()
    {
        if (gameInput != null)
        {
            gameInput.OnPauseToggle -= GameInput_OnPauseToggle;
        }
    }
}