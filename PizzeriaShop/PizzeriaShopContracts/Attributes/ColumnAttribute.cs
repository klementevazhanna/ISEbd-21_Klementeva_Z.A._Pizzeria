using System;
using System.Windows.Forms;

namespace PizzeriaShopContracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string title = "", bool visible = true, int width = 0, GridViewAutoSize gridViewAutoSize = GridViewAutoSize.None, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.NotSet, string dateType = "")
        {
            Title = title;
            Visible = visible;
            Width = width;
            GridViewAutoSize = gridViewAutoSize;
            Alignment = alignment;
            DateType = dateType;
        }

        public string Title { get; private set; }

        public bool Visible { get; private set; }

        public int Width { get; private set; }

        public GridViewAutoSize GridViewAutoSize { get; private set; }

        public DataGridViewContentAlignment Alignment { get; private set; }
        public string DateType { get; private set; }
    }
}
