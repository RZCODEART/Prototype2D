using UnityEngine;
using UnityEngine.UI; // Opcional: Solo si usas una barra de vida visual

public class PlayerHealth1 : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public float vidaActual = 80f;
    public float vidaMaxima = 100f;

    

    void Update()
    {
        
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            RecibirDanio(10); 
        }
       */
        ActualizarInterfaz();
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;

        
        if (vidaActual < 0) vidaActual = 0;

        Debug.Log("Vida restante: " + vidaActual);
    }

    
    private void ActualizarInterfaz()
    {
        // Si tienes una barra de vida, aquí actualizarías su valor
        // barraVida.value = vidaActual / vidaMaxima;
    }
}