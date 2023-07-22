using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;


namespace UnityEngine.XR.OpenXR.Samples.ControllerSample
{
    public class ActionToVector2Trackpad : ActionToControl
    {

        [SerializeField] public RectTransform _handle = null;

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateHandle(ctx);

        protected override void OnActionStarted(InputAction.CallbackContext ctx) => UpdateHandle(ctx);

        protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateHandle(ctx);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_WHEEL = 0x0800;

        private void UpdateHandle(InputAction.CallbackContext ctx)
        {
            if (ctx.ReadValue<Vector2>().y >= 0.1) 
            {
               mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 50, 0);
            }

            if (ctx.ReadValue<Vector2>().y <= -0.1)
            {
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 4294967246, 0);
            }
        }
    }
}
