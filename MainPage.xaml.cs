using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
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

        public MainPage()
        {
            this.InitializeComponent();
            CoolNameList.ItemsSource = coolNames;
            coolNames.Add(new CoolName("How to use?", "Just enter text in the box above 😄"));
        }

        private void TextBoxMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            // coolNames.Clear();
            if(TextBoxMain.Text == "")
            {
                coolNames.Clear();
                coolNames.Add(new CoolName("How to use?", "Just enter text in the box above 😄"));
            }
            else
            {
                coolService.CoolifyAsync(TextBoxMain.Text, coolNames);
                // System.Diagnostics.Debug.WriteLine(coolNames.Count);
            }
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

        private void CoolTextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            CreateOrUpdateSpringAnimation(1.1f);
            (sender as UIElement).StartAnimation(_springAnimation);
        }

        private void RichTextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            CreateOrUpdateSpringAnimation(1.0f);
            (sender as UIElement).StartAnimation(_springAnimation);
        }
    }
}
