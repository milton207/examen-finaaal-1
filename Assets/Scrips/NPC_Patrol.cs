using UnityEngine;
using UnityEngine.AI;

public class NPC_Patrol : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    [Header("Configuración de Persecución")]
    public Transform player;          // Referencia al jugador
    public float detectionRange = 7f; // Distancia a la que detecta al jugador
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Busca automáticamente al jugador por la etiqueta "Player" si no se asignó manualmente
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        if (waypoints.Length > 0)
        {
            GotoNextWaypoint();
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si el jugador está dentro del rango, comienza a perseguir
            if (distanceToPlayer <= detectionRange)
            {
                isChasing = true;
            }
            // Si el jugador se aleja lo suficiente (rango + margen), regresa a patrullar
            else if (isChasing && distanceToPlayer > detectionRange + 3f)
            {
                isChasing = false;
                GotoNextWaypoint();
            }
        }

        if (isChasing)
        {
            // El destino pasa a ser la posición del jugador
            agent.destination = player.position;
        }
        else
        {
            // Continúa patrullando por los waypoints
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextWaypoint();
            }
        }
    }

    void GotoNextWaypoint()
    {
        if (waypoints.Length == 0 || isChasing) return;

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}