using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.Content.ControlGallery.ControlTemplates
{
    public partial class ButtonControlTemplate : Frame, IControlTemplate
    {
        
        
        public ButtonControlTemplate()
        {
            InitializeComponent();

            TargetControl = targetControl;
        }

        public View TargetControl { get; set; }
    }
}
