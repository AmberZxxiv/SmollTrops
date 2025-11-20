using UnityEngine;

public class Destroyer_Prime : MonoBehaviour
{
    // asegura no superponer en la primera sala
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("roomconection"))
        {
            Destroy(other.gameObject);
        }
    }
}
