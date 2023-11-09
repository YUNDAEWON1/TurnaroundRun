using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCharacter : MonoBehaviour
{
    [SerializeField] List<GameObject> characterGroup;       // ĳ���� �����յ��� ��
    [SerializeField] List<GameObject> selectedCharacter;    // ���õ� ĳ����
    [SerializeField] List<int> selectedIndex;               // ���õ� UI �̹��� �ε���
    [SerializeField] List<float> characterPosition;         // ���õ� ĳ���͵��� ���� ��ġ

    void Start()
    {
        selectedCharacter = new List<GameObject>();
        characterPosition = new List<float>();
    }

    void Update()
    {
        // TODO : 
        // UI �̹������� �̹� ���õǾ� ������ ������ �� ���� �ϴ� �� �ʿ�
        // ������ list�ȿ� �̹� �ش� ���ӿ�����Ʈ�� �ִ��� üũ���ϰ� �ִٺ���
        // ��� �����ǰ� ���� 

        if(Input.GetKeyDown(KeyCode.Space))
        {
            selectedIndex.Clear();
            selectedIndex.Add(1);
            selectedIndex.Add(2);
            OnClickCharacter();
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            selectedIndex.Clear();
            selectedIndex.Add(1);
            OnDeselectCharacter();
        }
    }

    public void OnClickCharacter()
    {
        InitializeList();

        int selectedNum = selectedIndex.Count;
        int firstPosition = 0 - 1 * (selectedNum - 1);
        characterPosition.Add(firstPosition);
        // selectedIndex ���� ���� ��ġ ������ list ����� 
        for (int i = 1; i < selectedNum; i++)
        {
            characterPosition.Add(characterPosition[i - 1] + 2);
        }

        // selectedIndex�� �ִ� ������ selectedCharacter�� �־��ֱ� 
        // ���õ� ĳ���͵鸸 ȭ�鿡 ���̰� Ȱ��ȭ
        for (int i = 0; i < selectedIndex.Count; i++)
        {
            GameObject cha = Instantiate(characterGroup[selectedIndex[i]], new Vector3(characterPosition[i], 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);
        }
    }

    public void OnDeselectCharacter()
    {
        foreach(GameObject character in selectedCharacter)
        {
            Destroy(character);
        }

        // ���õ� ĳ���͵鸸 �ٽ� �����
        OnClickCharacter();
    }

    private void InitializeList()
    {
        selectedCharacter.Clear();
        characterPosition.Clear();
    }
}
