using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class PosteEscalera : MonoBehaviour
{
    private List<SpriteRenderer> renderersPostes = new List<SpriteRenderer>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer[] todosLosSprites = Object.FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);

        foreach (SpriteRenderer sr in todosLosSprites)
        {
            // Si el nombre coincide, lo guardamos en nuestra lista privada
            if (sr.gameObject.name == "stair6-Poste_0")
            {
                renderersPostes.Add(sr);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CambiarOrdenPostes(1);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CambiarOrdenPostes(-1);
        }
            
    }

    private void CambiarOrdenPostes(int nuevoOrden)
    {
        foreach (SpriteRenderer sr in renderersPostes)
        {
            if (sr != null)
            {
                sr.sortingOrder = nuevoOrden;
            }
        }
    }


}
