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
            this.Filter = currentfilter;
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
                HighlightRTF(txtFilter);
            }
        }

        private static void HighlightRTF(RichTextBox rtb)
        {
            var selStart = rtb.SelectionStart;
            var selLength = rtb.SelectionLength;

            int k = 0;

            string str = rtb.Text;

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
                    rtb.Select(lasten + 1, st - lasten - 1);
                    rtb.SelectionColor = HcInnerText;
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
                    rtb.Select(st + 1, en - st - 1);
                    rtb.SelectionColor = HcComment;
                    continue;
                }

                string nodeText = str.Substring(st + 1, en - st - 1);

                bool inString = false;

                int lastSt = -1;
                int state = 0;
                /* 0 = before node name
                 * 1 = in node name
                   2 = after node name
                   3 = in attribute
                   4 = in string
                   */
                int startNodeName = 0, startAtt = 0;
                ProcessEachNodeText(rtb, st, nodeText, ref inString, ref lastSt, ref state, ref startNodeName, ref startAtt);

                if (state == 1)
                {
                    rtb.Select(st + 1, nodeText.Length);
                    rtb.SelectionColor = HcNode;
                }
            }

            // reset selection
            rtb.Select(selStart, selLength);
        }

        private static void ProcessEachNodeText(RichTextBox rtb, int st, string nodeText, ref bool inString, ref int lastSt, ref int state, ref int startNodeName, ref int startAtt)
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
                    rtb.Select(lastSt + st + 2, i - lastSt - 1);
                    rtb.SelectionColor = HcString;
                }

                ProcessState(rtb, st, nodeText, inString, ref state, ref startNodeName, ref startAtt, i);
            }
        }

        private static void ProcessState(RichTextBox rtb, int st, string nodeText, bool inString, ref int state, ref int startNodeName, ref int startAtt, int i)
        {
            switch (state)
            {
                case 0:
                    if (!char.IsWhiteSpace(nodeText, i))
                    {
                        startNodeName = i;
                        state = 1;
                    }

                    break;

                case 1:
                    if (char.IsWhiteSpace(nodeText, i))
                    {
                        rtb.Select(startNodeName + st, i - startNodeName + 1);
                        rtb.SelectionColor = HcNode;
                        state = 2;
                    }

                    break;

                case 2:
                    if (!char.IsWhiteSpace(nodeText, i))
                    {
                        startAtt = i;
                        state = 3;
                    }

                    break;

                case 3:
                    if (char.IsWhiteSpace(nodeText, i) || nodeText[i] == '=')
                    {
                        rtb.Select(startAtt + st, i - startAtt + 1);
                        rtb.SelectionColor = HcAttribute;
                        state = 4;
                    }

                    break;

                case 4:
                    if (nodeText[i] == '"' && !inString)
                    {
                        state = 2;
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
            HighlightRTF(txtFilter);
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            QueryString = txtFilter.Text;
            Close();
        }
    }
}