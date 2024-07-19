using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Propiedad estática para acceder a la instancia del GameManager
    public static GameManager Instance { get; private set; }

    public Stats stats = new Stats();

    // Método Awake para inicializar el singleton
    private void Awake()
    {
        // Si ya existe una instancia y no es esta, destruir este objeto
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Asignar esta instancia y no destruir al cambiar de escena
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }




    // Método de Game Over
    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Implementar lógica de Game Over aquí
    }

    // Método para reiniciar el juego
    public void RestartGame()
    {
        // Implementar lógica para reiniciar el juego aquí
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
