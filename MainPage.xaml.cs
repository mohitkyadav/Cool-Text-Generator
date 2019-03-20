using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }

        private void TextBoxMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            coolNames.Add(new CoolName("Yo", "Lo"));
            coolService.CoolifyAsync(TextBoxMain.Text, coolNames);
            System.Diagnostics.Debug.WriteLine(coolNames.Count);
        }
    }
}
