using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameractrl : MonoBehaviour
{
    public float cameraSpeed = 10.0f; // ī�޶� �̵� �ӵ�
    public float startPosition = -210f; // ���� ��ġ
    public float endPosition = 510f; // �� ��ġ
    private bool movingForward = true; // �̵� ����

    void Update()
    {
        // ī�޶��� ���� ��ġ
        Vector3 currentPosition = transform.position;

        // ī�޶� Z �������� �̵�
        if (movingForward)
        {
            currentPosition.z += cameraSpeed * Time.deltaTime;
            if (currentPosition.z >= endPosition)
            {
                currentPosition.z = endPosition;
                movingForward = false;
            }
        }
        else
        {
            currentPosition.z -= cameraSpeed * Time.deltaTime;
            if (currentPosition.z <= startPosition)
            {
                currentPosition.z = startPosition;
                movingForward = true;
            }
        }

        transform.position = currentPosition;
    }

}
