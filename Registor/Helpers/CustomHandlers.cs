using Microsoft.Maui.Handlers;

namespace Registor.Helpers;

public static class CustomHandlers
{
    public static void ConfigureHandlers()
    {
        EntryHandler.Mapper.AppendToMapping(nameof(IEntry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Background = new Android.Graphics.Drawables.ShapeDrawable();
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            handler.PlatformView.SetPadding(0, 0, 0, 0);
#endif
#if IOS || MACCATALYST
            handler.PlatformView.Layer.CornerRadius = 0;
            handler.PlatformView.Layer.BorderWidth = 1;
            handler.PlatformView.Layer.BorderColor = UIKit.UIColor.Black.CGColor;
            handler.PlatformView.ClipsToBounds = true;
#endif
#if WINDOWS
            handler.PlatformView.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(0);
#endif
        });

        CheckBoxHandler.Mapper.AppendToMapping(nameof(ICheckBox), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetButtonDrawable(null); // Видаляємо стандартну іконку
            handler.PlatformView.Background = new Android.Graphics.Drawables.ShapeDrawable();
#endif
#if IOS || MACCATALYST
            handler.PlatformView.Layer.CornerRadius = 0;
            handler.PlatformView.Layer.BorderWidth = 2;
            handler.PlatformView.Layer.BorderColor = UIKit.UIColor.Black.CGColor;
            handler.PlatformView.ClipsToBounds = true;
#endif
#if WINDOWS
            handler.PlatformView.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(0);
#endif
        });
    }
}
