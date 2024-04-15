using UnityEngine;
using UnityEngine.AI; // ����޽� ���� �ڵ�
using Photon.Pun;
using System.Collections;

public class ItemSpawner : MonoBehaviourPun
{
    public GameObject[] items; // ������ �����۵�
    Transform playerTransform; // �÷��̾��� Ʈ������
    GameObject selectedItem; // ������ ����
    GameObject item;


    public float maxDistance = 5f; // �÷��̾� ��ġ�κ��� �������� ��ġ�� �ִ� �ݰ�

    public float timeBetSpawnMax = 1f; // �ִ� �ð� ����
    public float timeBetSpawnMin = 0.3f; // �ּ� �ð� ����
    private float timeBetSpawn; // ���� ����

    private float lastSpawnTime; // ������ ���� ����
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
