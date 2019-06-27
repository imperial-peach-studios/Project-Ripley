using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionGiver : MonoBehaviour
{
    private Fungus.Flowchart chart;
    private string messageStarter;

    [SerializeField] UnityEvent OnStart;
    [SerializeField] UnityEvent OnInteractBySelf;
    [SerializeField] UnityEvent OnInteractByOther;

    private GameObject item;
    private bool readyToInteract = false;
    private NewPlayerInvetory newInvetory;
    private GameObject hitObject;
    private bool addTolist = false;
    private int currentIcon = -1;
    private Vector2 direction;

    void Awake()
    {
        OnStart.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(GetComponent<RaycastLookAt>() == null)
        {
            if (collision.transform.tag == "Player")
            {
                ActivateBySelf(collision.gameObject);
            }
        }
    }
     
    public void SetReadyToInteract(bool interact)
    {
        readyToInteract = interact;
    }

    public GameObject GetItem()
    {
        return item;
    }

    public void AddItem(GameObject newItem)
    {
        item = newItem;
    }

    public void AddFlowChart(Fungus.Flowchart chart)
    {
        this.chart = chart;
    }

    public void SendItemToInventory()
    {
        if(addTolist == false)
        {
            newInvetory.AddSingleItem(ref item, currentIcon);
            addTolist = true;
        }
    }

    void OnDisable()
    {
        if (item != null)
        {
            this.enabled = true;
        }
    }

    public void SetIconIndex(int id)
    {
        currentIcon = id;
    }

    public void DisableMovement()
    {
        GameObject mainObject = gameObject;

        if (newInvetory != null)
        {
            mainObject = newInvetory.gameObject;
        }
        else if (hitObject != null)
        {
            mainObject = hitObject;
        }

        if (mainObject != gameObject)
        {
            Rigidbody2D rb = mainObject.transform.GetComponent<Rigidbody2D>();
            PlayerMovement move = mainObject.transform.GetComponent<PlayerMovement>();
            Animator anim = mainObject.transform.GetComponent<Animator>();

            if (rb != null && anim != null)
            {
                move.DisableMove = true;

                rb.bodyType = RigidbodyType2D.Static;
                rb.velocity = Vector2.zero;

                if (direction != Vector2.zero)
                {
                    anim.SetFloat("Horizontal", direction.x);
                    anim.SetFloat("Vertical", direction.y);
                    anim.SetFloat("Moving", 0);
                }
            }
        }
    }

    public void EnableMovement()
    {
        GameObject mainObject = gameObject; 

        if(newInvetory != null)
        {
            mainObject = newInvetory.gameObject;
        }
        else if(hitObject != null)
        {
            mainObject = hitObject;
        }
        
        if(mainObject != gameObject)
        {
            Rigidbody2D rb = mainObject.transform.GetComponent<Rigidbody2D>();
            PlayerMovement move = mainObject.transform.GetComponent<PlayerMovement>();

            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                move.DisableMove = false;
            }
        }
    }

    public void SetDirectionX(int x)
    {
        this.direction = new Vector2(x, 0);
    }

    public void SetDirectionY(int y)
    {
        this.direction = new Vector2(0, y);
    }

    public void SetDirectionDependentOnPlace()
    {
        if(newInvetory != null)
        {
            direction = ((Vector2)transform.position - (Vector2)newInvetory.transform.position).normalized;
        }
        else if(hitObject != null)
        {
            direction = ((Vector2)transform.position - (Vector2)hitObject.transform.position).normalized;
        }
       
        direction = new Vector2(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y));
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    public void FungusMessage(string fungusMessage)
    {
        chart.SendFungusMessage(fungusMessage);
    }

    public void ActivateBySelf(GameObject hitObject)
    {
        this.hitObject = hitObject;
        OnInteractBySelf.Invoke();
    }
    
    public void ActivateByOther(NewPlayerInvetory playerInvetory)
    {
        if (readyToInteract == true)
        {
            
        }
        newInvetory = playerInvetory;
        addTolist = false;
        OnInteractByOther.Invoke();
    }
}