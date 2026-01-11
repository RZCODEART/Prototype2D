using Unity.Cinemachine;
using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    
    public CinemachineCamera camaraSpline;
    public CinemachineCamera camaraPlayer; 

    
    private CinemachineSplineDolly splineDolly;
    private bool yaCambio = false;

    void Start()
    {
        
        splineDolly = camaraSpline.GetComponent<CinemachineSplineDolly>();

       
        camaraSpline.Priority = 20;
        camaraPlayer.Priority = 10;
    }

    void Update()
    {
        if (splineDolly == null || yaCambio) return;

        
        if (splineDolly.CameraPosition >= 0.99f)
        {
            CambiarAPlayer();
        }
    }

    void CambiarAPlayer()
    {
        yaCambio = true;

        
        camaraSpline.Priority = 10;
        camaraPlayer.Priority = 20;

        
    }
}
