using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEditor.ShaderGraph;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Movimientos : MonoBehaviour
{
    public float velocidad = 5f;
    private float velocidadX;
    private Vector2 input; 
    private Vector2 posicionActual;
    [SerializeField] private SpriteRenderer sprite;
    private Animator animator;

    public float fuerzaSalto = 10f;
    public LayerMask suelo;
    public float logitudRayo;
    private bool enSuelo;
    private Rigidbody2D guerrero;

    private bool saltoPress;
    private bool saltando;

    public float escalaGravedadNormal = 1f;
    public float escalaGravedadCaida = 3f;
   
    
    private bool atacar;

    [SerializeField] private AudioClip sonidoSalto;
    [SerializeField] private AudioClip sonidoAterrizaje;
    [SerializeField] private AudioClip[] sonidosPasos;
    [SerializeField] private AudioClip[] sonidosEscalar;
    private AudioSource audioSource;


    [SerializeField] private float velocidadEscalar;
    private CapsuleCollider2D capsuleCollider2D;
    private float gravedadInicial;
    private bool escalando;
    public SpriteRenderer poste;

    private float tiempoUltimoAterrizaje;

    public CinemachineImpulseSource emisorImpulso;







    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        guerrero = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        gravedadInicial = guerrero.gravityScale;
        audioSource = GetComponent<AudioSource>(); 
        
        emisorImpulso = GetComponent<CinemachineImpulseSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
        movimiento();
        
        
        ataque();

       
    }
    private void FixedUpdate()
    {
       escalar();


    }

    public void Reproducirpaso()
    {
        if (audioSource == null) return;

        if (escalando)
        {
            if (sonidosEscalar.Length > 0)
            {
                int indice = UnityEngine.Random.Range(0, sonidosEscalar.Length);
                audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(sonidosEscalar[indice], 0.4f);
            }
        }
        else if (enSuelo)
        {
            if (sonidosPasos.Length > 0)
            {
                int indice = UnityEngine.Random.Range(0, sonidosPasos.Length);
                audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(sonidosPasos[indice], 0.3f);
            }
        }
    }

    public void aterrizaje()
    {
        if (audioSource == null) return;

        if (Time.time - tiempoUltimoAterrizaje > 0.5f)
        {
            if (sonidoAterrizaje != null)
            {
                audioSource.PlayOneShot(sonidoAterrizaje);
                tiempoUltimoAterrizaje = Time.time;
            }
        }



    }

    void movimiento()
    {
        //Movimiento -------------------------------------------------------------------------------

        velocidadX = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        
        transform.Translate(velocidadX, 0, 0);

        
        if (Mathf.Abs(velocidadX) > 0)
        {
            animator.SetBool("oxygen", false);
        }

        if (velocidadX > 0)
        {
            sprite.flipX = false;
            animator.SetBool("idle", false);
            animator.SetBool("run", true);
        }
        else if (velocidadX == 0 )
        {
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
        }

        if (velocidadX < 0)
        {
            sprite.flipX = true;
            animator.SetBool("idle", false);
            animator.SetBool("run", true);

        }

        // Salto ----------------------------------------------------------------------------------------------

        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, logitudRayo, suelo);
        enSuelo = hit.collider != null;
        animator.SetBool("enSuelo", enSuelo);
        saltoPress = Input.GetButtonDown("Jump");
       

        if (enSuelo && saltoPress)
        {
            guerrero.gravityScale = escalaGravedadNormal;
            guerrero.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            saltando = true; 

            if (sonidoSalto != null)
            {
                audioSource.PlayOneShot(sonidoSalto);
            }
        }
        if (!enSuelo)
        {
            animator.SetBool("air", true);
            animator.SetBool("idle", false);
            animator.SetBool("run", false);
            
            if (guerrero.linearVelocityY < -0.1f && !escalando)
            {
                guerrero.gravityScale = escalaGravedadCaida;
                animator.SetBool("jumpFalling", true);
                
                

            }
        }
        else
        {
            animator.SetBool("air", false);
            animator.SetBool("jumpFalling", false);
            guerrero.gravityScale = escalaGravedadNormal;

            if (saltando)
            {
                animator.SetTrigger("land");
                saltando = false;

                if (guerrero.gravityScale >= escalaGravedadCaida)
                {
                    aterrizaje();
                }
                else
                {
                    if (sonidoAterrizaje != null) audioSource.PlayOneShot(sonidoAterrizaje);
                }


            }
     
            bool moviendose = Math.Abs(Input.GetAxis("Horizontal")) > 0.01f;
            animator.SetBool("idle", !moviendose);
            animator.SetBool("run", moviendose);
       
        }


    }
   
     /// Ataque.... ---------------------------------------------------------

    public void Atacando()
    {
        atacar = true;
    }
    public void DesactivaAtaque()
    {
        atacar = false;
    }

    public void ataque()
    {/*
        if (Input.GetKeyDown(KeyCode.Mouse0) && !atacar && enSuelo)
        {
            Atacando();
        }

        animator.SetBool("atacar", atacar);*/

    }

    /// ---------------------------------------------------

   public void escalar()
    {
        input.y = Input.GetAxisRaw("Vertical");

        bool tocandoEscalera = capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Escaleras"));


        if (tocandoEscalera && (Math.Abs(input.y) > 0.1f || escalando))
           {
            Vector2 velocidadSubida = new Vector2(guerrero.linearVelocityX, input.y * velocidadEscalar);
            guerrero.linearVelocity = velocidadSubida;
            guerrero.gravityScale = 0;
            escalando = true;
            sprite.flipX = false;
            

            animator.SetBool("jumpFalling", false);
            animator.SetBool("air", false) ;

            animator.SetFloat("velocidadVertical", input.y);    
            poste.sortingOrder = 1;
        }
        else
        {
            guerrero.gravityScale = gravedadInicial; 
            escalando = false;
            poste.sortingOrder = -2; 
        }
       if (enSuelo)
        {

            escalando = false; 

        }

        animator.SetBool("estaEscalando", escalando);


    }

     private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * logitudRayo);


    }

   









}
