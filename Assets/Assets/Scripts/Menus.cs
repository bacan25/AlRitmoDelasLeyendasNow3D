using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject inicio;
    public GameObject tuto1;
    public GameObject tuto2;
    public bool inTutorial = false; // Variable para rastrear si estás en un tutorial

    void Update()
    {
        // Verifica si el usuario ha presionado la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }

    public void ButtonStart()
    {
        SceneLoader.Instance.LoadScene("Level_1_Android"); // Cambia a la escena del nivel 1
    }

    public void ButtonRestart()
    {
        // Obtiene el nombre de la escena activa y recarga esa escena
        SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }

    public void ButtonMenu()
    {
        SceneLoader.Instance.LoadScene("Inicio"); // Vuelve al menú principal
    }

    public void NextScene()
    {
        // Si estás en un tutorial, carga directamente la escena final
        if (inTutorial)
        {
            SceneLoader.Instance.LoadScene("Final");
            inTutorial = false; // No olvides actualizar el estado del tutorial
        }
        else
        {
            // Si no estás en un tutorial, procede como normalmente lo harías
            SceneLoader.Instance.GoToNextLevel();
        }
    }

    public void Tutorial1()
    {
        tuto1.SetActive(true);
        inicio.SetActive(false);
        inTutorial = true; // Actualiza el estado cuando entras en el tutorial
    }

    public void Tutorial2()
    {
        tuto2.SetActive(true);
        tuto1.SetActive(false);
        inTutorial = true; // Asegúrate de mantener actualizado el estado de inTutorial
    }
}
