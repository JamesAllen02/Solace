using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour, IPointerEnterHandler
{
    private inventorySelector selector;
    [SerializeField] private int thisValue;
    [SerializeField] private string lore;
    [SerializeField] private string itemName;

    // Start is called before the first frame update
    void Start()
    {
        selector = FindObjectOfType<inventorySelector>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selector.selected = thisValue;
        selector.currentDescription = lore;
        selector.currentItem = itemName;
    }
}
