using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Itemholder : MonoBehaviour
{
    [field: SerializeField] public Image Background { get; set; }
    [field: SerializeField]public TextMeshProUGUI itemtext { get; set; } 
    [field: SerializeField]public Image imageholder { get; set; }    
}
