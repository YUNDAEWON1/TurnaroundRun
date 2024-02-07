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

    public GameObject pnlSelected;

    [SerializeField] List<GameObject> prefabs;              // ��ü ������ ĳ���͵�
    [SerializeField] List<GameObject> selectedCharacter;    // ���õ� ĳ����
    [SerializeField] List<int> selectedToggleIndexes;       // ���õ� ĳ���͵��� �ε���
    [SerializeField] List<float> characterPosition;         // ���õ� ĳ���͵��� ���� ��ġ

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

        selectedCharacter = new List<GameObject>();
        characterPosition = new List<float>();
    }

    public void SelectChar()
    {
        for (int i = 0; i < chartoggles.Length; i++)
        {
            if (chartoggles[i].isOn)
            {
                // ���õ� ��� �ε����� ����Ʈ�� �߰�
                if (!selectedToggleIndexes.Contains(i))
                {
                    selectedToggleIndexes.Add(i);
                }

                DestroyPrefabForToggle();
            }
            else
            {
                 // ���� ������ ��� �ε����� ����Ʈ���� ����
                selectedToggleIndexes.Remove(i);

                // ����� ���õ��� ������ �ش� �ε����� �������� �����ϴ� �Լ� ȣ��
                DestroyPrefabForToggle();
            }
        }
    }

    // ��� ���� �� ������ ���� �Լ�
    private void GeneratePrefabForToggle()
    {
        InitializeList();

        int selectedNum = selectedToggleIndexes.Count;
        float firstPosition = 0 - 1f * (selectedNum - 1);
        characterPosition.Add(firstPosition);

        // selectedIndex ���� ���� ��ġ ������ list ����� 
        for (int i = 1; i < selectedNum; i++)
        {
            characterPosition.Add(characterPosition[i - 1] + prefabSpacing);
        }

        // selectedIndex�� �ִ� ������ selectedCharacter�� �־��ֱ� 
        // ���õ� ĳ���͵鸸 ȭ�鿡 ���̰� Ȱ��ȭ
        for (int i = 0; i < selectedToggleIndexes.Count; i++)
        {
            GameObject cha = Instantiate(prefabs[selectedToggleIndexes[i]], new Vector3(characterPosition[i], 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);
        }
    }

    // ��� ���� ���� �� ������ ���� �Լ�
    private void DestroyPrefabForToggle()
    {
        foreach (GameObject character in selectedCharacter)
        {
            Destroy(character);
        }

        // ���õ� ĳ���͵鸸 �ٽ� �����
        GeneratePrefabForToggle();
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

    private void InitializeList()
    {
        selectedCharacter.Clear();
        characterPosition.Clear();
    }
}



