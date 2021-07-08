using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    // Instancia de la propia clase para poder editar el campo barrelCounter desde otro script.
    public static LevelManager instance;

    // Características del nivel.
    private string levelName = "SciFi_Industrial";
    private float levelTimer = 60f;
    public byte barrelCounter;
    private byte totalBarrels = 6;
    private bool gameEnded = false;


    /**
     * Start is called before the first frame update.
     */
    void Start() {
        instance = this;
        barrelCounter = 0;
        Debug.Log("¡Destruye " + totalBarrels + " barriles antes de que se agote el tiempo!");
    }

    /**
     * Update is called once per frame.
     */
    void Update() {
        // Mientras el timer sea mayor que 0 se va decrementando.
        // En el momento que valga 0 o menor, es Game Over.
        // Si se han explotado todos los barriles, el jugador gana y se acaba el juego.
        if (!gameEnded) {
            if (barrelCounter == totalBarrels) {
                Debug.Log("***YOU WIN***");
                gameEnded = true;

            } else {

                if (levelTimer > 0) {
                    levelTimer -= Time.deltaTime;
                    Debug.Log("Tiempo restante: " + (int)levelTimer);
                } else {
                    Debug.Log("***GAME OVER***");
                    SceneManager.LoadScene(levelName);
                }
            }
        }
    }

}
