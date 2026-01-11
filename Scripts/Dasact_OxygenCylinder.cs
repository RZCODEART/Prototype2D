using UnityEngine;

public class Dasact_OxygenCylinder : MonoBehaviour
{
    public float cantidadOxigeno = 20f;

    public AudioClip sonidoOxygeno;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                if (sonidoOxygeno != null)
                {
                    AudioSource.PlayClipAtPoint(sonidoOxygeno, transform.position); 
                }

                playerHealth.vidaActual += cantidadOxigeno;

                if (playerHealth.vidaActual > 100f)
                {
                    playerHealth.vidaActual = 100f;
                }
                                
                playerHealth.Invoke("ActualizarInterfaz", 0f);

                       
                gameObject.SetActive(false);
            }
        }
    }
}   
