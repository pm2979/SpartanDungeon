using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    public float power = 10f; // 위로 튀어 오르는 힘

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) // 충돌 확인
        {
            Rigidbody rb = collision.rigidbody;

            rb.velocity = Vector3.zero;

            rb.AddForce(transform.up * power, ForceMode.Impulse);
        }
    }
}
