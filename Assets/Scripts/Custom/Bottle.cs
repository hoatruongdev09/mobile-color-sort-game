using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Bottle : MonoBehaviour {
    public int MaxColor { get; set; } = 4;
    public List<BottleColor> Colors {
        get => colors;
        set => colors = value;
    }

    [SerializeField] private List<BottleColor> colors;
    [SerializeField] private List<Image> imgColors;
    [SerializeField] private RectTransform colorHolder;
    [SerializeField] private Image prefabImageColor;

    // private void Update () {
    //     if (Input.GetKeyUp (KeyCode.A)) {
    //         PourIn (new BottleColor { Color = new Color32 (255, 255, 255, 255), Count = 1 });
    //     }
    //     if (Input.GetKeyUp (KeyCode.R)) {
    //         PourOut ();
    //     }
    // }
    public void SetInitColor (List<BottleColor> colors) {
        Colors = colors;
        foreach (var color in Colors) {
            var imgColor = Instantiate (prefabImageColor, colorHolder);
            imgColor.rectTransform.localScale = new Vector3 (1, color.Count, 1);
            imgColor.color = color.Color;
            imgColors.Add (imgColor);
        }
    }
    public BottleColor GetTopColor () {
        if (Colors.Count == 0) { throw new Exception ("Bottle is empty"); }
        return Colors.Last ();
    }
    public void PourIn (BottleColor color, Action<BottleColor, BottleColor> callback = null) {
        if (Colors.Count == 0) {
            AddColor (color, callback);
            return;
        }
        var topColor = Colors.Last ();
        if (!CompareColor (topColor.Color, color.Color)) { throw new Exception ("Colors are not the same"); }
        var totalColor = Colors.Select (col => col.Count).Sum ();
        if (totalColor + color.Count > MaxColor) { throw new Exception ("Bottle is full"); }
        topColor.Count += color.Count;
        AnimateAddColor (imgColors.Last (), color.Count, color.Count * .3f).setOnComplete (() => { callback?.Invoke (topColor, color); });
    }
    public void PourOut () {
        if (Colors.Count == 0) { return; }
        var topColor = Colors.Last ();
        Colors.Remove (topColor);
        AnimateRemoveColor (imgColors.Last (), topColor.Count * .3f).setOnComplete (() => {
            var imageLast = imgColors.Last ();
            imgColors.Remove (imageLast);
            Destroy (imageLast.gameObject);
        });
    }
    public void AddColor (BottleColor color, Action<BottleColor, BottleColor> callback = null) {
        Colors.Add (color);
        var imgColor = Instantiate (prefabImageColor, colorHolder);
        imgColor.rectTransform.localScale = new Vector3 (1, 0, 1);
        imgColor.color = color.Color;
        imgColors.Add (imgColor);
        AnimateAddColor (imgColors.Last (), color.Count, color.Count * .3f).setOnComplete (() => { callback?.Invoke (color, color); });
    }
    private bool CompareColor (Color color1, Color color2) {
        return ColorUtility.ToHtmlStringRGB (color1) == ColorUtility.ToHtmlStringRGB (color2);
    }
    private LTDescr AnimateAddColor (Image imgColor, int number, float time = .3f) {
        var nextSize = imgColor.rectTransform.localScale + new Vector3 (0, number);
        return imgColor.rectTransform.LeanScale (nextSize, time);
    }
    private LTDescr AnimateRemoveColor (Image imgColor, float time = .3f) {
        return imgColor.rectTransform.LeanScale (new Vector3 (1, 0), time);
    }

}