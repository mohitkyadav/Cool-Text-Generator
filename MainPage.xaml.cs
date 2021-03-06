﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Cool_Text_Generator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CoolNameAPI coolService = new CoolNameAPI();
        private ObservableCollection<CoolName> coolNames = new ObservableCollection<CoolName>();
        private ObservableCollection<string> recentCoolNames = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();
            CoolNameList.ItemsSource = coolNames;
            RecentCoolNameList.ItemsSource = recentCoolNames;
            coolNames.Add(new CoolName("How to use?", "Just enter text in the box above 😄"));
            recentCoolNames.Add("Sample Text");
        }

        Compositor _compositor = Window.Current.Compositor;
        SpringVector3NaturalMotionAnimation _springAnimation;

        private void CreateOrUpdateSpringAnimation(float finalValue)
        {
            if (_springAnimation == null)
            {
                _springAnimation = _compositor.CreateSpringVector3Animation();
                _springAnimation.Target = "Scale";
            }

            _springAnimation.FinalValue = new System.Numerics.Vector3(finalValue);
        }
        
        // TODO => animaiton
        private void CoolTextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            CreateOrUpdateSpringAnimation(1.1f);
            (sender as UIElement).StartAnimation(_springAnimation);
        }
        
        // TODO => animaiton
        private void CoolTextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            CreateOrUpdateSpringAnimation(1.0f);
            (sender as UIElement).StartAnimation(_springAnimation);
        }

        private void DisplayProgressBar()
        {
            MainLoadingBar.Visibility = Visibility.Visible;
        }

        private void HideProgressBar()
        {
            MainLoadingBar.Visibility = Visibility.Collapsed;
        }

        private void CallCool(string s)
        {
            DisplayProgressBar();
            coolService.CoolifyAsync(s, coolNames, MainLoadingBar);
            foreach (var item in recentCoolNames)
            {
                if(item.ToString() == s)
                {
                    return;
                }
            }
            recentCoolNames.Add(s);
        }

        private void CoolButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxMain.Text == "")
            {
                coolNames.Clear();
                coolNames.Add(new CoolName("How to use?", "Just enter text in the box above 😄"));
            }
            else
            {
                CallCool(TextBoxMain.Text);
            }
        }

        private void CopyCoolName_Click(object sender, RoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(((Button)sender).Tag.ToString());
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
        }

        private void TextBoxMain_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString() == "Enter")
            {
                if (TextBoxMain.Text == "")
                {
                    coolNames.Clear();
                    coolNames.Add(new CoolName("How to use?", "Just enter text in the box above 😄"));
                }
                else
                {
                    CallCool(TextBoxMain.Text);
                }
            }
        }

        private void ReCool_Click(object sender, RoutedEventArgs e)
        {
            CallCool(((Button)sender).Tag.ToString());
        }

        private void RemoveAction_Click(object sender, RoutedEventArgs e)
        {
            recentCoolNames.Remove(((Button)sender).Tag.ToString());
        }

        private async void CoolNameToSteam_Click(object sender, RoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(((Button)sender).Tag.ToString());
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            try
            {
                await Windows.System.Launcher.LaunchUriAsync(new Uri(@"steam://url/SteamIDMyProfile"));
            }
            catch(Exception ex)
            {
                var messageDialog = new MessageDialog("Steam not found");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                await messageDialog.ShowAsync();
            }
        }
    }
}
