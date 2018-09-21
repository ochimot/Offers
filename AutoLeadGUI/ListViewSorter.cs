// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.ListViewSorter
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System.Collections;
using System.Windows.Forms;

namespace AutoLeadGUI
{
  public class ListViewSorter : IComparer
  {
    private int Column = 0;
    private int LastColumn = 0;

    public int Compare(object o1, object o2)
    {
      if (!(o1 is ListViewItem) || !(o2 is ListViewItem))
        return 0;
      ListViewItem listViewItem = (ListViewItem) o2;
      string text1 = listViewItem.SubItems[this.ByColumn].Text;
      string text2 = ((ListViewItem) o1).SubItems[this.ByColumn].Text;
      int num = listViewItem.ListView.Sorting != SortOrder.Ascending ? string.Compare(text2, text1) : string.Compare(text1, text2);
      this.LastSort = this.ByColumn;
      return num;
    }

    public int ByColumn
    {
      get
      {
        return this.Column;
      }
      set
      {
        this.Column = value;
      }
    }

    public int LastSort
    {
      get
      {
        return this.LastColumn;
      }
      set
      {
        this.LastColumn = value;
      }
    }
  }
}
