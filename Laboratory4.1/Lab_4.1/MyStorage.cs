using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_4._1
{
    class AObject
    {
        public virtual bool CheckPos(int x, int y, MouseButtons mouseButtons) {
            return false;
        }
        public virtual bool GetUsed()
        {
            return false;
        }
        public virtual bool GetSelected()
        {
            return false;
        }
        public virtual void SetSelected() { }
        public virtual void Draw() { }
    }
    class MyStorage
    {
        int count;
        AObject[] objects;
        public MyStorage(int count)
        {
            this.count = count;
            objects = new AObject[count];
        }
        public int getCount()
        {
            return count;
        }
        public void setObject(AObject _object)
        {
            for(int i = 0;i < count; i++)
            {
                if(objects[i] == null)
                {
                    objects[i] = _object;
                    return;
                }
            }
        }
        public AObject getObject(int index)
        {
            return objects[index];
        }
        public void delObject(int index)
        {
            objects[index] = null;
        }
        public void ActionObject(int x, int y, Graphics graphics,MouseButtons mouseButtons)
        {
            for (int i = 0; i < count; i++)
            {
                if (CheckAndUpdate(x, y, mouseButtons)) return;
            }
            if (mouseButtons == MouseButtons.Left)
                setObject(new CCicle(x, y, graphics));
            else
            {
                DelUsed();
            }
        }
        private void DelUsed()
        {
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (objects[i] == null) continue;
                if (objects[i].GetUsed())
                {
                    if (objects[i].GetSelected())
                    {
                        for (int j = 0; j < count; j++)
                        {
                            if (objects[j] != null && j != i)
                            {
                                objects[j].SetSelected();
                                break;
                            }
                        }
                    }
                    delObject(i);
                    flag = true;
                }
            }
            if (!flag)
            {
                for (int i = 0; i < count; i++)
                {
                    if (objects[i] == null) continue;
                    if (objects[i].GetSelected())
                    {
                        for (int j = 0; j < count; j++)
                        {
                            if (objects[j] != null && j != i)
                            {
                                objects[j].SetSelected();
                                break;
                            }
                        }
                        delObject(i);
                        return;
                    }
                }
            }
        }
        private bool CheckAndUpdate(int x, int y, MouseButtons mouseButtons)
        {
            bool tmp = false;
            for (int i = 0; i < count; i++)
            {
                if (objects[i] == null) continue;
                if (objects[i].CheckPos(x, y, mouseButtons))
                {
                    tmp = true;
                    continue;
                }
            }
            return tmp;
        }
        public bool checkObject(int index)
        {
            if(index >= 0 && index < count)
            {
                return objects[index] != null;
            }
            return false;
        }
    }

    class CCicle : AObject
    {
        int x,y,r = 30;
        Graphics graphics;
        Pen pen;
        Brush brush;
        bool selected;
        bool used;
        
        public CCicle(int x, int y, Graphics graphics)
        {
            this.x = x;
            this.y = y;
            this.graphics = graphics;
            pen = new Pen(Color.Black);
            pen.Width = 2;
            brush = Brushes.White;
            selected = true;
            used = false;
            Draw();
        }
        public override bool CheckPos(int x,int y, MouseButtons mouseButtons)
        {
            if (Math.Sqrt(Math.Pow((this.x - x), 2) + Math.Pow((this.y - y), 2)) <= r*2) {
                if (mouseButtons == MouseButtons.Left)
                    selected = true;
                else
                    used = !used;
                return true;
            }
            else
            {
                if(mouseButtons == MouseButtons.Left)
                    selected = false;
                return false;
            }
        }
        public override void Draw()
        {
            if (used)
                brush = Brushes.Yellow;
            else
                brush = Brushes.White;

            if (selected)
                pen.Color = Color.Green;
            else
                pen.Color = Color.Black;

            DrawCicle();
        }
        public override bool GetUsed()
        {
            return used;
        }
        public override bool GetSelected()
        {
            return selected;
        }
        public override void SetSelected()
        {
            selected = true;
        }
        private void DrawCicle()
        {
            graphics.FillEllipse(brush, (x - r), (y - r), 2 * r, 2 * r);
            graphics.DrawEllipse(pen, (x - r), (y - r), 2 * r, 2 * r);
        }
    }

    class PaintBox
    {
        Graphics graphics;
        Bitmap bitmap;
        public PaintBox(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
            ClearBox();
        }
        public Graphics GetGraphics()
        {
            return graphics;
        }
        public Bitmap GetBitmap()
        {
            return bitmap;
        }
        public void ClearBox()
        {
            graphics.Clear(Color.White);
        }
    }
}
