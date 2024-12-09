using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABKaspo.assets.AURW.UnderWater
{
    public class Free_UnderWaterEffect : MonoBehaviour
    {
        public Material material;
        public float min = 0.0f;
        public float max = 4.0f;
        public float underWaterStrength = 1.0f;
        public Camera _camera;

        void Start()
        {

        }

        void Update()
        {
            float UnderWaterEffect = Mathf.Clamp(_camera.transform.position.y * (underWaterStrength * -1.0f), min, max);
            material.SetFloat("_Under_Water_Effect", UnderWaterEffect);
        }
    }
}