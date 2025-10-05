using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{   
    public GameObject menuPrincipal; 

    public void QuitGame()
    {
        // Cierra la aplicación (no funciona en editor, solo en build)
        Application.Quit();
    }
    public void PlayGame()
    {

        SceneManager.LoadScene("SampleScene");
    }
}
