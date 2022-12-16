using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private EquipSystem _equipSystem;
    private Inventory _inventory;

    [SerializeField] private GameObject dropdownParent;
    [SerializeField] private TMP_Dropdown dropdown;
    
    
    [SerializeField]private GameObject UIObject;
    [SerializeField]private GameObject ItemHolder;

    //[SerializeField] private List<GameObject> itemholders;
    public int currentItem;
    public GameObject itemOutline;
    
    [Serializable] public struct ItemHolders
    {
        public Item itemreference;
        public Button Button;
        public Itemholder Itemholder;
    }

    [SerializeField] private Color[] _colors;
    [SerializeField] public ItemHolders[] _itemholders;
    
    //public List<Button> buttons = new List<Button>();
    private readonly KeyCode[] _inputList = new KeyCode[]
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
        KeyCode.Minus,
        KeyCode.Equals

    };

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _equipSystem = FindObjectOfType<EquipSystem>();
        currentItem = 0;
        UpdateOutline();
    }

    private void Update()
    {
        InputUpdate();
    }

    private void InputUpdate()
    {
        if (!Input.anyKeyDown)
            return;

        foreach (var key in _inputList)
            if (Input.GetKeyDown(key))
                switch (key)
                {
                    case KeyCode.Alpha1:
                        currentItem = 0;
                        break;
                    case KeyCode.Alpha2:
                        currentItem = 1;
                        break;
                    case KeyCode.Alpha3:
                        currentItem = 2;
                        break;
                    case KeyCode.Alpha4:
                        currentItem = 3;
                        break;
                    case KeyCode.Alpha5:
                        currentItem = 4;
                        break;
                    case KeyCode.Alpha6:
                        currentItem = 5;
                        break;
                    case KeyCode.Alpha7:
                        currentItem = 6;
                        break;
                    case KeyCode.Alpha8:
                        currentItem = 7;
                        break;
                    case KeyCode.Alpha9:
                        currentItem = 8;
                        break;
                    case KeyCode.Alpha0:
                        currentItem = 9;
                        break;
                    case KeyCode.Minus:
                        currentItem = 10;
                        break;
                    case KeyCode.Equals:
                        currentItem = 11;
                        break;
                }
        UpdateOutline();
    }
    public void ButtonPressed(Button targetButton)
    {
        if (!Array.Exists(_itemholders, Button => targetButton)) return;
        var index = Array.FindIndex(_itemholders, s => s.Button);
        currentItem = index;
        UpdateOutline();
    }
    public void UpdateOutline()
    {
        itemOutline.transform.position = _itemholders[currentItem].Button.transform.position;
    }
    public void SwitchDropdown()
    {
        dropdown.RefreshShownValue();
        dropdownParent.SetActive(!dropdownParent.activeInHierarchy);
    }
    public void Equip()
    {
        SwitchDropdown();
        _equipSystem.Equip(currentItem, dropdown.value);
    }
    
    public void CreateItemUI(Item targetItem)
    {
        var l = _itemholders.Length;
        for (var i = 0; i < l; i++)
        {
            var script = _itemholders[i].Itemholder;
            if (script.imageholder.sprite != null) continue;
            script.itemtext.text = targetItem.name;
            script.imageholder.sprite = targetItem.Icon;
            _itemholders[i].itemreference = targetItem;
            break;
        }
    }
    public void RemoveItemUI(int index)
    {
        var s = _itemholders[index].Itemholder;
        s.imageholder.sprite = null;
        s.itemtext.text = null;
        _itemholders[index].itemreference= null;
    }

    public void SetItemHolderColor(int itemindex,int bodytype)
    {
        _itemholders[itemindex].Itemholder.Background.color = _colors[bodytype];
    }
}
