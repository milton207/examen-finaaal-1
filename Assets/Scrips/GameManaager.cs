using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI y Puntuación")]
    public TextMeshProUGUI coinText;
    public int totalCoins = 6; // Configuras la cantidad total manualmente aquí o en el Inspector
    private int collectedCoins = 0;

    [Header("Paneles de Estado")]
    public GameObject winPanel;
    public GameObject gameOverPanel;

    private bool isGameEnded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Si quieres contar automático descomenta la siguiente línea, 
        // de lo contrario usará el número que pongas en el Inspector (6):
        // totalCoins = GameObject.FindGameObjectsWithTag("Moneda").Length;

        UpdateCoinUI();

        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void AddCoin()
    {
        if (isGameEnded) return;

        collectedCoins++;
        UpdateCoinUI();

        // Solo gana cuando alcance el total real
        if (collectedCoins >= totalCoins)
        {
            WinGame();
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Monedas: " + collectedCoins + " / " + totalCoins;
        }
    }

    void WinGame()
    {
        isGameEnded = true;
        Debug.Log("¡GANASTE! Recolectaste todas las monedas.");
        if (winPanel) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        if (isGameEnded) return;
        isGameEnded = true;
        Debug.Log("¡GAME OVER! El enemigo te atrapó.");
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}