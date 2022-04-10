using System;
using System.Windows.Forms;

namespace Lab_4._1
{
    public partial class Form1 : Form
    {
        MyStorage storage;
        PaintBox _paintBox;
        public Form1()
        {
            InitializeComponent();
            storage = new MyStorage(100);//N = 10
            _paintBox = new PaintBox(paintBox.Width, paintBox.Height);
            paintBox.Image = _paintBox.GetBitmap();
        }

        private void paintBox_MouseDown(object sender, MouseEventArgs e)
        {
            storage.ActionObject(e.X, e.Y, _paintBox.GetGraphics(), e.Button);
            UpdateForm();
        }

        private void UpdateForm()
        {
            _paintBox.ClearBox();
            for (int i = 0; i < storage.getCount(); i++)
            {
                if (storage.checkObject(i))
                {
                    storage.getObject(i).Draw();
                }
            }
            paintBox.Image = _paintBox.GetBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
