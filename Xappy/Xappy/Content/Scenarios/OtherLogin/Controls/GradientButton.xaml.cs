using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.OtherLogin.Controls
{
    public partial class GradientButton
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text",
            typeof(string),
            typeof(GradientButton),
            default(string));

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
            this.FadeTo(0.7, 100);
        }

        void Handle_Released(object sender, System.EventArgs e)
        {
            this.FadeTo(1, 200);
        }
    }
}
