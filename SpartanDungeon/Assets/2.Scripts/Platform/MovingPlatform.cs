using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform[] waypoints; // ���� ��ġ
    [SerializeField] private float speed; // ������ �ӵ�
    [SerializeField] private float delayTime; // ��� �ð�

    private int curIndex = 0;
    private bool isWaiting = false;
    private Vector3 lastPosition;
    private Rigidbody playerRb; // �÷��̾� ������

    private void Start()
    {
        // �ʱ� ��ġ ����
        transform.position = waypoints[0].position;
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isWaiting || waypoints.Length == 0) return;

        // ���� ��ǥ �������� �̵� ���
        Vector3 targetPos = waypoints[curIndex].position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

        // �̹� ������ �÷��� �̵��� ���
        Vector3 delta = newPos - lastPosition;
        lastPosition = newPos;

        // �÷��� ��ü �̵�
        transform.position = newPos;

        // �÷��̾ ��� ������, ������ �̵��� ��ŭ �÷��̾ �̵�
        if (playerRb != null)
        {
            playerRb.MovePosition(playerRb.position + delta);
        }

        // ��������Ʈ ���� �� ��� �ڷ�ƾ ����
        if (Vector3.Distance(newPos, targetPos) < 0.01f)
            StartCoroutine(WaitingTime());
    }


    private IEnumerator WaitingTime() // ��� �� waypoint ����
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayTime);

        // ���� �ε���
        curIndex = (curIndex + 1) % waypoints.Length;
        isWaiting = false;
    }

    private void OnCollisionEnter(Collision collision) // �÷��̾� �ö�ź ���� ���� ����
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerRb = collision.rigidbody;
        }
    }

    private void OnCollisionExit(Collision collision) // �÷��̾ �÷����� ����� ���� ����
    {
        if (collision.rigidbody == playerRb)
        {
            playerRb = null;
        }
    }
}
