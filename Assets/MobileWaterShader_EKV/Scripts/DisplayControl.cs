using System;
using UnityEngine;

namespace MobileWaterShaderEKV
{
    public class DisplayControl : MonoBehaviour
    {

        public Canvas mainCanvas, secondaryCanvas, thirdCanvas;
        public Camera mainCamera, secondaryCamera, thirdCamera;

        // Update is called once per frame
        public void ToggleCam(int c)
        {
            mainCanvas.gameObject.SetActive(false);
            secondaryCanvas.gameObject.SetActive(false);
            thirdCanvas.gameObject.SetActive(false);

            mainCamera.gameObject.SetActive(false);
            secondaryCamera.gameObject.SetActive(false);
            thirdCamera.gameObject.SetActive(false);

            switch (c)
            {
                case 0:
                    {
                        mainCanvas.gameObject.SetActive(true);
                        mainCamera.gameObject.SetActive(true);
                    }
                    break;

                case 1:
                    {
                        secondaryCanvas.gameObject.SetActive(true);
                        secondaryCamera.gameObject.SetActive(true);
                    }
                    break;

                case 2:
                    {
                        thirdCanvas.gameObject.SetActive(true);
                        thirdCamera.gameObject.SetActive(true);
                    }
                    break;

                default:
                    break;
            }

        }

    }
}
