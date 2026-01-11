using UnityEngine;
using System.Collections;

public class ZonaOxigeno : MonoBehaviour
{
    public float pointLife = 5f;
    public float maxLife = 100f;

    public AudioClip sonidoAmbienteZona;
    private AudioSource audioSource;
    [Range(0.1f, 2f)] public float tiempoFade = 0.5f;

    
    private float volumenMaximo = 0.5f; 
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource= gameObject.AddComponent<AudioSource>();    
        }

        audioSource.clip = sonidoAmbienteZona;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0;

    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.EnZonaOxigeno(true, pointLife, maxLife);

                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

                fadeCoroutine = StartCoroutine(FadeAudio(volumenMaximo));
               
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.EnZonaOxigeno(false);

                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                fadeCoroutine = StartCoroutine(FadeAudio(0));
                
            }
        }
    }
    IEnumerator FadeAudio(float targetVolume)
    {
        if (targetVolume > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }



        float startVolume = audioSource.volume;
        float time = 0;

        while (time < tiempoFade)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / tiempoFade);
            yield return null;
        }

        audioSource.volume = targetVolume;
        if (targetVolume <= 0)
        {
            audioSource.Stop();
        }
    }
}
