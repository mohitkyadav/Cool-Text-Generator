﻿using System;
using System.Collections.Generic;
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
        private List<CoolName> coolNames = new List<CoolName>();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TextBoxMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            coolService.CoolifyAsync(TextBoxMain.Text, coolNames);
            System.Diagnostics.Debug.WriteLine(coolNames.Count);
        }
    }
}
