using System;
using System.Drawing;
using System.Drawing.Text; 
using System.Windows.Forms;
using System.IO; 

namespace TextEditorApp 
{
    public partial class Form1 : Form
    {
        private string currentFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSystemFonts(); 
            LoadFontSizes();  

            cmbFont.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = "14";
            SetDefaultFont();
        }

        private void LoadSystemFonts()
        {
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            foreach (var font in installedFontCollection.Families)
            {
                cmbFont.Items.Add(font.Name);
            }
        }
        private void LoadFontSizes()
        {
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in sizes)
            {
                cmbSize.Items.Add(size.ToString());
            }
        }

        private void SetDefaultFont()
        {

            string fontName = "Tahoma";
            if (cmbFont.Items.Count > 0 && cmbFont.SelectedIndex < 0)
                fontName = cmbFont.Items[0].ToString();

            richText.Font = new Font(fontName, 14, FontStyle.Regular);
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            richText.Clear();
            currentFilePath = "";
            cmbFont.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = "14";
            SetDefaultFont();
            MessageBox.Show("Đã tạo văn bản mới!", "Thông báo");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openDlg.FileName;
                try
                {
                    if (currentFilePath.ToLower().EndsWith(".rtf"))
                        richText.LoadFile(currentFilePath, RichTextBoxStreamType.RichText);
                    else
                        richText.LoadFile(currentFilePath, RichTextBoxStreamType.PlainText);

                    MessageBox.Show("Mở file thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mở file: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Filter = "Rich Text Format (*.rtf)|*.rtf";

                if (saveDlg.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveDlg.FileName;
                    richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo");
                }
            }
            else
            {
                richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo");
            }
        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontChange();
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFontChange();
        }

        private void ApplyFontChange()
        {
            if (richText.SelectionFont != null && cmbFont.SelectedItem != null && cmbSize.SelectedItem != null)
            {
                try
                {
                    string fontName = cmbFont.SelectedItem.ToString();
                    float fontSize = float.Parse(cmbSize.SelectedItem.ToString());
                    FontStyle currentStyle = richText.SelectionFont.Style;

                    richText.SelectionFont = new Font(fontName, fontSize, currentStyle);
                }
                catch
                {
                    
                }
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        private void ToggleFontStyle(FontStyle style)
        {
            if (richText.SelectionFont != null)
            {
                Font currentFont = richText.SelectionFont;
                FontStyle newStyle = currentFont.Style ^ style;
                richText.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        private void mnuDinhDang_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;

            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;
            }
        }

        private void mnuMoi_Click(object sender, EventArgs e) => btnNew_Click(sender, e);
        private void mnuMo_Click(object sender, EventArgs e) => btnOpen_Click(sender, e);
        private void mnuLuu_Click(object sender, EventArgs e) => btnSave_Click(sender, e);
        private void mnuThoat_Click(object sender, EventArgs e) => Application.Exit();

        private void richText_TextChanged(object sender, EventArgs e)
        {
            string text = richText.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {

                if (lblStatus != null) lblStatus.Text = "Tổng số từ: 0";
            }
            else
            {
                string[] words = text.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (lblStatus != null) lblStatus.Text = "Tổng số từ: " + words.Length;
            }
        }
    }
}