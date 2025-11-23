using UnityEngine;

public class Room_Spawner : MonoBehaviour
{
    public int doorDirection;
    // 1 = door en 1X
    // 2 = door en 0X
    // 3 = door en 1Z
    // 4 = door en 0Z
    public Room_Manager _RM; //singleton de las listas
    public bool spawned = false;
    public bool closed = false;
    // cortar spawn (NO FUNDIONAN MULTI PUERTAS, generan varias)

    void Start()
    {
        // pillo el singleton de las listas
        _RM = Room_Manager.instance;
        // SPAWNEO CADA X SEGUNDOS PORQUE PETA
        Invoke("SpawnRoom", 0.5f);
        // SPAWNEO CADA X SEGUNDOS PORQUE PETA

    }

    void SpawnRoom()
    {
        // si ya ha spawneado, que no lo haga mas
        if (spawned || closed)
        {
            return;
        }

        // si tenemos el maximo de salas colocadas, se cierre
        if (_RM.roomSpawned >= _RM.maxRooms)
        {
            Instantiate(_RM.closedRoom, transform.position, transform.rotation);
            closed = true;
            return;
        }

        if (spawned == false)
        {
            if (doorDirection == 1) //para puerta en 1X busca puerta en 0X=2
            {
                Instantiate(_RM.room0X[Random.Range(0, _RM.room1X.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            if (doorDirection == 2) //para puerta en 0X busca puerta en 1X=1
            {
                Instantiate(_RM.room1X[Random.Range(0, _RM.room0X.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            if (doorDirection == 3) //para puerta en 1Z busca puerta en 0Z=4
            {
                Instantiate(_RM.room0Z[Random.Range(0, _RM.room1Z.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            if (doorDirection == 4) //para puerta en 0Z busca puerta en 1Z=3
            {
                Instantiate(_RM.room1Z[Random.Range(0, _RM.room0Z.Length)], transform.position + Vector3.down * 2f, transform.rotation);
            }
            _RM.roomSpawned++;
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherSpawner = other.GetComponent<Room_Spawner>();
        // aqui cojo el objeto con el script (el trigger)
        // Si intentan spawnear 2 en el mismo lugar, elimino la sala
        if (!spawned && !otherSpawner.spawned)
        {
            // destruyo el trigger de conexion antes de que se genere la sala
            Destroy(gameObject);
        }
    }
}
