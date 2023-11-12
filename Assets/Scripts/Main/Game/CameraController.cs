using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera waitCamera;
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera firstPlaceCamera;

    // TODO : 1���� ĳ���� ã�Ƽ� ���� ���� ��ü
    [SerializeField] GameObject firstPlaceCharacter;

    void Start()
    {
        StartCoroutine(ControlCamera());
    }

    void Update()
    {
        
    }

    IEnumerator ControlCamera()
    {
        // 3�ʰ� ����ϰ� �ִ� �÷��̾ �� ������ 
        waitCamera.gameObject.SetActive(true);
        // TODO :
        // ������ ĳ���� ����ŭ ������ 

        yield return new WaitForSeconds(3f);

        // 3�ʰ� �޸��� ��� ��ü�� ������
        waitCamera.gameObject.SetActive(false);
        startCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (3�ʰ�) 1������ �޸��� �ִ� ����� ������
        startCamera.enabled = false;
        firstPlaceCamera.Follow = FindFirstPlace();
        firstPlaceCamera.enabled = true;

        // (2�ʰ�) 1, 2�� �ֺ����� ������ 

        // (2�ʰ�) 1�� ��ü�� ������

        // �ν��� �� ����� �ִ�ӵ��� �޸��Ƿ�, 
        // 5�ʰ� �ִ�ӵ��� �޸� �� ��¼��� ���� �� �ִ� �������� 
        // �ν��� �� ����� ��� �����ָ� 
        // ��¼��� ��������� ��¼� ī�޶�� ��ȯ�ǰ�
        //
        // �ν��� �� ��� �����ϱ� �������� 1�� ��ü�� �����ֱ� 
    }

    // ���� 1������ �޸��� �ִ� ĳ���͸� ã��
    private Transform FindFirstPlace()
    {
        return firstPlaceCharacter.GetComponent<Transform>();
    }

    IEnumerator ShowWaitingCharaters()
    {
        yield return null;
    }
}
