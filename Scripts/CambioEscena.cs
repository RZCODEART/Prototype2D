using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    
    public void EmpezarJuego()
    {
        
        SceneManager.LoadScene("Game");
    }
}
