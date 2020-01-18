using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using TenEnv.Core;

namespace TenApp.DWMTools
{
    static class DWMScreenshotTool
    {
        public static Form SourceForm;

        public static void Initialize()
        {
            SourceForm = new Form();
            SourceForm.Show();

            TenAppSpace.TenAppLoader.Hook.KeyboardPressed += Hook_KeyboardPressed;   
        }

        private static void Hook_KeyboardPressed(object sender, TenEnv.Core.GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp &&
                e.Key == Key.PrintScreen &&
                Keyboard.IsKeyDown(Key.LeftAlt))
            {
                if (DwmApi.CheckAeroEnabled())
                {
                    var foreground = User32.GetActiveWindow();
                    Debugger.Log(0, "0", $"Foreground = {foreground.ToString()}\r\n");

                    if(foreground != IntPtr.Zero)
                    {
                        IntPtr thumb = IntPtr.Zero;

                        int result = DwmApi.DwmRegisterThumbnail(SourceForm.Handle, foreground, out thumb);

                        Debugger.Log(0, "0", $"Thumb = {thumb.ToString()}, DwmRegisterThumbnail result = {result.ToString()}\r\n");

                        if (result == 0)
                        {
                            DwmApi.PSIZE size;
                            DwmApi.DwmQueryThumbnailSourceSize(thumb, out size);

                            DwmApi.DWM_THUMBNAIL_PROPERTIES props = new DwmApi.DWM_THUMBNAIL_PROPERTIES();
                            props.dwFlags = DwmApi.DWM_TNP_VISIBLE | DwmApi.DWM_TNP_RECTDESTINATION | DwmApi.DWM_TNP_OPACITY;

                            props.fVisible = true;
                            props.opacity = 1;

                            //props.rcDestination = new DwmApi.Rect(image.Left, image.Top, image.Right, image.Bottom);
                            //if (size.x < image.Width)
                            //    props.rcDestination.Right = props.rcDestination.Left + size.x;
                            //if (size.y < image.Height)
                            //    props.rcDestination.Bottom = props.rcDestination.Top + size.y;

                            DwmApi.DwmUpdateThumbnailProperties(thumb, ref props);
                        }

                        if (thumb != IntPtr.Zero)
                            DwmApi.DwmUnregisterThumbnail(thumb);

                        e.Handled = true;
                    }

                }
            }
        }

        public static void Stop()
        {
            SourceForm.Dispose();
            TenAppSpace.TenAppLoader.Hook.KeyboardPressed -= Hook_KeyboardPressed;
        }
    }
}
