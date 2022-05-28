using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventorySelector : MonoBehaviour
{
    public int selected;

    public string currentDescription;
    public string currentItem;

    [SerializeField] private TextMeshProUGUI loreText;
    [SerializeField] private TextMeshProUGUI nameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loreText.text = currentDescription;
        nameText.text = currentItem;
        // print(selected);
    }
}
