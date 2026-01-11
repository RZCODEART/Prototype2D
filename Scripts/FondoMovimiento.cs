using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D guerrero;
    private float ultimaPosicionX;


    private void Start()
    {
        if (guerrero != null) ultimaPosicionX = guerrero.transform.position.x;
    }


    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            guerrero = jugador.GetComponent<Rigidbody2D>();
        }
    }
    private void Update()
    {
        /* if (guerrero != null)
         {

             float vX = guerrero.linearVelocityX;
             Vector2 offset = new Vector2(vX, 0) * velocidadMovimiento * Time.deltaTime;
             material.mainTextureOffset += offset;

         }*/
        if (guerrero != null)
        {
            // Calculamos cuánto se movió el jugador desde el último frame
            float desplazamientoJugador = guerrero.transform.position.x - ultimaPosicionX;

            // Aplicamos ese desplazamiento al offset del material
            Vector2 offset = new Vector2(desplazamientoJugador, 0) * velocidadMovimiento;
            material.mainTextureOffset += offset;

            // Guardamos la posición actual para el siguiente frame
            ultimaPosicionX = guerrero.transform.position.x;
        }



    }



}
