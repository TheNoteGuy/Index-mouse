using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

namespace UnityEngine.XR.OpenXR.Samples.ControllerSample
{
    public class ActionToVector2 : ActionToControl
    {



        [SerializeField] public RectTransform _handle = null;

        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateHandle(ctx);

        protected override void OnActionStarted(InputAction.CallbackContext ctx) => UpdateHandle(ctx);

        protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateHandle(ctx);


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        static extern Vector2 SetCursorPos(int x, int y);

        [DllImport("user32.dll")]

        static extern bool GetCursorPos(out POINT lpPoint);

        private POINT cursorPos;


        private void UpdateHandle(InputAction.CallbackContext ctx)
        {

            // Get the position of the VR controller (in Unity units)
            Vector2 controllerPosition = (ctx.ReadValue<Vector2>() * 20f);
            //controllerPosition.x = Mathf.Pow(controllerPosition.x, 3) * 20f;
            //controllerPosition.y = Mathf.Pow(controllerPosition.y, 3) * 20f;
            GetCursorPos(out cursorPos);
            

            int outx = (cursorPos.x + ((int)controllerPosition.x));
            int outy = (cursorPos.y - ((int)controllerPosition.y));

            //Debug.Log("x : " + cursorPos.x);
            //Debug.Log("y : " + cursorPos.y);
            // Set the position of the mouse cursor
            SetCursorPos(outx, outy);

        }
    }
}
