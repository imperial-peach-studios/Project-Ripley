using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum Selected { Primary, Secondary }

    public static Equipment Instance;
    int primary;
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
    int secondary;
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

    private Selected _selectedEQ;
    public Selected SelectedEQ
    {
        get => _selectedEQ;
        private set
        {
            _selectedEQ = value;
            OnSelectedHasChanged?.Invoke(value);
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

        Inventory.Instance.OnInventoryChanged += OnInvetoryChanged;
        GameData.OnSavePlayer += OnSaveEquipmentData;
        GameData.OnLoadPlayer += OnLoadEquipmentData;
    }

    void OnSaveEquipmentData()
    {
        GameData.aData.pData.SaveEquipmentData(primary, secondary, (int)_selectedEQ);
    }

    void OnLoadEquipmentData()
    {
        int p = 0;
        int s = 0;
        int se = 0;

        GameData.aData.pData.LoadEquipmentData(ref p, ref s, ref se);

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
        if(Primary == slot)
        {
            OnPrimaryChanged?.Invoke(slot, slot);
        }
        else if(Secondary == slot)
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

    public GameObject CurrentSelectedItem()
    {
        int currentIndex = -1;

        if(_selectedEQ == Selected.Primary)
            currentIndex = primary;
        else if(_selectedEQ == Selected.Secondary)
            currentIndex = secondary;

        return Inventory.Instance.GetInventorySlot(currentIndex);
    }
    public Items GetCurrentSelectedItems()
    {
        int currentIndex = -1;

        if (_selectedEQ == Selected.Primary)
            currentIndex = primary;
        else if (_selectedEQ == Selected.Secondary)
            currentIndex = secondary;

        return Inventory.Instance.GetItemInventorySlot(currentIndex);
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedEQ = Selected.Primary;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedEQ = Selected.Secondary;
        }
    }
}