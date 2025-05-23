using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;
    public Transform rayPos;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    private ItemObject itemObject;

    public Inventory inventory;
    public TextMeshProUGUI promptText;
    private Camera _camera;
    private Ray ray;

    public void Init(Inventory inventory)
    {
        this.inventory = inventory;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // ȭ�� �߾ӿ��� Ray �߻�
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) // �浹 Ȯ��
            {
                if (hit.collider.gameObject != curInteractGameObject) // ������ �ٸ� Object�̸�
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();

                    if(hit.collider.TryGetComponent<ItemObject>(out ItemObject itemObject))
                    {
                        this.itemObject = itemObject;
                    }

                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText() // ��ȣ�ۿ� Object�� ����
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context) // ��ȣ�ۿ� (E)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            if(itemObject != null)
            {
                inventory.AddItem(itemObject.data);
                itemObject = null;
            }

            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
