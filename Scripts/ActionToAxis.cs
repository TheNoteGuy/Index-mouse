using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
using System;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSample
{
    public class ActionToAxis : ActionToControl
    {
        [Tooltip("Slider controlled by the action value")]
        [SerializeField] private Slider _slider = null;

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateValue(ctx);
        protected override void OnActionStarted(InputAction.CallbackContext ctx) => UpdateValue(ctx);
        protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        private void UpdateValue(InputAction.CallbackContext ctx) => _slider.value = ctx.ReadValue<float>();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private bool clicked = false;

        public struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]

        static extern bool GetCursorPos(out POINT lpPoint);

        private POINT cursorPos;
        private void Update()
        {
            if (_slider.value == 1)
            {
                if (clicked == false)
                {
                    GetCursorPos(out cursorPos);

                    int outx = (cursorPos.x);
                    int outy = (cursorPos.y);

                    uint X = (uint)outx;
                    uint Y = (uint)outy;
                    mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                    clicked = true;
                }
            }
            else
            {
                if (clicked == true)
                {
                    GetCursorPos(out cursorPos);

                    int outx = (cursorPos.x);
                    int outy = (cursorPos.y);

                    uint X = (uint)outx;
                    uint Y = (uint)outy;
                    mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                    clicked = false;
                }
            }
        }
    }
}
