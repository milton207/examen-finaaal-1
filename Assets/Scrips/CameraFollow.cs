using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Objeto a seguir (Player)
    public Vector3 offset = new Vector3(0, 5, -7); // Distancia e inclinación de la cámara
    public float smoothSpeed = 5.0f;

    void Start()
    {
        // Busca automáticamente al jugador si no fue asignado en el Inspector
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada detrás del jugador
        Vector3 targetPosition = target.position + offset;

        // Movimiento suave de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Hacer que la cámara mire siempre al jugador
        transform.LookAt(target.position + Vector3.up * 1.2f);
    }
}