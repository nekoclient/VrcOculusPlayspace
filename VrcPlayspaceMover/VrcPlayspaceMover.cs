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
        public static void Initialize()
        {
            new Thread(() =>
            {
                Thread.Sleep(7000);

                GameObject obj = new GameObject("VrcPlayspaceMover");
                obj.AddComponent<Behaviour>();
                UnityEngine.Object.DontDestroyOnLoad(obj);
            });
        }

        // to find OVRInput class: find `OVRGamepad` string
        private static class OVRInput
        {
            // OVRInput.Get() -- find by searching `.None,` (returns: bool)
            public static bool GetTriggerButton(bool right = true) => AEKOAMDGMGB.MHBKDOFNCBJ(right ? AEKOAMDGMGB.IPJJLPDNABN.One : AEKOAMDGMGB.IPJJLPDNABN.Three); // if right get A otherwise get X

            // OVRInput.GetLocalControllerPosition -- find `().position` (returns: Vector3)
            public static Vector3 GetLocalControllerPosition(bool right = true) => AEKOAMDGMGB.CJDJIBGLLOG(right ? AEKOAMDGMGB.GJHJJPDDIGG.RTouch : AEKOAMDGMGB.GJHJJPDDIGG.LTouch);
        }

        public class Behaviour : MonoBehaviour
        {
            void Update()
            {
                bool leftTrigger = OVRInput.GetTriggerButton(false);
                bool rightTrigger = OVRInput.GetTriggerButton();

                if (leftTrigger || rightTrigger)
                {
                    List<VRCVrCameraOculus> ctrls = ((VRCVrCameraOculus[])UnityEngine.Object.FindObjectsOfType(typeof(VRCVrCameraOculus))).ToList();

                    ctrls.ForEach(ctrl =>
                    {
                        if (leftTrigger)
                        {
                            ctrl.cameraLiftTransform.localPosition -= new Vector3(0, 0.005f, 0);
                        }

                        if (rightTrigger)
                        {
                            ctrl.cameraLiftTransform.localPosition += new Vector3(0, 0.005f, 0);
                        }
                    });
                }
            }
        }
    }
}
