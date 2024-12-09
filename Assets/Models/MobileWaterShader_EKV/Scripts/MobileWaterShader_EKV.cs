using UnityEngine;

namespace MobileWaterShaderEKV
{
    public class MobileWaterShader_EKV : MonoBehaviour
    {
        [Header("Access methods to shader parameters on the referenced material")]
        [Tooltip("Referenced material")]
        public Material shaderMaterial;

        #region LIGHTING

        
        public bool IsLightingEnabled()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("LIGHTING");
            else
                return false;
        }

        public void EnableLighting(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("LIGHTING");
                else
                    shaderMaterial.DisableKeyword("LIGHTING");

        }

        public float GetShininess()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_Shininess");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetShininess(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_Shininess", Mathf.Clamp(f, 0.01f, 2f));
        }

        public float GetBrightness()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_Brightness");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetBrightness(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_Brightness", Mathf.Clamp(f, 0.01f, 100f));
        }

        public float GetAttenuation()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_Attenuation");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetAttenuation(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_Attenuation", Mathf.Clamp(f, 0.001f, 1f));
        }
        #endregion

        # region COLOR
        public Color GetColor()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetColor("_Color");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return Color.black;
            }
        }

        public void SetColor(Color c)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetColor("_Color", c);
        }
        #endregion

        #region TEXTURES
        public bool IsTexturesEnabled()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("TEXTURES");
            else
                return false;
        }
        public void EnableTextures(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("TEXTURES");
                else
                    shaderMaterial.DisableKeyword("TEXTURES");
        }

        public Texture GetTextureA()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetTexture("_MainTex");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return null;
            }
        }

        public void SetTextureA(Texture t)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetTexture("_MainTex", t);
        }

        public float GetTextureARotation()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_MainTexRot");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetTextureARotation(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_MainTexRot", f);
        }

        public Texture GetTextureB()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetTexture("_DiffTex");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return null;
            }
        }

        public void SetTextureB(Texture t)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetTexture("_DiffTex", t);
        }

        public float GetTextureBRotation()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_DiffTex");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetTextureBRotation(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_DiffTex", f);
        }
        #endregion

        #region WAVES
        public bool IsWavesEnabled()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("WAVES");
            else
                return false;
        }
        public void EnableWaves(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("WAVES");
                else
                    shaderMaterial.DisableKeyword("WAVES");
        }

        public Texture GetTextureWaves()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetTexture("_DerivHeightMap");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return null;
            }
        }

        public void SetTextureWaves(Texture t)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetTexture("_DerivHeightMap", t);
        }

        public float GetWaveTiling()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_Tiling");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetWaveTiling(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_Tiling", f);
        }

        public float GetSpeed()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_Speed");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetSpeed(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_Speed", Mathf.Clamp(f, 0.001f, 1f));
        }

        public float GetFlowStrength()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FlowStrength");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetFlowStrength(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FlowStrength", Mathf.Clamp(f, -1f, 1f));
        }

        public float GetFlowOffset()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FlowOffset");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetFlowOffset(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FlowOffset", Mathf.Clamp(f, -1f, 1f));
        }

        public float GetHeightScale()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_HeightScale");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetHeightScale(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_HeightScale", Mathf.Clamp(f, -1f, 1f));
        }

        public float GetHeightScaleModulated()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_HeightScaleModulated");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetHeightScaleModulated(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_HeightScaleModulated", Mathf.Clamp(f, -5f, 5f));
        }
        #endregion

        #region CUBEMAP REFLECTION
        public bool IsReflectionEnabled()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("REFLECTION");
            else
                return false;
        }
        public void EnableReflection(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("REFLECTION");
                else
                    shaderMaterial.DisableKeyword("REFLECTION");
        }

        public Texture GetReflectionCubemap()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetTexture("_Cube");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return null;
            }
        }

        public void SetReflectionCubemap(Texture t)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetTexture("_Cube", t);
        }

        public float GetReflectionStrength()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_RefStrength");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetReflectionStrength(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_RefStrength", Mathf.Clamp01(f));
        }
        #endregion

        #region BLENDING MODE

        public int GetSourceMode()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetInt("SrcMode");

            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public int GetDestinationMode()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetInt("DstMode");

            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetSourceMode(int i)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetInt("SrcMode", Mathf.Clamp(i, 0, 9));
        }

        public void SetDestinationMode(int i)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetInt("DstMode", Mathf.Clamp(i, 0, 9));
        }

        public void EnableTransparency(bool b)
        {
            if (b)
            {
                SetSourceMode(5);
                SetDestinationMode(3);
            }
            else
            {
                SetSourceMode(4);
                SetDestinationMode(3);
            }

        }

        public bool IsTransparencyEnabled()
        {
            if ((GetSourceMode() == 5 || GetSourceMode() == 3) && GetDestinationMode() == 3)
                return true;
            else
                return false;
        }
        #endregion

        #region FOAM

        public bool IsFoamEnabled()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("FOAM");
            else
                return false;
        }
        public void EnableFoam(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("FOAM");
                else
                    shaderMaterial.DisableKeyword("FOAM");
        }

        public bool IsFoamInverted()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("INVERT");
            else
                return false;
        }
        public void InvertFoam(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("INVERT");
                else
                    shaderMaterial.DisableKeyword("INVERT");
        }

        public bool IsFoamSquare()
        {
            if (shaderMaterial != null)
                return shaderMaterial.IsKeywordEnabled("SQUARED");
            else
                return false;
        }
        public void SquareFoam(bool b)
        {
            if (shaderMaterial != null)
                if (b)
                    shaderMaterial.EnableKeyword("SQUARED");
                else
                    shaderMaterial.DisableKeyword("SQUARED");
        }

        public Texture GetFoamTexture()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetTexture("_FoamTex");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return null;
            }
        }

        public void SetFoamTexture(Texture t)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetTexture("_FoamTex", t);
        }

        public float GetFoamSpeed()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FoamSpeed");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetFoamSpeed(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FoamSpeed", Mathf.Clamp(f, 0f, 5f));
        }

        public float GetFoamIntensity()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FoamIntensity");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetFoamIntensity(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FoamIntensity", Mathf.Clamp(f, 0f, 100f));
        }

        public float GetFoamWidth()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FoamWidth");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetFoamWidth(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FoamWidth", Mathf.Clamp01(f));
        }

        public float GetFoamDir()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FoamDir");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return 0;
            }
        }

        public void SetFoamDir(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FoamDir", Mathf.Clamp(f, -1f, 1f));
        }

        public float GetTextureFoamRotation()
        {
            if (shaderMaterial != null)
                return shaderMaterial.GetFloat("_FoamTexRot");
            else
            {
                Debug.LogWarning("Trying to access an undefined material");
                return -99;
            }
        }

        public void SetTextureFoamRotation(float f)
        {
            if (shaderMaterial != null)
                shaderMaterial.SetFloat("_FoamTexRot", f);
        }

        #endregion
    }
}

