using BiliBili3.Helper;
using BiliBili3.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BiliBili3
{
    public class PreSplashPage : Page
    {
        public PreSplashPage()
        {
            var layoutRoot = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            layoutRoot.Children.Add(
                new Image
                {
                    Source = new BitmapImage(
                        new Uri("ms-appx:///Assets/Splash.png")),
                    MaxWidth = 350
                });
            layoutRoot.Children.Add(
                new ProgressRing
                {
                    IsActive = true,
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(0, 5, 0, 5),
                    Foreground = new SolidColorBrush(Colors.Gray)
                });
            layoutRoot.Children.Add(
                new TextBlock
                {
                    Text = "富强，民主，文明，和谐",
                    Foreground = new SolidColorBrush(Colors.Gray),
                    HorizontalAlignment = HorizontalAlignment.Center
                });
            Content = layoutRoot;
        }

        internal async Task LoadAsync(LaunchActivatedEventArgs e)
        {
            await Task.Delay(100);

            Frame rootFrame = Window.Current.Content as Frame;
            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += ((App)App.Current).OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }
            }

            if (e.PrelaunchActivated)
            {
                return;
            }

            ApiHelper.access_key = SettingHelper.Get_Access_key();
            UserManage.access_key = SettingHelper.Get_Access_key();
            var par = new StartModel() { StartType = StartTypes.None };
            if (e.Arguments.Length != 0)
            {
                var d = e.Arguments.Split(',');
                if (d.Length > 1)
                {
                    if (d[0] == "bangumi")
                    {
                        par.StartType = StartTypes.Bangumi;
                        par.Par1 = d[1];
                    }
                    if (d[0] == "live")
                    {
                        par.StartType = StartTypes.Live;
                        par.Par1 = d[1];
                    }
                }
                else
                {
                    par.StartType = StartTypes.Video;
                    par.Par1 = e.Arguments;
                }
            }

            if (SettingHelper.Get_PlayerMode())
            {
                rootFrame.Navigate(typeof(PlayerModePage));
            }
            else
            {
                if (rootFrame.Content == null ||
                    rootFrame.Content is PreSplashPage)
                {
                    rootFrame.Navigate(typeof(SplashPage), par);
                }
                else
                {
                    if (par.StartType == StartTypes.Video)
                    {
                        MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(VideoViewPage), par.Par1);
                    }
                    if (par.StartType == StartTypes.Bangumi)
                    {
                        MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(BanInfoPage), par.Par1);
                    }
                    if (par.StartType == StartTypes.Live)
                    {
                        MessageCenter.SendNavigateTo(NavigateMode.Info, typeof(LiveRoomPage), par.Par1);
                    }
                }
            }

            // 将框架放在当前窗口中
            Window.Current.Content = rootFrame;
        } // End Function ' LoadAsync

    }
}
