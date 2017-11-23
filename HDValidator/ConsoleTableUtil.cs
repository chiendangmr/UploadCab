using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HDValidator
{
    public enum ColAlign { left, center, right }
    public class ConsoleTableUtil
    {
        int tableWidth = 12 * 9;
        int[] cols;
        ColAlign[] colsAlign;
        List<List<string>> headers;
        List<List<string>> rows;
        String text = "";
        RichTextBox rickTextBox;

        public ConsoleTableUtil(RichTextBox arickTextBox = null, List<List<string>> aheaders = null, List<List<string>> arows = null)
        {
            this.headers = aheaders;
            this.rows = arows;
            this.rickTextBox = arickTextBox;
        }

        public int Width
        {
            get { return tableWidth; }
            set { tableWidth = value; }
        }

        public int[] Cols
        {
            set
            {
                cols = value;
                int w = 0;
                foreach (int col in cols)
                {
                    w += col + 1;
                }
                if (w > 0) { Width = w; }
            }
        }

        public ColAlign[] ColsAlign
        {
            set
            {
                colsAlign = value;
            }
        }

        public string PrintTable()
        {
            text = "";
            if ((headers != null && headers.Count() > 0) || (rows != null && rows.Count() > 0))
            {
                PrintLine();
            }
            if (headers != null && headers.Count() > 0)
            {
                for (var i = 0; i < headers.Count(); i++)
                {
                    PrintRow(headers[i]);
                    PrintLine();
                }
            }

            if (rows != null)
            {
                for (var i = 0; i < rows.Count(); i++)
                {
                    PrintRow(rows[i]);
                }
                PrintLine();
            }
            return text;
        }

        string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        string AlignLeft(string text, int width, int align = 1)
        {
            text = text.Length > width ? text.Substring(0, width - 3 - align) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - align).PadLeft(width);
            }
        }

        public string PrintRow(List<string> columns)
        {
            string row = "|";
            if (cols == null || columns.Count < cols.Count())
            {
                int width = (tableWidth - columns.Count) / columns.Count;
                for (var i = 0; i < columns.Count; i++)
                {
                    string column = columns[i];
                    if (colsAlign != null && colsAlign.Length > i)
                    {
                        switch (colsAlign[i])
                        {
                            case ColAlign.left:
                                row += AlignLeft(column, width) + "|";
                                break;
                            default:
                                row += AlignCentre(column, width) + "|";
                                break;
                        }
                    }
                    else
                    {
                        row += AlignCentre(column, width) + "|";
                    }
                }
            }
            else
            {
                for (int i = 0; i < cols.Count(); i++)
                {
                    String colValue = columns.Count > i ? columns[i] : "";
                    if (colsAlign != null && colsAlign.Length > i)
                    {
                        switch (colsAlign[i])
                        {
                            case ColAlign.left:
                                row += AlignLeft(colValue, cols[i]) + "|";
                                break;
                            default:
                                row += AlignCentre(colValue, cols[i]) + "|";
                                break;
                        }
                    }
                    else
                    {
                        row += AlignCentre(colValue, cols[i]) + "|";
                    }
                }
            }
            if (rickTextBox != null)
            {
                rickTextBox.AppendText(row + "\n");
            }
            text += row + "\n";
            return row + "\n";
        }

        public string PrintLine()
        {
            string line = new string('-', tableWidth) + "\n";
            if (rickTextBox != null)
            {
                rickTextBox.AppendText(line);
            }
            text += line;
            return line;
        }

        public string PrintLine(String s)
        {
            string row = "|" + AlignCentre(s, tableWidth) + "|";
            if (rickTextBox != null)
            {
                rickTextBox.AppendText(row + "\n");
            }
            text += row + "\n";
            return row + "\n";
        }
    }
}
