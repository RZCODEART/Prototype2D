using System.Collections.Generic;
using UnityEngine;

public class OxigenCiylinderPooling : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    public Transform[] PuntosOxygen; 
    public float TiempoAparicion = 2.0f;  

    private List<GameObject> pool;
    private float Tiempo;

    void Start()
    {
        
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); 
            pool.Add(obj);
        }
    }

    void Update()
    {
        Tiempo += Time.deltaTime;

        if (Tiempo >= TiempoAparicion)
        {
            SpawnFromPool();
            Tiempo = 0;
        }
    }

    void SpawnFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy) 
            {
               
                int randomIndex = Random.Range(0, PuntosOxygen.Length);
                obj.transform.position = PuntosOxygen[randomIndex].position;
                obj.transform.rotation = PuntosOxygen[randomIndex].rotation;

                obj.SetActive(true); 
                return;
            }
        }
    }
}
