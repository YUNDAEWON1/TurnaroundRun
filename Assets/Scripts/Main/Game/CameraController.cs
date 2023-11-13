using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera waitCamera;
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera firstPlaceCamera;
    [SerializeField] CinemachineVirtualCamera boostCamera;
    [SerializeField] CinemachineVirtualCamera finalCamera;

    [SerializeField] GameObject firstPlaceCharacter;
    [SerializeField] GameObject boostCharacter;

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

        // 2�ʰ� �޸��� �����ϴ� ��� ��ü�� ������
        waitCamera.gameObject.SetActive(false);
        startCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        // (3�ʰ�) 1������ �޸��� �ִ� ����� ������
        startCamera.gameObject.SetActive(false);
        firstPlaceCharacter = FindFirstPlace();
        firstPlaceCamera.Follow = firstPlaceCharacter.GetComponent<Transform>();
        firstPlaceCamera.LookAt = firstPlaceCharacter.GetComponent<Transform>();
        firstPlaceCamera.transform.position = new Vector3(firstPlaceCharacter.transform.position.x, 2.91f, firstPlaceCharacter.transform.position.z + 4.1f);
        firstPlaceCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (5�ʰ�) �ν��� �޸��� �ִ� ����� ������ 
        firstPlaceCamera.gameObject.SetActive(false);
        boostCharacter = FindBooster();
        boostCamera.Follow = boostCharacter.GetComponent<Transform>();
        boostCamera.LookAt = boostCharacter.GetComponent<Transform>();
        boostCamera.transform.position = new Vector3(boostCharacter.transform.position.x, 2.91f, firstPlaceCharacter.transform.position.z + 4.1f);
        boostCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        // (5�ʰ�) ��¼��� �����ϴ� ������� ������ 
        boostCamera.gameObject.SetActive(false);
        finalCamera.gameObject.SetActive(true);

        // �ν��� �� ����� �ִ�ӵ��� �޸��Ƿ�, 
        // 5�ʰ� �ִ�ӵ��� �޸� �� ��¼��� ���� �� �ִ� �������� 
        // �ν��� �� ����� ��� �����ָ� 
        // ��¼��� ��������� ��¼� ī�޶�� ��ȯ�ǰ�
        //
        // �ν��� �� ��� �����ϱ� �������� 1�� ��ü�� �����ֱ� 
    }

    // ���� 1������ �޸��� �ִ� ĳ���͸� ã��
    private GameObject FindFirstPlace()
    {
        return CharacterManager.Instance.SelectFirstPlace();
    }

    IEnumerator ShowWaitingCharaters()
    {
        yield return null;
    }

    // �ν��ͷ� �޸��� �ִ� ĳ���͸� ã��
    private GameObject FindBooster()
    {
        return CharacterManager.Instance.GetBoostCharacter();
    }
}
