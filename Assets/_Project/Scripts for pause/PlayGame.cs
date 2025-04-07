using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public GameObject pausePanel; // Панель для отображения меню паузы

    public void Pause()
    {
        pausePanel.SetActive(true); // Включаем панель паузы
        Time.timeScale = 0f; // Останавливаем время
    }

    public void Resume()
    {
        pausePanel.SetActive(false); // Выключаем панель паузы
        Time.timeScale = 1f; // Возобновляем время
    }
}

