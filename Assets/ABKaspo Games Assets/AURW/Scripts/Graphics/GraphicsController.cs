using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ABKaspo.assets.AURW.Examples
{
    public class GraphicsController : MonoBehaviour
    {
        [Header("Settings")]
        public Material material;
        [Header("Sliders")]
        public Slider SmoothnessSlider;
        public Slider normalMapStrentghSlider;
        public Slider DepthSlider;
        public Slider DepthStrengthSlider;
        public Slider FresnelSlider;
        [Header("Texts")]
        public TextMeshProUGUI smoothnessValue;
        public TextMeshProUGUI normalMapStrentghValue;
        public TextMeshProUGUI depthValue;
        public TextMeshProUGUI depthStrengthValue;
        public TextMeshProUGUI fresnelValue;
        void Start()
        {

        }
        void Update()
        {
            // materal. setfloat[change a float value of a shader](Value reference, to value);
            material.SetFloat("_Smoothness", SmoothnessSlider.value / 100);
            material.SetFloat("_Depth", DepthSlider.value / 100);
            material.SetFloat("_Depth_Strength", DepthStrengthSlider.value / 100);
            material.SetFloat("_Fresnel_Power", FresnelSlider.value / 100);
            material.SetFloat("_Normal_Strength", normalMapStrentghSlider.value / 4);
            // TMPtext.text = material.GetFloat(Value Reference).ToString();
            smoothnessValue.text = material.GetFloat("_Smoothness").ToString();
            normalMapStrentghValue.text = material.GetFloat("_Normal_Strength").ToString();
            depthStrengthValue.text = material.GetFloat("_Depth_Strength").ToString();
            depthValue.text = material.GetFloat("_Depth").ToString();
            fresnelValue.text = material.GetFloat("_Fresnel_Power").ToString();
        }
        public void Reset()
        {
            SmoothnessSlider.value = 95;
            normalMapStrentghSlider.value = 3;
            DepthSlider.value = 3;
            DepthStrengthSlider.value = 100;
            FresnelSlider.value = 100;
        }
    }
}