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
        set {
            OnPrimaryChanged?.Invoke(primary, value);
            primary = value;
        }
    }
    int secondary;
    public int Secondary
    {
        get => secondary;
        set
        {
            OnSecondaryChanged?.Invoke(secondary, value);
            secondary = value;
        }
    }

    public delegate void OnChangedEquipment(int oldEQ, int newEQ);
    public event OnChangedEquipment OnPrimaryChanged;
    public event OnChangedEquipment OnSecondaryChanged;

    Selected selectedEQ = Selected.Primary;
    public Selected SelectedEQ
    {
        get => selectedEQ;
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedEQ = Selected.Primary;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedEQ = Selected.Secondary;
        }
        
        if (selectedEQ == Selected.Primary)
        {

        }
        else if(selectedEQ == Selected.Secondary)
        {

        }
    }
}