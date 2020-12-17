using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelGame : UIView {
    public string TextLevel {
        get => textLevel.text;
        set => textLevel.text = value;
    }

    [SerializeField] private Text textLevel;
    [SerializeField] private RectTransform bottleHolder;
}