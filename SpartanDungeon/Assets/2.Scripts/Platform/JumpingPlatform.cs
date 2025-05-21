using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    public float power = 10f; // ���� Ƣ�� ������ ��

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) // �浹 Ȯ��
        {
            Rigidbody rb = collision.rigidbody;

            rb.velocity = Vector3.zero;

            rb.AddForce(transform.up * power, ForceMode.Impulse);
        }
    }
}
