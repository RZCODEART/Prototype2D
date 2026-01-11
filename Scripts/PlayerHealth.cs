using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float vidaActual = 100f;
    public float dañoAsfixia = 2f;
    public Slider barraOxigeno;

    private bool estaProtegido = false;
    private float regeneracionOxigeno = 1f;
    private float maxVidaOxigeno = 100f;
    private Animator anim;

    private bool muerto = false;
    

    void Start()
    {
        
        if (barraOxigeno != null)
        {
            barraOxigeno.maxValue = maxVidaOxigeno;
            barraOxigeno.value = vidaActual;
        }


        StartCoroutine(CicloDeOxigeno());
        anim = GetComponent<Animator>(); 
        


    }

    void ActualizarInterfaz()
    {
        if (barraOxigeno != null)
        {
            barraOxigeno.value = vidaActual;
        }
    }

    public void EnZonaOxigeno(bool estado, float regeneracion = 0, float max = 100)
    {
        estaProtegido = estado;
        regeneracionOxigeno = regeneracion;
        maxVidaOxigeno = max;
        
        if (!estaProtegido && anim != null)
        {
            anim.SetBool("oxygen",false);
        }
    
    }

    IEnumerator CicloDeOxigeno()
    {
        while (!muerto)
        {
            yield return new WaitForSeconds(1f);

            bool seEstaMoviendo = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f;

            if (estaProtegido && !seEstaMoviendo)
            {
                if (vidaActual < maxVidaOxigeno)
                {
                    vidaActual += regeneracionOxigeno;
                    if (anim != null) anim.SetBool("oxygen", true);

                    if (vidaActual > maxVidaOxigeno) vidaActual = maxVidaOxigeno;
                }
                else
                {
                    if (anim !=null) anim.SetBool("oxygen", false);

                }
            }
            else
            {
                if (anim !=null) anim.SetBool("oxygen", false );

                if (!estaProtegido)
                {
                    vidaActual -= dañoAsfixia;
                    if (vidaActual <= 0 && !muerto)
                    {
                        StartCoroutine(SecuenciaMuerte());
                        
                    }
                }
               
            }
            ActualizarInterfaz();
        }
    }

    IEnumerator SecuenciaMuerte()
    {
        muerto = true;
        vidaActual = 0;
        ActualizarInterfaz();

        
        Movimientos mov = GetComponent<Movimientos>();
        if (mov != null) mov.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("die"); // 
        }

        Debug.Log("Muerto por asfixia");

        
        yield return new WaitForSeconds(3f);

        
        SceneManager.LoadScene("Lose");
    }

}