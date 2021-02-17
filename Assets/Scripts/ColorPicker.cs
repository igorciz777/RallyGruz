using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorPicker : MonoBehaviour
{
    [SerializeField]
    private Slider RSlider, GSlider, BSlider;
    [SerializeField]
    private Material[] carMaterials;
    private float r,g,b;
    private Color _color;

    private void Update() {
        r = RSlider.value;
        g = GSlider.value;
        b = BSlider.value;

        _color = new Color(r,g,b);

        foreach(Material carMaterial in carMaterials){
            carMaterial.SetColor("_Color", _color);
        }
    }
    private void Awake() {
        loadColor();
    }
    public void loadColor(){
        RSlider.value = PlayerPrefs.GetFloat("carColorR");
        GSlider.value = PlayerPrefs.GetFloat("carColorG");
        BSlider.value = PlayerPrefs.GetFloat("carColorB");
    }
    public void saveColor(){
        PlayerPrefs.SetFloat("carColorR", r);
        PlayerPrefs.SetFloat("carColorG", g);
        PlayerPrefs.SetFloat("carColorB", b);
    }
}
