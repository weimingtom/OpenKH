using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KH1_ImgParser
{
    public partial class fMain : Form
    {
        Kh.Img img;

        public fMain()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Kingdom Hearts 1 IMG files|*.img";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                img = new Kh.Img(openFileDialog.FileName);
                vScrollBarImage.Value = 0;
                vScrollBarImage.Maximum = img.Count - 1;
                SelectImgIndex(0);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TIM2 files|*.tm2";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                panelImage.BackgroundImage.Save(saveFileDialog.FileName);
            }
        }

        private void vScrollBarImage_Scroll(object sender, ScrollEventArgs e)
        {
            SelectImgIndex(vScrollBarImage.Value);
        }

        void SelectImgIndex(int index)
        {
            panelImage.BackgroundImage = new Kh.TM2(img.Get(index)).Image;
        }
    }
}
