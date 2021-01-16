using Capgemini.Xrm.DataMigration.XrmToolBox.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms
{
    /// <summary>
    /// Implementation of FilterEditor.
    /// </summary>
    public partial class FilterEditor : Form
    {
        private static readonly Color HcNode = Color.Firebrick;
        private static readonly Color HcString = Color.Blue;
        private static readonly Color HcAttribute = Color.Red;
        private static readonly Color HcComment = Color.GreenYellow;
        private static readonly Color HcInnerText = Color.Black;

        public FilterEditor(string currentfilter)
        {
            InitializeComponent();
            Filter = currentfilter;
        }

        public string QueryString { get; set; }

        public string Filter
        {
            get
            {
                return txtFilter.Text.Trim();
            }
            set
            {
                txtFilter.Text = value;
                HighlightRTF();
            }
        }

        private void HighlightRTF()
        {
            var selStart = txtFilter.SelectionStart;
            var selLength = txtFilter.SelectionLength;

            int k = 0;

            string str = txtFilter.Text;

            int st, en;
            int lasten = -1;
            while (k < str.Length)
            {
                st = str.IndexOf('<', k);

                if (st < 0)
                {
                    break;
                }

                if (lasten > 0)
                {
                    txtFilter.Select(lasten + 1, st - lasten - 1);
                    txtFilter.SelectionColor = HcInnerText;
                }

                en = str.IndexOf('>', st + 1);
                if (en < 0)
                {
                    break;
                }

                k = en + 1;
                lasten = en;

                if (str[st + 1] == '!')
                {
                    txtFilter.Select(st + 1, en - st - 1);
                    txtFilter.SelectionColor = HcComment;
                    continue;
                }

                string nodeText = str.Substring(st + 1, en - st - 1);

                bool inString = false;

                int lastSt = -1;
                FilterEditorState state = FilterEditorState.BeforeNodeName;

                int startNodeName = 0, startAtt = 0;
                ProcessEachNodeText(st, nodeText, ref inString, ref lastSt, ref state, ref startNodeName, ref startAtt);

                if (state == FilterEditorState.InNodeName)
                {
                    txtFilter.Select(st + 1, nodeText.Length);
                    txtFilter.SelectionColor = HcNode;
                }
            }

            // reset selection
            txtFilter.Select(selStart, selLength);
        }

        private void ProcessEachNodeText(int st, string nodeText, ref bool inString, ref int lastSt, ref FilterEditorState state, ref int startNodeName, ref int startAtt)
        {
            for (int i = 0; i < nodeText.Length; ++i)
            {
                if (nodeText[i] == '"')
                {
                    inString = !inString;
                }

                if (inString && nodeText[i] == '"')
                {
                    lastSt = i;
                }
                else if (nodeText[i] == '"')
                {
                    txtFilter.Select(lastSt + st + 2, i - lastSt - 1);
                    txtFilter.SelectionColor = HcString;
                }

                ProcessState(st, nodeText, inString, ref state, ref startNodeName, ref startAtt, i);
            }
        }

        private void ProcessState(int st, string nodeText, bool inString, ref FilterEditorState state, ref int startNodeName, ref int startAtt, int i)
        {
            switch (state)
            {
                case FilterEditorState.BeforeNodeName:
                    if (!char.IsWhiteSpace(nodeText, i))
                    {
                        startNodeName = i;
                        state = FilterEditorState.InNodeName;
                    }

                    break;

                case FilterEditorState.InNodeName:
                    if (char.IsWhiteSpace(nodeText, i))
                    {
                        txtFilter.Select(startNodeName + st, i - startNodeName + 1);
                        txtFilter.SelectionColor = HcNode;
                        state = FilterEditorState.AfterNodeName;
                    }

                    break;

                case FilterEditorState.AfterNodeName:
                    if (!char.IsWhiteSpace(nodeText, i))
                    {
                        startAtt = i;
                        state = FilterEditorState.InAttribute;
                    }

                    break;

                case FilterEditorState.InAttribute:
                    if (char.IsWhiteSpace(nodeText, i) || nodeText[i] == '=')
                    {
                        txtFilter.Select(startAtt + st, i - startAtt + 1);
                        txtFilter.SelectionColor = HcAttribute;
                        state = FilterEditorState.InString;
                    }

                    break;

                case FilterEditorState.InString:
                    if (nodeText[i] == '"' && !inString)
                    {
                        state = FilterEditorState.AfterNodeName;
                    }

                    break;
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBoxFilterTextChanged(object sender, EventArgs e)
        {
            HighlightRTF();
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            QueryString = txtFilter.Text;
            Close();
        }
    }
}