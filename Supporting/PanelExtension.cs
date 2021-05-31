using System.Windows.Forms;

namespace Cave_Adventure
{
    public static class PanelExtension
    {
        public static void ShowAndHide(this Panel panel)
        {
            panel.Show();
            //panel.Hide();
            //panel.ResumeLayout();
            //panel.Refresh();
            //panel.Update();
        }
    }
}