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

        internal class Behaviour : MonoBehaviour
        {
            private Vector3 m_startingOffset = new Vector3();

            private void Update()
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
                    Object[] ctrls = Object.FindObjectsOfType(VRCVrCameraOculus.Il2CppType);

                    VRCVrCameraOculus ctrl;

                    if (ctrls.Length > 0)
                    {
                        ctrl = ctrls[0].TryCast<VRCVrCameraOculus>();
                    }
                    else
                    {
                        Log.Debug("camera not found?");
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
}
