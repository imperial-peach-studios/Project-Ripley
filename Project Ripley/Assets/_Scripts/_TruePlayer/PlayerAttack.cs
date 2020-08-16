using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float followMouseRate;
    [SerializeField] float clickRate;

    float mouseTrajectoryLength = 15;
    float followMouseTimer = -1;

    private float attackTimer = 0;
    private float attackRate = 1;
    private float consumeTimer = 0;
    private float clickTimer = 0;

    bool stopMoving = false;

    public bool hasAttacked = false;

    [SerializeField] Animator myAnim;

    [SerializeField] bool followMouse = false;

    [SerializeField] float attackCombo = 0;
    [SerializeField] float maxCombos = 3;
    float comboWaitingTimer = 0;
    [SerializeField] float comboWaitingLength;

    void Update()
    {
        MouseDatabase.UpdateMousePosition();

        //AnimationController();
        Action();
    }

    void Action()
    {
        //int selectedIndex = (int)Equipment.Instance.SelectedEQ;

        //int itemIndex = -1;
        //if (selectedIndex == 0)
        //    itemIndex = Equipment.Instance.Primary;
        //else
        //    itemIndex = Equipment.Instance.Secondary;

        Items info = Player.Instance.equipment.GetCurrentSelectedItems();

        if (info?.noDurability == false)
        {
            if (info is Melee m)
            {
                Melee(m);
            }
            else if (info is Range r)
            {
                Range(r);
            }
            else if(info is Consumable c)
            {
                Consumable(c);
            }
        }

        if (info?.noDurability == true)
        {
            int selectedIndex = Player.Instance.equipment.GetCurrentSelectedIndex(); //Equipment.Instance.GetCurrentSelectedIndex()
            GameData.aData.iData.AddDestroyedItem(Player.Instance.inventory.GetItemInventorySlot(selectedIndex)); //Inventory.Instance.GetItemInventorySlot(selectedIndex)

            //Inventory.Instance.DestroyItemAtIndex(selectedIndex);
            Player.Instance.inventory.DestroyItemAtIndex(selectedIndex);

            attackCombo = 0;
            consumeTimer = 0;
            //myAnim.SetBool("DoneConsuming", false);
            comboWaitingTimer = 0;
            myAnim.SetBool("ExitRecovery", true);
            //playerMovement.StopMoving = false;
            //GetComponent<PlayerDash>().enabled = true;
            Player.Instance.SetAllMovementActive(true);
        }

        CharacterFollowMouse();
    }

    void Melee(Melee m)
    {
        if (Input.GetMouseButtonDown(0) && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Melee") && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeRecover")) //When We Get Input
        {
            myAnim.SetFloat("ItemAttackID", m.animationID);
            myAnim.Play("Melee");
            attackCombo += 1;
            myAnim.SetFloat("AttackCombo", attackCombo);
            myAnim.SetBool("ExitRecovery", false);

            comboWaitingTimer = 0;
            //followMouseTimer = 0;
            followMouse = true;
            //playerMovement.StopMoving = true;
            //GetComponent<PlayerDash>().enabled = false;
            Player.Instance.SetAllMovementActive(false);
        }
        
        //if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack Before Recovery"))
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        myAnim.Play("Click Attacks");
        //        attackCombo += 1;
        //        myAnim.SetFloat("AttackCombo", attackCombo);

        //        comboWaitingTimer = 0;
        //        return;
        //    }
        //}

        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("MeleeRecover"))
        {
            //attackCombo = 0;
            //comboWaitingTimer = 0;

            comboWaitingTimer += Time.deltaTime;

            if(comboWaitingTimer < comboWaitingLength && attackCombo < 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    myAnim.Play("Melee");
                    attackCombo += 1;
                    myAnim.SetFloat("AttackCombo", attackCombo);
                    followMouse = true;
                    comboWaitingTimer = 0;
                    //followMouseTimer = 0;
                    Player.Instance.SetAllMovementActive(false);
                }
            }
            else
            {
                attackCombo = 0;
                comboWaitingTimer = 0;
                myAnim.SetBool("ExitRecovery", true);
                //playerMovement.StopMoving = false;
                //GetComponent<PlayerDash>().enabled = true;
                Player.Instance.SetAllMovementActive(true);
            }
        }

        //if (attackCombo > 0)
        //{
        //    comboWaitingTimer += Time.deltaTime;

        //    if (comboWaitingTimer > comboWaitingLength || attackCombo >= 4)
        //    {
        //        attackCombo = 0;
        //        comboWaitingTimer = 0;
        //    }
        //}
    }

    void Range(Range r)
    {
        //if (Input.GetMouseButton(0) && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Hold & Click"))
        //{
        //    myAnim.SetFloat("ItemAttackID", info.GetAnimationID());
        //    myAnim.Play("Hold & Click");
        //    followMouse = true;
        //    playerMovement.StopMoving = true;
        //}

        if(Input.GetMouseButton(0))
        {
            followMouse = true;
            //playerMovement.StopMoving = true;
            //GetComponent<PlayerDash>().enabled = false;
            Player.Instance.SetMovementActive(false);

            if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Range")) //Hold & Click
            {
                myAnim.SetFloat("ItemAttackID", r.animationID);
                myAnim.Play("Range");
                myAnim.SetBool("ExitRecovery", false);
            }
        }

        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("RangeRecover") && Input.GetMouseButtonUp(0) || !myAnim.GetCurrentAnimatorStateInfo(0).IsName("RangeRecover") && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Range"))
        {
            //playerMovement.StopMoving = false;
            //GetComponent<PlayerDash>().enabled = true;
            Player.Instance.SetMovementActive(true);

            myAnim.SetBool("ExitRecovery", true);
            //Debug.Log("HEGJ");

        }
           

    }

    void Consumable(Consumable c)
    {
        if (Input.GetMouseButton(0))
        {
            if(consumeTimer == 0)
            {
                myAnim.Play("Consuming");
            }

            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Consuming"))
            {
                followMouse = true;
                //playerMovement.StopMoving = true;
                //GetComponent<PlayerDash>().enabled = false;
                Player.Instance.SetMovementActive(false);

                consumeTimer += Time.deltaTime;

                if(consumeTimer > c.consumeTimeRate)
                {
                    myAnim.SetBool("DoneConsuming", true);
                }
            }
            else if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("ConsumingRecover"))
            {
                myAnim.SetBool("DoneConsuming", false);
                //playerMovement.StopMoving = false;
                Player.Instance.SetMovementActive(true);
                GetComponent<PlayerHealth>().DecreaseHealthWith((int)-c.healthIncrease);
                c.DecreaseDurability();
                consumeTimer = 0;
            }

            if (Input.GetMouseButtonUp(0) && consumeTimer > 0 && consumeTimer < c.consumeTimeRate)
            {
                //myAnim.SetBool("DoneConsuming", false);
                //playerMovement.StopMoving = false;
                Player.Instance.SetMovementActive(true);
                consumeTimer = 0;

            }
        }
    }

    void CharacterFollowMouse()
    {
        int x = (int)MouseDatabase.CalculateDirectionNonDisplay(MouseDatabase.mousePosition, transform).x;
        int y = (int)MouseDatabase.CalculateDirectionNonDisplay(MouseDatabase.mousePosition, transform).y;

        if (followMouse == true)
        {
            //movementDatabase.SetAnim(new Vector2(x, y));
            myAnim.SetFloat("Horizontal", x);
            myAnim.SetFloat("Vertical", y);

            followMouseTimer = 0;
            followMouse = false;
        }

        if (followMouseTimer >= 0) //&& movementDatabase.GetMoving() == 0
        {
            followMouseTimer += Time.deltaTime;
            if (followMouseTimer > followMouseRate)
            {
                followMouseTimer = -1;
            }
            else if(Player.Instance.GetAllMovementActive() == true)
            {
                myAnim.SetFloat("Horizontal", x);
                myAnim.SetFloat("Vertical", y);
            }
            //else if (playerMovement.StopMoving == false)
            //{
            //    //movementDatabase.SetAnim(new Vector2(x, y));
            //}
        }
    }
}