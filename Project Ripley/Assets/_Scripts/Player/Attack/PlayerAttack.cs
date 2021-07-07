using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct ComboTracker
{
    [SerializeField] int myMaxComboCount;
    [SerializeField] int myCurrentComboIndex;
    [SerializeField] float myComboCooldownLength;
    private float myComboCooldownTimer;

    public void AddToComboTimer(float aDelta)
    {
        myComboCooldownTimer += aDelta;
    }

    public bool HasExtededCooldown()
    {
        if (myComboCooldownTimer < myComboCooldownLength && myCurrentComboIndex < myMaxComboCount)
        {
            return true;
        }

        return false;
    }

    public void Reset()
    {
        myCurrentComboIndex = 0;
        myComboCooldownTimer = 0;
    }

    public void AddCombo()
    {
        myCurrentComboIndex += 1;
        myComboCooldownTimer = 0;
    }

    public int GetCurrentComboIndex()
    {
        return myCurrentComboIndex;
    }
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] bool myPlayerFollowsMouse = false;
    [SerializeField] float myPlayerFollowMouseLength;
    private float myPlayerFollowMouseTimer = -1;
    private float myMouseTrajectoryLength = 15;
    [Space]

    [SerializeField] ComboTracker myMeleeComboTracker;
    [SerializeField] ComboTracker myRangeComboTracker;

    private float myConsumeTimer = 0;

    private bool myOnClick = false;
    private bool myHasClicked = false;

    private Item myCurrentItem;
    private Animator myAnim;
    private Camera myCamera;

    void Awake()
    {
        myCamera = Camera.main;
        myAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Player.Instance.CanChangeState(PlayerState.Attacking))
        {
            bool canDoAction = Player.Instance.equipment.CanDoAction();

            if (Input.GetMouseButtonDown(0) && myOnClick == false && canDoAction == true)
            {
                Player.Instance.UpdateStateTo(PlayerState.Attacking);
                myAnim.SetBool("ExitRecovery", false);
                GetTypeOfAction();
                myOnClick = true;
            }

            if (myOnClick)
            {
                ActionHandler();
            }

            CharacterFollowMouse();
        }
    }

    void GetTypeOfAction()
    {
        myCurrentItem = Player.Instance.equipment.GetSelectedItem();
        myAnim.SetFloat("ItemAttackID", myCurrentItem.GetItemID());

        myAnim.SetFloat("AttackCombo", 0);
        myMeleeComboTracker.Reset();
        myRangeComboTracker.Reset();
    }

    void ActionHandler()
    {
        Item currentItem = Player.Instance.equipment.GetSelectedItem();

        if (myCurrentItem != currentItem)
        {
            GetTypeOfAction();
        }

        switch (currentItem.GetItemCategory())
        {
            case ItemCategory.Melee:
                MeleeAction(currentItem as Melee);
                break;
            case ItemCategory.Range:
                if (Player.Instance.inventory.GetAmmo(currentItem.GetItemType()) > 0)
                {
                    RangeAction(currentItem as Range);
                }
                else
                {
                    ResetVariables();
                    myOnClick = false;
                }
                break;
            case ItemCategory.Consumable:
                ConsumableAction(currentItem as Consumable);
                break;
            case ItemCategory.None:
                ResetVariables();
                myOnClick = false;
                break;
        }

    }

    void ResetVariables()
    {
        myConsumeTimer = 0;
        myMeleeComboTracker.Reset();
        myRangeComboTracker.Reset();
        myAnim.SetFloat("AttackCombo", 0);
        myAnim.SetBool("ExitRecovery", true);
        Player.Instance.UpdateStateTo(PlayerState.Idle);
    }

    void MeleeAttack()
    {
        myMeleeComboTracker.AddCombo();
        myAnim.SetFloat("AttackCombo", myMeleeComboTracker.GetCurrentComboIndex());
        myAnim.Play("Melee");

        myPlayerFollowsMouse = true;
    }

    void RangeAttack()
    {
        myRangeComboTracker.AddCombo();
        myAnim.SetFloat("AttackCombo", myRangeComboTracker.GetCurrentComboIndex());


        myAnim.Play("Range");
        myPlayerFollowsMouse = true;
    }

    void ConsumableAttack()
    {
        myAnim.Play("Consuming");
        myPlayerFollowsMouse = true;
    }

    void MeleeAction(Melee aM)
    {
        AnimatorStateInfo animStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        if (myHasClicked == false)
        {
            MeleeAttack();
            myHasClicked = true;
        }

        if (animStateInfo.IsName("MeleeRecover"))
        {
            myMeleeComboTracker.AddToComboTimer(Time.deltaTime);

            if (myMeleeComboTracker.HasExtededCooldown())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MeleeAttack();
                }
            }
            else
            {
                ResetVariables();
                myOnClick = false;
                myHasClicked = false;
            }
        }
    }

    void RangeAction(Range aR)
    {
        AnimatorStateInfo animStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        if (myHasClicked == false)
        {
            RangeAttack();
            aR.SetComboIndex(myRangeComboTracker.GetCurrentComboIndex());
            aR.SetLastClicked(MouseManager.Instance.GetMousePosition());
            myHasClicked = true;
        }

        if (animStateInfo.IsName("RangeRecover"))
        {
            myRangeComboTracker.AddToComboTimer(Time.deltaTime);

            if (myRangeComboTracker.HasExtededCooldown())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RangeAttack();
                    aR.SetComboIndex(myRangeComboTracker.GetCurrentComboIndex());
                    aR.SetLastClicked(MouseManager.Instance.GetMousePosition());
                }
            }
            else
            {
                ResetVariables();
                aR.SetComboIndex(0);
                myOnClick = false;
                myHasClicked = false;
            }
        }
    }

    void ConsumableAction(Consumable aC)
    {
        AnimatorStateInfo animStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        if (myHasClicked == false)
        {
            ConsumableAttack();
            myHasClicked = true;
        }

        myConsumeTimer += Time.deltaTime;

        if (myConsumeTimer > aC.GetConsumeLength())
        {
            myAnim.Play("ConsumingRecover");
        }

        if (animStateInfo.IsName("ConsumingRecover") || Input.GetMouseButton(0) == false)
        {
            ResetVariables();
            myOnClick = false;
            myHasClicked = false;
        }
    }
    void CharacterFollowMouse()
    {
        int x = (int)MouseManager.Instance.CalculateDirectionNonDisplay(transform, false).x;
        int y = (int)MouseManager.Instance.CalculateDirectionNonDisplay(transform, false).y;

        if (myPlayerFollowsMouse == true)
        {
            //movementDatabase.SetAnim(new Vector2(x, y));
            myAnim.SetFloat("Horizontal", x);
            myAnim.SetFloat("Vertical", y);

            myPlayerFollowMouseTimer = 0;
            myPlayerFollowsMouse = false;
        }

        if (myPlayerFollowMouseTimer >= 0) //&& movementDatabase.GetMoving() == 0
        {
            myPlayerFollowMouseTimer += Time.deltaTime;
            if (myPlayerFollowMouseTimer > myPlayerFollowMouseLength)
            {
                myPlayerFollowMouseTimer = -1;
            }
            else if (Player.Instance.GetState() == PlayerState.Idle)
            {
                myAnim.SetFloat("Horizontal", x);
                myAnim.SetFloat("Vertical", y);
            }
        }
    }
}