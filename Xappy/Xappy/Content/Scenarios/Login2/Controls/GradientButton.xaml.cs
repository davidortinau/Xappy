using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.Login2.Controls
{
    public partial class GradientButton : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(GradientButton), default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public GradientButton()
        {
            InitializeComponent();
        }

        void Handle_Pressed(object sender, System.EventArgs e)
        {
            this.FadeTo(0.7, 200);
        }

        void Handle_Released(object sender, System.EventArgs e)
        {
            this.FadeTo(1, 200);
        }
    }
}
