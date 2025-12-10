using UnityEngine;
using Unity.AI.Navigation;
using System.Collections.Generic;

public class Room_Manager : MonoBehaviour
{ // este script está en el empty ROM_MAN del inspector

    // Listas donde declaro las salas desde el _ROOM_MAN_ del inspector
    public GameObject[] room1Z;
    public GameObject[] room1X;
    public GameObject[] room0z;
    public GameObject[] room0x;
    public GameObject closedRoom;
    // lista control de las salas en el mapa
    public List<GameObject> roomMap;
    public int roomsSpawned = 0;
    public int maxRooms = 15;
    public NavMeshSurface surface;
    // gestion de enemigos
    public GameObject bossBall;
    public GameObject minionBall;
    public int minionCount = 3;
    // gestion de powers
    public List<GameObject> powerHands;

    // singletonpara llamar a este código desde cualquier otro
    public static Room_Manager instance;

    
    void Awake()
    { // awake para instanciar singleton sin superponer varios
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke("BakeNavMesh", 2f); //timer bake navmesh
        Invoke("SpawnEnemy", 2.5f); //timer spawn de enemigos
    }

    void BakeNavMesh() 
    { 
        surface.BuildNavMesh(); 
    }

    void SpawnEnemy()
    {
        Instantiate(bossBall, roomMap[roomMap.Count-1].transform.position + Vector3.up * 5, transform.rotation);

        float radio = 1.5f;
        for(int i = 0; i < roomMap.Count-1; i++)
        {
           Vector3 center = roomMap[i].transform.position + Vector3.up * 2.5f;
       
            for (int m = 0; m < minionCount; m++)
            {
               float angle = (360f / minionCount) * m;
                Vector3 offset = new Vector3
                (Mathf.Cos(angle * Mathf.Deg2Rad), 0,
                 Mathf.Sin(angle * Mathf.Deg2Rad)) * radio;

                Instantiate(minionBall, center + offset, transform.rotation);

            }
        }

    }
}
