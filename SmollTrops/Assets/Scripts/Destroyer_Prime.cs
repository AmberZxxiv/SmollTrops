using UnityEngine;

public class Destroyer_Prime : MonoBehaviour
{
    // Controla que solo salgan cerradas si no hay otra
    private void OnTriggerEnter(Collider other)
    {
        var otherSpawner = other.GetComponent<Room_Spawner>();

        // si choca con una sala spawneada, se elimina el cierre
        if (otherSpawner.spawned)
        {
            // se destroye el padre del trigger
            Destroy(transform.parent.gameObject);
        }
    }
}
