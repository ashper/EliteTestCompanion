using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TestingUI.Objects;

namespace TestingUI
{
    /// <summary>
    /// Interaction logic for Bookmarks.xaml
    /// </summary>
    public partial class Bookmarks : Page
    {
        public Bookmarks()
        {
            InitializeComponent();
            BookmarkList = LoadBookMarksFromFile();
            BookmarkList.ListChanged += BookmarkList_ListChanged;
            bookmarksGrid.ItemsSource = BookmarkList;
        }

        public BindingList<Bookmark> BookmarkList { get; set; }

        private void BookmarkList_ListChanged(object sender, ListChangedEventArgs e)
        {
            File.WriteAllText(@"Bookmarks.json", JsonConvert.SerializeObject(BookmarkList));
        }

        private void bookmarksGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid.SelectedCells != null && grid.SelectedCells.Count == 1)
            {
                var selectedCell = grid.SelectedCells.First();
                DataGridCell dgc = (DataGridCell)selectedCell.Column.GetCellContent(selectedCell.Item).Parent;
                if (!dgc.IsMouseOver)
                {
                    dgc.IsSelected = false;
                }
            }
        }

        private void bookmarksGrid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                if (bookmarksGrid.SelectedCells.Count > 0)
                {
                    var item = bookmarksGrid.SelectedCells[0].Item;
                    var col = bookmarksGrid.SelectedCells[0].Column;
                    DataGridCell cell = (DataGridCell)col.GetCellContent(item).Parent;
                    var test = cell.IsEditing;
                    if (!cell.IsEditing)
                    {
                        e.Handled = true;
                        BindingList<Bookmark> items = (BindingList<Bookmark>)bookmarksGrid.ItemsSource;
                        items.Remove((Bookmark)item);
                    }
                }
            }
        }

        private BindingList<Bookmark> LoadBookMarksFromFile()
        {
            if (File.Exists(@"Bookmarks.json"))
            {
                BindingList<Bookmark> bookmarks = JsonConvert.DeserializeObject<BindingList<Bookmark>>(File.ReadAllText(@"Bookmarks.json"));
                return bookmarks;
            }
            else
            {
                return new BindingList<Bookmark>();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }
    }
}