using UnityEngine;
using UnityEngine.AI; // 내비메쉬 관련 코드
using Photon.Pun;
using System.Collections;

public class ItemSpawner : MonoBehaviourPun
{
    public GameObject[] items; // 생성할 아이템들
    Transform playerTransform; // 플레이어의 트랜스폼
    GameObject selectedItem; // 아이템 복제
    GameObject item;


    public float maxDistance = 5f; // 플레이어 위치로부터 아이템이 배치될 최대 반경

    public float timeBetSpawnMax = 1f; // 최대 시간 간격
    public float timeBetSpawnMin = 0.3f; // 최소 시간 간격
    private float timeBetSpawn; // 생성 간격

    private float lastSpawnTime; // 마지막 생성 시점
    // Start is called before the first frame update

    public static ItemSpawner instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ItemSpawner>();
            }
            return m_instance;
        }
    }
    private static ItemSpawner m_instance;

    void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;

        playerTransform = GameObject.Find("Player(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (Time.time >= lastSpawnTime + timeBetSpawn)//&& playerTransform != null
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Spawn();
        }

    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(Vector3.zero, maxDistance);
        spawnPosition += Vector3.forward * 0.4f;

        if (GameManager.instance.CheckTime() < 30f)
        {
            selectedItem = items[Random.Range(0, items.Length - 1)];
        }
        else
        {
            selectedItem = items[Random.Range(0, items.Length)];
        }

        item = PhotonNetwork.Instantiate(selectedItem.name, spawnPosition, Quaternion.identity);
        StartCoroutine(DestroyAfter(item, 3f));

    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);
        return hit.position;
    }

    public void UseSpawn()
    {
        ItemSpawner.instance.Spawn();
    }
    IEnumerator DestroyAfter(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (target != null)
        {
            PhotonNetwork.Destroy(target);
        }
    }
}
