using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractReceiver : MonoBehaviour
{
    [SerializeField] private GameObject myClosestObject;
    private GameObject myPreviousObject;
    [Space]
    [SerializeField] float myRadius;
    [SerializeField] bool myShowRadius = false;
    [Space]
    [SerializeField] LayerMask myInteractionMask;
    [SerializeField] KeyCode myPickUpKey;
    [SerializeField] string myInteractInputName;
    [SerializeField] float myPickUpDelay = 0.0f;

    bool myIsPickingUp = false;

    private ItemInteractor myItemInteractor;
    private ItemShow myItemShow;

    void Update()
    {
        if (myIsPickingUp)
        {
            return;
        }

        if (Player.Instance.CanChangeState(PlayerState.PickingUp))
        {
            FindClosestObject();

            if (myClosestObject == null)
            {
                if (myPreviousObject != null)
                {
                    myItemShow?.OnExit();
                    myPreviousObject = null;
                }
                return;
            }

            if (myClosestObject != myPreviousObject)
            {
                myItemInteractor = myClosestObject?.GetComponent<ItemInteractor>();
                myItemShow = myClosestObject?.GetComponent<ItemShow>();
                myPreviousObject = myClosestObject;
            }

            myItemShow?.OnActive();

            if (Input.GetButtonDown(myInteractInputName))
            {
                //myItemInteractor.Invoke();
                //myItemShow?.OnExit();

                StartCoroutine(PickUpItem(myItemInteractor, myItemShow));
                Player.Instance.UpdateStateTo(PlayerState.PickingUp);
                myIsPickingUp = true;

                myClosestObject = null;
                myPreviousObject = null;
            }
        }
    }

    IEnumerator PickUpItem(ItemInteractor aItemInteract, ItemShow aItemShow)
    {
        yield return new WaitForSeconds(myPickUpDelay);
        aItemInteract.Invoke();
        aItemShow?.OnExit();
    }

    void FindClosestObject()
    {
        RaycastHit2D[] circleHit = Physics2D.CircleCastAll(transform.position + new Vector3(0f, 0.5f, 0f), myRadius, Vector2.zero, 0f, myInteractionMask);

        //if(circleHit.Length == 0)
        //{
        //    return;
        //}

        Vector3 newTransform = transform.position;
        float destination = Mathf.Infinity;
        myClosestObject = null;

        foreach (RaycastHit2D r in circleHit)
        {
            Vector3 diff = r.transform.position - newTransform;
            float newDistance = diff.sqrMagnitude;
            if (destination > newDistance && newDistance >= 0)
            {
                destination = newDistance;
                myClosestObject = r.transform.gameObject;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (myShowRadius)
        {
            Gizmos.DrawWireSphere(transform.position, myRadius);
        }
    }

    public void FinishedPickingUp()
    {
        Player.Instance.UpdateStateTo(PlayerState.Idle);
        myIsPickingUp = false;
    }
}