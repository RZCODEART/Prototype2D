using UnityEngine;
using UnityEngine.Audio;

public class SonidoAmbiente : MonoBehaviour
{
    private AudioSource audioSource;
    public PlayerHealth player;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.vidaActual < 20f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.1f, Time.deltaTime);
        }
        else
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.4f, Time.deltaTime);
        }
    }
}
