using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] List<GameObject> characterGroup;       // ĳ���� �����յ��� ��
    [SerializeField] List<GameObject> selectedCharacter;    // ���õ� ĳ����
    List<int> selectedIndex;

    void Start()
    {
        // TODO : ���ӽ��� ���� �� �ٲٱ� selectedIndexe
        selectedIndex = new List<int>();
        selectedIndex.Add(0);
        selectedIndex.Add(1);
        //selectedIndexe = FindObjectOfType<LobbyUIManager>().GetSelectedToggleIndexes();

        SetCharactersPosition();
    }

    void Update()
    {
        
    }

    private void SetCharactersPosition()
    {
        int selectedNum = selectedIndex.Count;
        float firstPosition = 0 - 0.5f * (selectedNum - 1);

        // selectedIndex�� �ִ� ������ selectedCharacter�� �־��ֱ� 
        // ���õ� ĳ���͵鸸 ȭ�鿡 ���̰� Ȱ��ȭ
        for (int i = 0; i < selectedIndex.Count; i++)
        {
            GameObject cha = Instantiate(characterGroup[selectedIndex[i]], new Vector3(firstPosition, 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);

            firstPosition += 2;
        }
    }
}
