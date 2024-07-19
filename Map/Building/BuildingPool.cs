using System.Collections.Generic;
using UnityEngine;

public class BuildingPool : MonoBehaviour
{
    public static BuildingPool Instance;

    public List<GameObject> buildingPrefabs;
    public int poolSize = 10;
    public float spawnDistance = 50f;
    public float despawnDistance = -50f;
    public float buildingSpacing = 20f; // Espaciado entre edificios

    public Transform leftSpawnPoint; // Punto de spawn para los edificios de la izquierda
    public Transform rightSpawnPoint; // Punto de spawn para los edificios de la derecha

    private Queue<GameObject> leftBuildingQueue;
    private Queue<GameObject> rightBuildingQueue;
    private Transform playerTransform;
    private float leftSpawnZ;
    private float rightSpawnZ;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        leftBuildingQueue = new Queue<GameObject>();
        rightBuildingQueue = new Queue<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Cargar los prefabs de edificios desde la carpeta Resources/Buildings
        buildingPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Buildings"));

        // Inicializar el pool de objetos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject leftBuilding = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)]);
            leftBuilding.SetActive(false);
            leftBuildingQueue.Enqueue(leftBuilding);

            GameObject rightBuilding = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)]);
            rightBuilding.SetActive(false);
            rightBuildingQueue.Enqueue(rightBuilding);
        }

        // Configurar la posición inicial de los edificios
        leftSpawnZ = playerTransform.position.z;
        rightSpawnZ = playerTransform.position.z;

        // Generar los primeros edificios
        for (int i = 0; i < poolSize / 2; i++)
        {
            ReuseBuilding(leftBuildingQueue, leftSpawnPoint.position.x, ref leftSpawnZ);
            ReuseBuilding(rightBuildingQueue, rightSpawnPoint.position.x, ref rightSpawnZ);
        }
    }

    void Update()
    {
        // Reutilizar edificios si están fuera del rango
        if (leftBuildingQueue.Peek().transform.position.z < playerTransform.position.z + despawnDistance)
        {
            ReuseBuilding(leftBuildingQueue, leftSpawnPoint.position.x, ref leftSpawnZ);
        }

        if (rightBuildingQueue.Peek().transform.position.z < playerTransform.position.z + despawnDistance)
        {
            ReuseBuilding(rightBuildingQueue, rightSpawnPoint.position.x, ref rightSpawnZ);
        }
    }

    void ReuseBuilding(Queue<GameObject> buildingQueue, float xPosition, ref float spawnZ)
    {
        GameObject building = buildingQueue.Dequeue();
        building.SetActive(true);
        building.transform.position = new Vector3(xPosition, 0, spawnZ);
        spawnZ += buildingSpacing; // Incrementar la posición Z para el próximo spawn
        buildingQueue.Enqueue(building);
    }
}
