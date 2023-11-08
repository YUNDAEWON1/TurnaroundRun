using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    public GameObject[] platforms; // �÷��� ������ �迭
    public int poolSize = 6; // Ǯ�� ũ��
    public float platformLength = 33f; // �÷����� ����
    public Transform cameraTransform; // ī�޶��� Transform

    private Queue<GameObject> platformPool; // �÷��� Ǯ
    private Vector3 lastSpawnPoint; // ���������� ������ �÷����� ��ġ

    private void Start()
    {
        platformPool = new Queue<GameObject>();

        // ó���� Ǯ�� ũ�⸸ŭ �÷����� �����մϴ�.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(platforms[Random.Range(0, platforms.Length)]);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        // ���� ���� �ʱ�ȭ
        lastSpawnPoint = transform.position;

        for (int i = 0; i < poolSize; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        // ī�޶� �ٰ����� ���ο� �÷����� �����ϰ� ������ �÷����� �����մϴ�.
        if (cameraTransform.position.z > lastSpawnPoint.z - (poolSize * platformLength) + platformLength)
        {
            SpawnPlatform();
            RecyclePlatform();
        }
    }

    private void SpawnPlatform()
    {
        if (platformPool.Count == 0) return;

        GameObject platform = platformPool.Dequeue();
        platform.transform.position = lastSpawnPoint;
        platform.SetActive(true);

        lastSpawnPoint = new Vector3(lastSpawnPoint.x, lastSpawnPoint.y, lastSpawnPoint.z + platformLength);
    }

    private void RecyclePlatform()
    {
        // Ǯ���� ������� �ʴ� �÷����� ã�� �����մϴ�.
        foreach (var platform in platformPool)
        {
            if (platform.activeSelf && (cameraTransform.position.z - platform.transform.position.z) > (poolSize * platformLength))
            {
                platform.SetActive(false);
                platformPool.Enqueue(platform);
                return;
            }
        }
    }
}
