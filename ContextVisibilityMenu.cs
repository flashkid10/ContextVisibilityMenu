using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AniLyst_5._0.CustomControls;
using System.Threading;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using AniLyst_5._0.ItemModel;

namespace AniLyst_5._0
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

        //private void GridViewColumnVisCheck(MenuItem CB)
        //{
        //    int i = CB.Tag.ToInt32();
        //    if (CB.IsChecked) GRD.Columns.Add(VisColInt[i]);
        //    else GRD.Columns.Remove(VisColInt[i]);

        //    List<GridViewColumn> Tem = new List<GridViewColumn>();
        //    foreach (GridViewColumn GVC in GRD.Columns) Tem.Add(GVC);
        //    foreach (GridViewColumn GVC in Tem) GRD.Columns.Remove(GVC);
        //    foreach (KeyValuePair<GridViewColumn, int> GVC in VisColGrid) if (Tem.Contains(GVC.Key)) GRD.Columns.Add(GVC.Key);
        //}

        //public  Dictionary<int, GridViewColumn> VisColInt = new Dictionary<int, GridViewColumn>();
        //public  Dictionary<GridViewColumn, int> VisColGrid = new Dictionary<GridViewColumn, int>();
    }
}