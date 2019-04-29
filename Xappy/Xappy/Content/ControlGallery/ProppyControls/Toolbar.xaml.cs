using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public partial class Toolbar
    {
        
        public event EventHandler Reset;
        
        public event EventHandler Back;
        
        public event EventHandler Undo;
        
        public event EventHandler Redo;
        
        public event EventHandler ViewSource;
        
        public event EventHandler TogglePropertyPanel;
        
        public Toolbar()
        {
            InitializeComponent();
        }

        private void ImageButton_OnClicked(object sender, EventArgs e)
        {
            ViewSource?.Invoke(this,EventArgs.Empty);
        }

        private void UndoBtn_OnClicked(object sender, EventArgs e)
        {
            Undo?.Invoke(this,EventArgs.Empty);
        }

        private void RedoBtn_OnClicked(object sender, EventArgs e)
        {
            Redo?.Invoke(this,EventArgs.Empty);
        }

        private void ResetBtn_OnClicked(object sender, EventArgs e)
        {
            Reset?.Invoke(this,EventArgs.Empty);
        }

        private void BackBtn_OnClicked(object sender, EventArgs e)
        {
            Back?.Invoke(this,EventArgs.Empty);
            PropertyInfoContainer.IsVisible = false;
            PropertyNameLbl.Text = string.Empty;
            ControlNameLbl.IsVisible = true;
        }

        public void SetProperty(string name)
        {
            PropertyNameLbl.Text = name;
            PropertyInfoContainer.IsVisible = true;
            ControlNameLbl.IsVisible = false;
        }

        private void ToggleBtn_OnClicked(object sender, EventArgs e)
        {
            TogglePropertyPanel?.Invoke(this,EventArgs.Empty);
        }
    }
}
