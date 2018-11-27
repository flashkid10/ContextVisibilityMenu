using System.Windows;
using System.Windows.Controls;

namespace Project
{
    public static partial class Utils
    {
        public static ContextVisibilityMenu GetContextVisibilityMenu(this ListView LST)
        {
            if (LST.ContextMenu != null && LST.ContextMenu.Tag is ContextVisibilityMenu) return LST.ContextMenu.Tag as ContextVisibilityMenu;
            else return new ContextVisibilityMenu(LST);
        }
    }

    public class ContextVisibilityMenu
    {
        private ListView LST { get; set; }
        private GridView GRD { get; set; }

        private ContextMenu ContextMenu = new ContextMenu();
        public Trictionary<GridViewColumn, MenuItem, int> Columns = new Trictionary<GridViewColumn, MenuItem, int>();

        public ContextVisibilityMenu(ListView _LST)
        {
            _LST.ContextMenu = ContextMenu;
            ContextMenu.Tag = this;
            LST = _LST;
            GRD = LST.View as GridView;
            CheckColumens();
        }

        public void CheckColumens()
        {
            int Pointer = 0;
            foreach (GridViewColumn x in GRD.Columns)
            {
                if (!Columns.Keys.Contains(x))
                {
                    MenuItem MI = new MenuItem();
                    MI.Header = x.Header;
                    MI.IsChecked = MI.IsCheckable = true;
                    MI.Tag = x;

                    MI.Checked += MI_CheckedChanged;
                    MI.Unchecked += MI_CheckedChanged;

                    ContextMenu.Items.Add(MI);
                    Columns.Add(x, MI, Pointer);
                }
                Pointer++;
            }
        }

        private void MI_CheckedChanged(object sender, RoutedEventArgs e)
        {
            MenuItem MI = sender as MenuItem;
            GridViewColumn GVC = MI.Tag as GridViewColumn;
            if (MI.IsChecked) GRD.Columns.Insert(Columns[GVC].Control, GVC);
            else GRD.Columns.Remove(GVC);
        }

        public MenuItem GetMenuItem(GridViewColumn GVC)
        {
            return Columns[GVC].Value;
        }

    }
}
