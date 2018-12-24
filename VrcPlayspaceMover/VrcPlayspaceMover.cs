using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

using OvrInputOrig = LIADNAKJPHD;
using OvrInputControls = LIADNAKJPHD.NJNOBABHHJN;
using OvrInputControllers = LIADNAKJPHD.EHEKCPMBABB;


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

        // to find OVRInput class: find `OVRGamepad` string
        private static class OVRInput
        {
            // OVRInput.Get() -- find by searching `.None,` (returns: bool)
            public static bool GetTriggerButton(bool right = true) => OvrInputOrig.HFCPFKCLFCK(right ? OvrInputControls.One : OvrInputControls.Three); // if right get A otherwise get X

            // OVRInput.GetLocalControllerPosition -- find `().position` (returns: Vector3)
            public static Vector3 GetLocalControllerPosition(bool right = true) => OvrInputOrig.IOLOHLMNMHA(right ? OvrInputControllers.RTouch : OvrInputControllers.LTouch);
        }

        internal class Behaviour : MonoBehaviour
        {
            private void Update()
            {
                bool leftTrigger = OVRInput.GetTriggerButton(false);
                bool rightTrigger = OVRInput.GetTriggerButton();

                if (leftTrigger || rightTrigger)
                {
                    VRCVrCameraOculus[] ctrls = (VRCVrCameraOculus[])UnityEngine.Object.FindObjectsOfType(typeof(VRCVrCameraOculus));

                    if (leftTrigger)
                    {
                        ctrls[0].cameraLiftTransform.localPosition -= new Vector3(0, Time.deltaTime * 0.2f, 0);
                    }

                    if (rightTrigger)
                    {
                        ctrls[0].cameraLiftTransform.localPosition += new Vector3(0, Time.deltaTime * 0.2f, 0);
                    }
                }
            }
        }
    }
}
