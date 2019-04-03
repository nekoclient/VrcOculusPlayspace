using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;


namespace VrcPlayspaceMover
{
    public class VrcPlayspaceMover
    {
        private static GameObject m_mainObject;

        public static void Initialize()
        {
            new Thread(() =>
            {
                Thread.Sleep(7000);

                m_mainObject = new GameObject("VrcPlayspaceMover");
                m_mainObject.AddComponent<Behaviour>();

                UnityEngine.Object.DontDestroyOnLoad(m_mainObject);
            }).Start();
        }

        internal class Behaviour : MonoBehaviour
        {
            private void Update()
            {
                bool leftTrigger = OVRInput.Get(OVRInput.Button.Three);
                bool rightTrigger = OVRInput.Get(OVRInput.Button.One);

                if (leftTrigger || rightTrigger)
                {
                    VRCVrCameraOculus ctrl = ((VRCVrCameraOculus[])UnityEngine.Object.FindObjectsOfType(typeof(VRCVrCameraOculus)))[0];

                    if (leftTrigger)
                    {
                        ctrl.cameraLiftTransform.localPosition -= new Vector3(0, Time.deltaTime * 0.2f, 0);
                    }

                    if (rightTrigger)
                    {
                        ctrl.cameraLiftTransform.localPosition += new Vector3(0, Time.deltaTime * 0.2f, 0);
                    }
                }
            }
        }
    }
}
