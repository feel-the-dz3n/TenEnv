using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TenEnv.Core
{
    public static class Extensions
    {
        public static string[] ImageExtensions = { ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif", ".png" };
        public static string[] VideoExtensions = { ".mp4", ".avi", ".webm" };
        public static string[] MusicExtensions = { ".wav", ".wave", ".mp3" };

        public static int ContentFadeDuration = 150;

        public static bool CheckForEnding(this string str, string[] array)
        {
            foreach (var img in array)
            {
                if (str.ToLower().EndsWith(img.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public static UIElement FadeFromTo(this UIElement uiElement, double fromOpacity,
       double toOpacity, int durationInMilliseconds, bool loopAnimation,
       bool showOnStart, bool collapseOnFinish, int startAt = 0)
        {
            uiElement.Opacity = fromOpacity;

            var timeSpan = TimeSpan.FromMilliseconds(durationInMilliseconds);
            var doubleAnimation =
                  new DoubleAnimation(fromOpacity, toOpacity,
                                      new Duration(timeSpan));

            doubleAnimation.BeginTime = TimeSpan.FromMilliseconds(startAt);

            if (loopAnimation)
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            uiElement.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);

            if (showOnStart)
            {
                uiElement.ApplyAnimationClock(UIElement.VisibilityProperty, null);
                uiElement.Visibility = Visibility.Visible;
            }

            if (collapseOnFinish)
            {
                var keyAnimation = new ObjectAnimationUsingKeyFrames { Duration = new Duration(timeSpan) };
                keyAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromTimeSpan(timeSpan)));
                uiElement.BeginAnimation(UIElement.VisibilityProperty, keyAnimation);
            }

            return uiElement;
        }

        public static UIElement FadeIn(this UIElement uiElement, int durationInMilliseconds, int startAt = 0)
        {
            return uiElement.FadeFromTo(0, 1, durationInMilliseconds, false, true, false, startAt);
        }

        public static UIElement FadeOut(this UIElement uiElement, int durationInMilliseconds, int startAt = 0)
        {
            return uiElement.FadeFromTo(1, 0, durationInMilliseconds, false, false, true, startAt);
        }
    }
}
