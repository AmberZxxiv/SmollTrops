using UnityEngine;

public class Destroyer_Prime : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // lo elimina tarde?
        if (other.CompareTag("spawnroom"))
        {
            // se destruye el trigger que intenta generar
            Destroy(other.gameObject);
        }
    }
}
