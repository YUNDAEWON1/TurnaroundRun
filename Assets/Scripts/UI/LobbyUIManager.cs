using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LobbyUIManager : MonoBehaviour
{
    public Toggle[] chartoggles;
    public Button btnStart;
    private float prefabSpacing = 2f; // ������ ����

    private Dictionary<int, GameObject> prefabInstances = new Dictionary<int, GameObject>();

    private List<int> selectedToggleIndexes = new List<int>();

    public GameObject pnlSelected;

    

    public List<int> GetSelectedToggleIndexes()
    {
        return selectedToggleIndexes;    
    }

    // *** GetSelectedToggleIndexes() ���� ***

    // �ٸ� ��ũ��Ʈ���� ȣ���ϴ� �κ�
    //List<int> selectedIndexes = FindObjectOfType<LobbyUIManager>().GetSelectedToggleIndexes();

    // ���õ� ����� �ε��� ����� ���
    //foreach (int index in selectedIndexes)
    //{
    //    Debug.Log("���õ� ��� �ε���: " + index);
    //}

    private void Start()
    {
        foreach (Toggle toggle in chartoggles)
        {
            toggle.onValueChanged.AddListener(delegate { SoundMgr.Instance.PlayButtonClickSound(); });
        }
    }



    public void SelectChar()
    {
        for (int i = 0; i < chartoggles.Length; i++)
        {
            if (chartoggles[i].isOn)
            {
                // ����� ���õǸ� �ش� �ε����� �������� �����ϴ� �Լ� ȣ��
                GeneratePrefabForToggle(i);
                // ���õ� ��� �ε����� ����Ʈ�� �߰�
                if (!selectedToggleIndexes.Contains(i))
                {
                    selectedToggleIndexes.Add(i);
                }

            }
            else
            {
                // ����� ���õ��� ������ �ش� �ε����� �������� �����ϴ� �Լ� ȣ��
                DestroyPrefabForToggle(i);

                // ���� ������ ��� �ε����� ����Ʈ���� ����
                selectedToggleIndexes.Remove(i);
             
            }

        }
        // ���� �������� ��ġ ����
        RearrangePrefabPositions();
    }


    // ��� ���� �� ������ ���� �Լ�
    private void GeneratePrefabForToggle(int toggleIndex)
    {
        Debug.Log("���õ� ��� �ε���: " + toggleIndex);

        if (!prefabInstances.ContainsKey(toggleIndex))
        {   
            // �ε����� ���� �ٸ� ������ ���� ������ �߰�
            switch (toggleIndex)
            {
                case 0:
                    InstantiatePrefab("Prefab0",toggleIndex);
                    break;
                case 1:
                    InstantiatePrefab("Prefab1",toggleIndex);
                    break;
                case 2:
                    InstantiatePrefab("Prefab2",toggleIndex);
                    break;
                case 3:
                    InstantiatePrefab("Prefab3",toggleIndex);
                    break;
                case 4:
                    InstantiatePrefab("Prefab4",toggleIndex);
                    break;
                case 5:
                    InstantiatePrefab("Prefab5",toggleIndex);
                    break;
                case 6:
                    InstantiatePrefab("Prefab6",toggleIndex);
                    break;
                case 7:
                    InstantiatePrefab("Prefab7",toggleIndex);
                    break;
            }
        }
    }

    // ��� ���� ���� �� ������ ���� �Լ�
    private void DestroyPrefabForToggle(int toggleIndex)
    {
        Debug.Log("������ ������ ��� �ε���: " + toggleIndex);

        // �ε����� �ش��ϴ� ������ �ν��Ͻ��� �ִٸ� ����
        if (prefabInstances.ContainsKey(toggleIndex))
        {
            DestroyPrefab(toggleIndex);
        }
    }



    // ���� ������ ���� �Լ� (������ �̸��� ��� �ε����� �޾� ����)
    private void InstantiatePrefab(string prefabName, int toggleIndex)
    {
        // Resources ������ �ִ� ������ �ε�
        GameObject prefab = Resources.Load<GameObject>(prefabName);

        if (prefab != null)
        {
            // �������� �ν��Ͻ�ȭ�Ͽ� ����
            Vector3 spawnPosition = new Vector3(toggleIndex * prefabSpacing, 0f, 0f); // ������ ���� ��ġ ���
            GameObject instance = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // ������ ������ �ν��Ͻ��� ��ųʸ��� �߰�
            prefabInstances.Add(toggleIndex, instance);

            Debug.Log("������ ����: " + prefabName);
        }
        else
        {
            Debug.LogError("�������� ã�� �� �����ϴ�: " + prefabName);
        }
    }

    // ���� ������ ���� �Լ� (��� �ε����� �޾� ����)
    private void DestroyPrefab(int toggleIndex)
    {
        // ��ųʸ����� �ش� �ε����� ������ �ν��Ͻ��� ã�� ����
        GameObject prefabInstance;
        if (prefabInstances.TryGetValue(toggleIndex, out prefabInstance))
        {
            prefabInstances.Remove(toggleIndex);

            // ������ �ν��Ͻ��� �ı��Ͽ� ����
            Destroy(prefabInstance);
            Debug.Log("������ ����: " + toggleIndex);
        }
        else
        {
            Debug.LogError("������ ������ �ν��Ͻ��� ã�� �� �����ϴ�: " + toggleIndex);
        }
    }
    private void RearrangePrefabPositions()
    {
        float offset = prefabSpacing * 0.5f;

        var sortedPrefabInstances = prefabInstances.OrderBy(pair => pair.Key);

        int index = 0;
        foreach (var pair in sortedPrefabInstances)
        {
            Vector3 newPosition = new Vector3(index * prefabSpacing, 0f, 0f);

            // �߾� ����
            float totalWidth = (prefabInstances.Count - 1) * prefabSpacing;
            newPosition.x -= totalWidth / 2f;

            // �ٸ� �����յ���� �浹 üũ �� ����
            foreach (var otherPair in sortedPrefabInstances)
            {
                if (pair.Key != otherPair.Key)
                {
                    float distance = Mathf.Abs(newPosition.x - otherPair.Value.transform.position.x);
                    float minDistance = prefabSpacing;

                    if (distance < minDistance)
                    {
                        float overlap = minDistance - distance;
                        newPosition.x += overlap * Mathf.Sign(newPosition.x - otherPair.Value.transform.position.x);
                    }
                }
            }

            pair.Value.transform.position = newPosition;
            index++;
        }
    }
    public void ChangeGameScene()
    {
        if (selectedToggleIndexes.Count >= 2)
        {
            SoundMgr.Instance.ChangeBGMForScene();//���� ����
            SoundMgr.Instance.countDown();
            SceneManager.LoadScene("Game");
        }
        else
        {
            StartCoroutine(ShowPanelAndHideAfterDelay());
        }
    }

    private IEnumerator ShowPanelAndHideAfterDelay()
    {
        UIManager.Instance.ActivePnl(pnlSelected);

        // 2�� ���
        yield return new WaitForSeconds(2f);

        // �г� �ٽ� ����
        UIManager.Instance.ActivePnl(pnlSelected); 
        
    }
}



