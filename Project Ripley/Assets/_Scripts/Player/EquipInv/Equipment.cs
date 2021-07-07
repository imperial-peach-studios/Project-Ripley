using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment Instance;
    public enum Selected { Primary, Secondary }

    int primary;
    int secondary;
    public int Primary
    {
        get => primary;
        set
        {
            var old = primary;
            primary = value;
            OnPrimaryChanged?.Invoke(old, value);
        }
    }
    public int Secondary
    {
        get => secondary;
        set
        {
            var old = secondary;
            secondary = value;
            OnSecondaryChanged?.Invoke(old, value);
        }
    }

    public delegate void OnChangedEquipment(int oldEQ, int newEQ);
    public event OnChangedEquipment OnPrimaryChanged;
    public event OnChangedEquipment OnSecondaryChanged;

    public delegate void OnSelectedChanged(Selected newSelected);
    public event OnSelectedChanged OnSelectedHasChanged;

    [SerializeField] private Selected _selectedEQ;
    public Selected SelectedEQ
    {
        get => _selectedEQ;
        private set
        {
            _selectedEQ = value;
            OnSelectedHasChanged?.Invoke(value);
            Player.Instance.inventory.SetSelected(value);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            // DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(gameObject);
        }

        //Player.Instance.inventory.OnInventoryChanged += OnInvetoryChanged;

        Primary = 1;
        Secondary = 2;
        //GameData.OnSavePlayer += OnSaveEquipmentData;
        //GameData.OnLoadPlayer += OnLoadEquipmentData;
    }

    void OnSaveEquipmentData()
    {
        //GameData.aData.pData.SaveEquipmentData(primary, secondary, (int)_selectedEQ);
    }

    void OnLoadEquipmentData()
    {
        int p = 0;
        int s = 0;
        int se = 0;

        //GameData.aData.pData.LoadEquipmentData(ref p, ref s, ref se);

        Primary = p;
        Secondary = s;
        SelectedEQ = (Selected)se;
    }

    void Start()
    {
        SelectedEQ = Selected.Primary;
        Primary = 0;
        Secondary = 1;
    }

    void OnInvetoryChanged(int slot)
    {
        if (Primary == slot)
        {
            OnPrimaryChanged?.Invoke(slot, slot);
        }
        else if (Secondary == slot)
        {
            OnSecondaryChanged?.Invoke(slot, slot);
        }
    }

    public void SwapEquipment()
    {
        int temp = Primary;

        Primary = Secondary;
        Secondary = temp;
    }

    public Item GetSelectedItem()
    {
        return Player.Instance.inventory.GetItem();
    }

    public bool CanDoAction()
    {
        return Player.Instance.inventory.GetItem() != null ? true : false;
    }

    public int GetCurrentSelectedIndex()
    {
        if (_selectedEQ == Selected.Primary)
            return primary;
        else if (_selectedEQ == Selected.Secondary)
            return secondary;

        return -1;
    }

    public int GetCurrentIndex()
    {
        if (_selectedEQ == Selected.Primary)
            return primary;
        else if (_selectedEQ == Selected.Secondary)
            return secondary;

        return -1;
    }

    void Update()
    {
        if (Player.Instance.CanChangeState(PlayerState.Walking))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //if (InventoryUI.IsMouseOverUI())
                //{
                //    int slotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

                //    if (slotIndex != Secondary)
                //    {
                //        Primary = slotIndex;
                //    }
                //    else
                //    {
                //        SwapEquipment();
                //    }
                //}

                SelectedEQ = Selected.Primary;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //if (InventoryUI.IsMouseOverUI())
                //{
                //    int slotIndex = InventoryUI.GetCurrentMouseOverSlotIndex();

                //    if (slotIndex != Primary)
                //    {
                //        Secondary = slotIndex;
                //    }
                //    else
                //    {
                //        SwapEquipment();
                //    }
                //}

                SelectedEQ = Selected.Secondary;
            }
        }
    }
}