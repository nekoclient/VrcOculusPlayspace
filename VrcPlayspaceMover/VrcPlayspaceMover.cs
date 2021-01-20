using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Diagnostics;
using MelonLoader;
using Object = UnityEngine.Object;
using UnhollowerRuntimeLib;

[assembly: MelonInfo(typeof(VrcPlayspaceMover.VrcPlayspaceMover),
        "VrcPlayspaceMover",
        "1.0.0",
        "avail",
        "https://github.com/nekoclient/VrcOculusPlayspace/releases")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace VrcPlayspaceMover
{
    public class VrcPlayspaceMover : MelonMod
    {
        private static Dictionary<OVRInput.Button, bool> ms_wasPressed = new Dictionary<OVRInput.Button, bool>()
        {
            { OVRInput.Button.Three, false },
            { OVRInput.Button.One, false }
        };

        private static bool IsKeyJustPressed(OVRInput.Button key)
        {
            if (!ms_wasPressed.ContainsKey(key))
            {
                ms_wasPressed.Add(key, false);
            }

            if (OVRInput.Get(key, OVRInput.Controller.Touch))
            {
                if (!ms_wasPressed[key])
                {
                    ms_wasPressed[key] = true;

                    return true;
                }
            }
            else
            {
                if (ms_wasPressed[key])
                {
                    ms_wasPressed[key] = false;
                }
            }

            return false;
        }

        private Vector3 m_startingOffset = new Vector3();

        public override void OnUpdate()
        {
            bool leftJustPressed = IsKeyJustPressed(OVRInput.Button.Three);
            bool rightJustPressed = IsKeyJustPressed(OVRInput.Button.One);

            if (leftJustPressed)
            {
                m_startingOffset = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            }

            if (rightJustPressed)
            {
                m_startingOffset = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            }

            bool leftTrigger = OVRInput.Get(OVRInput.Button.Three, OVRInput.Controller.Touch);
            bool rightTrigger = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Touch);

            if (leftTrigger || rightTrigger)
            {
                Object[] ctrls = Object.FindObjectsOfType(Il2CppType.Of<VRCVrCameraOculus>());

                VRCVrCameraOculus ctrl;

                if (ctrls.Length > 0)
                {
                    ctrl = ctrls[0].TryCast<VRCVrCameraOculus>();
                }
                else
                {
                    Trace.WriteLine("camera not found?");
                    return;
                }

                if (leftTrigger)
                {
                    Vector3 currentOffset = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                    Vector3 calculatedOffset = (currentOffset - m_startingOffset) * -1.0f;
                    m_startingOffset = currentOffset;

                    ctrl.cameraLiftTransform.localPosition += calculatedOffset;
                }

                if (rightTrigger)
                {
                    Vector3 currentOffset = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                    Vector3 calculatedOffset = (currentOffset - m_startingOffset) * -1.0f;
                    m_startingOffset = currentOffset;

                    ctrl.cameraLiftTransform.localPosition += calculatedOffset;
                }
            }
        }
    }
}
