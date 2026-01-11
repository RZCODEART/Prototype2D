using UnityEngine;
using System.Collections;

public class Oxigen : MonoBehaviour
{
    public float pointLife = 1; 
    public float tiempoEspera = 1f; 
    public float maxLife = 100f;



    private Coroutine corrutinaCuracion;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            corrutinaCuracion = StartCoroutine(CurarPeriodicamente(other.gameObject));
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && corrutinaCuracion != null)
        {
            StopCoroutine(corrutinaCuracion);
        }
    }
    
    IEnumerator CurarPeriodicamente(GameObject jugador)
    {
       var scriptVida = jugador.GetComponent<PlayerHealth>();

        while (true)
        {
            if (scriptVida != null && scriptVida.vidaActual < maxLife)
            {
                scriptVida.vidaActual += pointLife;

                if (scriptVida.vidaActual > maxLife)
                    scriptVida.vidaActual = maxLife;

                Debug.Log("Vida actual: " + scriptVida.vidaActual);
            }

            yield return new WaitForSeconds(tiempoEspera);
        }
    }








}
