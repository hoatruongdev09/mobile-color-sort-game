using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerating : MonoBehaviour {
    public int maxBottleColorCount;
    public Color[] levelColors;

    public string outputJson;
    private Dictionary<string, int> colorDictionary;
    public void Start () {

        GenerateLevel ();
    }
    public void GenerateLevel () {
        colorDictionary = CreateColorDictionary (maxBottleColorCount, levelColors);
        var bottles = GenerateBottles ();
        var levelData = new LevelDataModel () { bottles = bottles };
        outputJson = JsonUtility.ToJson (levelData);

    }
    public BottleDataModel[] GenerateBottles () {
        var bottleModels = new BottleDataModel[maxBottleColorCount + 1];
        for (int i = 0; i < maxBottleColorCount; i++) {
            bottleModels[i] = GenerateABottle ();
        }
        bottleModels[maxBottleColorCount] = new BottleDataModel () { colors = new BottleColorDataModel[0], maxColor = maxBottleColorCount };
        return bottleModels;
    }
    private BottleDataModel GenerateABottle () {
        BottleDataModel model = new BottleDataModel ();
        List<BottleColorDataModel> listBottleColor = new List<BottleColorDataModel> ();
        int loopLimit = 50;
        while (listBottleColor.Select (record => record.count).Sum () < maxBottleColorCount) {
            if (loopLimit <= 0) { throw new System.Exception ("Stackoverflow Detected"); }
            loopLimit--;
            var colorData = RandomColorDataInDictionary (maxBottleColorCount - listBottleColor.Select (record => record.count).Sum ());
            if (colorData is null) {
                Debug.Log ($"Data is null");
                break;
            }
            if (listBottleColor.Count == 0) {
                listBottleColor.Add (new BottleColorDataModel () { hexColor = colorData.Key, count = colorData.Value });
                continue;
            }
            if (listBottleColor[listBottleColor.Count - 1].hexColor == colorData.Key) {
                listBottleColor[listBottleColor.Count - 1].count += colorData.Value;
                continue;
            }
            listBottleColor.Add (new BottleColorDataModel () { hexColor = colorData.Key, count = colorData.Value });
        }
        model.maxColor = maxBottleColorCount;
        model.colors = listBottleColor.ToArray ();
        return model;
    }
    private dynamic RandomColorDataInDictionary (int value) {
        var listColor = colorDictionary.ToArray ();
        var sum = listColor.Select (color => color.Value).Sum ();
        if (sum == 0) { return null; }
        var randomIndex = Random.Range (0, listColor.Length);
        while (listColor[randomIndex].Value == 0) {
            randomIndex = Random.Range (0, listColor.Length);
        }
        var randomValue = Random.Range (1, value);
        randomValue = randomValue < listColor[randomIndex].Value ? randomValue : listColor[randomIndex].Value;
        colorDictionary[listColor[randomIndex].Key] -= randomValue;
        return new { Key = listColor[randomIndex].Key, Value = randomValue };
    }
    private Dictionary<string, int> CreateColorDictionary (int maxBottleColorCount, Color[] levelColors) {
        var dictionary = new Dictionary<string, int> ();
        foreach (Color color in levelColors) {
            dictionary.Add (ColorUtility.ToHtmlStringRGBA (color), maxBottleColorCount);
        }
        return dictionary;
    }
}