using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] InventorySO inventorySO;
    //public ShowcaseItemsUI showcaseUI;
    public GameObject uiArrow;
    public LayerMask layersToCollide;
    public KeyCode pickUpKey;
    public float radius;
    GameObject closestObject;
    [Space]
    [SerializeField] string lootText;
    [SerializeField] string pickUpText;
    [SerializeField] string speakText;
    
    [Space]

    [SerializeField] string lootTag;
    [SerializeField] string pickUpTag;
    [SerializeField] string speakTag;

    public delegate void OnArrowShow(Vector3 position, InteractionGiver iGiver);
    public static event OnArrowShow OnShow;
    public static event OnArrowShow OnExit;
    public delegate void OnShowText(string text, Vector3 position);
    public static event OnShowText OnText;

    void Update()
    {
        RaycastHit2D[] circleHit = Physics2D.CircleCastAll(transform.position + new Vector3(0f, 0.5f, 0f), radius, Vector2.zero, 0f, layersToCollide);

        Vector3 newTransform = transform.position;
        float destination = Mathf.Infinity;
        uiArrow.gameObject.SetActive(false);

        foreach (RaycastHit2D r in circleHit)
        {
            Vector3 diff = r.transform.position - newTransform;
            float newDistance = diff.sqrMagnitude;
            if (destination > newDistance && newDistance >= 0)
            {
                destination = newDistance;
                closestObject = r.transform.gameObject;
            }
        }

        if (closestObject != null)
        {
            if (Vector3.Distance(transform.position, closestObject.transform.position) < radius)
            {
                if (closestObject.tag == lootTag)
                {
                    if (closestObject.GetComponent<InteractionGiver>().GetItem() != null)
                    {
                        OnText(lootText + " " + pickUpKey, Vector3.zero);
                        OnShow(new Vector3(closestObject.transform.position.x, closestObject.transform.position.y + 2f, closestObject.transform.position.z), closestObject.GetComponent<InteractionGiver>());
                    }
                    else
                    {
                        OnText("", Vector3.zero);
                        OnExit(Vector3.zero, null);
                    }
                }
                else if (closestObject.tag == pickUpTag)
                {
                    OnText(pickUpText + " " + pickUpKey, Vector3.zero);
                    OnExit(Vector3.zero, null);
                }
                else if (closestObject.tag == "InteractionGiver")
                {
                    OnText("", Vector3.zero);
                }
                else if (closestObject.tag == "InteractionInput")
                {
                    OnText(speakText + " " + pickUpKey, Vector3.zero);
                    uiArrow.gameObject.SetActive(true);
                    uiArrow.transform.position = new Vector3(closestObject.transform.position.x, closestObject.transform.position.y + 4f, closestObject.transform.position.z);
                }

                if (Input.GetKeyDown(pickUpKey))
                {
                    if (closestObject.GetComponent<InteractionGiver>() != null)
                    {
                        closestObject.GetComponent<InteractionGiver>().ActivateByOther(GetComponent<NewPlayerInvetory>());
                    }
                }
            }
            else
            {
                OnText("", Vector3.zero);
                uiArrow.gameObject.SetActive(false);
                OnExit(Vector3.zero, null);
            }

            InLootingMode();
        }
    }

    public void InLootingMode()
    {
        if (Vector3.Distance(transform.position, closestObject.transform.position) < radius)
        {
            if (closestObject.tag == "InteractionLoot" && closestObject.GetComponent<InteractionGiver>().enabled == true)
            {
                inventorySO.SetLootingMode(true);
            }
            else
            {
                inventorySO.SetLootingMode(false);
            }
        }
        else
        {
            inventorySO.SetLootingMode(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 0.5f, 0f), radius);
        Gizmos.color = Color.cyan;
        if (Application.isPlaying && closestObject != null)
        {
            Gizmos.DrawLine(transform.position, closestObject.transform.position);
        }
    }
}