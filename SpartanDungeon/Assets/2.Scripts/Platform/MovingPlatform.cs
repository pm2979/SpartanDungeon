using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform[] waypoints; // 도착 위치
    [SerializeField] private float speed; // 움직임 속도
    [SerializeField] private float delayTime; // 대기 시간

    private int curIndex = 0;
    private bool isWaiting = false;
    private Vector3 lastPosition;
    private Rigidbody playerRb; // 플레이어 추적용

    private void Start()
    {
        // 초기 위치 세팅
        transform.position = waypoints[0].position;
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isWaiting || waypoints.Length == 0) return;

        // 다음 목표 지점까지 이동 계산
        Vector3 targetPos = waypoints[curIndex].position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

        // 이번 프레임 플랫폼 이동량 계산
        Vector3 delta = newPos - lastPosition;
        lastPosition = newPos;

        // 플랫폼 자체 이동
        transform.position = newPos;

        // 플레이어가 밟고 있으면, 동일한 이동량 만큼 플레이어도 이동
        if (playerRb != null)
        {
            playerRb.MovePosition(playerRb.position + delta);
        }

        // 웨이포인트 도달 시 대기 코루틴 실행
        if (Vector3.Distance(newPos, targetPos) < 0.01f)
            StartCoroutine(WaitingTime());
    }


    private IEnumerator WaitingTime() // 대기 및 waypoint 갱신
    {
        isWaiting = true;
        yield return new WaitForSeconds(delayTime);

        // 다음 인덱스
        curIndex = (curIndex + 1) % waypoints.Length;
        isWaiting = false;
    }

    private void OnCollisionEnter(Collision collision) // 플레이어 올라탄 순간 추적 시작
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerRb = collision.rigidbody;
        }
    }

    private void OnCollisionExit(Collision collision) // 플레이어가 플랫폼을 벗어나면 추적 해제
    {
        if (collision.rigidbody == playerRb)
        {
            playerRb = null;
        }
    }
}
