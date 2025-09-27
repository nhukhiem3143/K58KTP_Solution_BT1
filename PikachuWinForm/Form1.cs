using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PikachuWinForm
{
    public partial class Form1 : Form
    {
        private int rows = 15;
        private int cols = 20;
        private int cellSize = 30;
        private Random rand = new Random();

        private PictureBox[,] board; // Bàn cờ lưu pictureBox
        private int[,] map;          // Lưu index ảnh tại mỗi ô (-1 = trống)

        private PictureBox firstPick = null;
        private PictureBox secondPick = null;

        private Image[] images =
        {
            Properties.Resources.pika1,
            Properties.Resources.pika2,
            Properties.Resources.pika3,
            Properties.Resources.pika4,
            Properties.Resources.pika5,
            Properties.Resources.pika6,
            Properties.Resources.pika7,
            Properties.Resources.pika8,
            Properties.Resources.pika9,
            Properties.Resources.pika10,
            Properties.Resources.pika11,
            Properties.Resources.pika12,
            Properties.Resources.pika13,
            Properties.Resources.pika14,
            Properties.Resources.pika15,
            Properties.Resources.pika16,
            Properties.Resources.pika17,
            Properties.Resources.pika18,
            Properties.Resources.pika19,
            Properties.Resources.pika20,
            Properties.Resources.pika21,
            Properties.Resources.pika22,
            Properties.Resources.pika23,
            Properties.Resources.pika24,
            Properties.Resources.pika25,
        };

        public Form1()
        {
            InitializeComponent();
        }

        // Nút Bắt đầu
        private void buttonStart_Click(object sender, EventArgs e)
        {
            GenerateBoard();
        }

        // Nút Thoát
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ===== Sinh bàn cờ =====
        private void GenerateBoard()
        {
            panelBoard.Controls.Clear();
            board = new PictureBox[rows, cols];
            map = new int[rows, cols];

            // Tạo list index ảnh (đảm bảo số lượng chẵn)
            List<int> imageIndexes = new List<int>();
            int totalCells = rows * cols;
            for (int i = 0; i < totalCells / 2; i++)
            {
                int imgIndex = rand.Next(images.Length);
                imageIndexes.Add(imgIndex);
                imageIndexes.Add(imgIndex);
            }

            // Xáo trộn
            for (int i = imageIndexes.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int tmp = imageIndexes[i];
                imageIndexes[i] = imageIndexes[j];
                imageIndexes[j] = tmp;
            }

            // Đặt vào board
            int k = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int imgIndex = imageIndexes[k++];
                    map[r, c] = imgIndex;

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(cellSize - 2, cellSize - 2);
                    pic.Location = new Point(c * cellSize, r * cellSize);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.Image = images[imgIndex];
                    pic.Tag = new Point(r, c);

                    pic.Click += Pic_Click;

                    panelBoard.Controls.Add(pic);
                    board[r, c] = pic;
                }
            }
        }

        // ===== Click chọn ảnh =====
        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox clicked = sender as PictureBox;
            if (clicked.Image == null) return;

            if (firstPick == null)
            {
                firstPick = clicked;
                firstPick.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (secondPick == null && clicked != firstPick)
            {
                secondPick = clicked;
                secondPick.BorderStyle = BorderStyle.Fixed3D;

                Point p1 = (Point)firstPick.Tag;
                Point p2 = (Point)secondPick.Tag;

                if (map[p1.X, p1.Y] == map[p2.X, p2.Y] && CanConnect(p1, p2))
                {
                    // Xóa nếu hợp lệ
                    map[p1.X, p1.Y] = -1;
                    map[p2.X, p2.Y] = -1;
                    firstPick.Image = null;
                    secondPick.Image = null;
                }
                else
                {
                    // Reset viền
                    firstPick.BorderStyle = BorderStyle.FixedSingle;
                    secondPick.BorderStyle = BorderStyle.FixedSingle;
                }

                firstPick = null;
                secondPick = null;
            }
        }

        // ===== Kiểm tra nối 2 ô bằng <= 3 đoạn thẳng =====
        private bool CanConnect(Point a, Point b)
        {
            // 1. Cùng hàng
            if (a.X == b.X && ClearRow(a, b)) return true;
            // 2. Cùng cột
            if (a.Y == b.Y && ClearCol(a, b)) return true;

            // 3. L nối (qua 1 góc)
            Point corner1 = new Point(a.X, b.Y);
            Point corner2 = new Point(b.X, a.Y);

            if (IsEmpty(corner1) && ClearRow(a, corner1) && ClearCol(corner1, b))
                return true;
            if (IsEmpty(corner2) && ClearCol(a, corner2) && ClearRow(corner2, b))
                return true;

            // 4. 2 góc (Z hoặc U)
            // Quét theo hàng
            for (int r = 0; r < rows; r++)
            {
                Point pa = new Point(r, a.Y);
                Point pb = new Point(r, b.Y);
                if (IsEmpty(pa) && IsEmpty(pb) &&
                    ClearCol(a, pa) && ClearRow(pa, pb) && ClearCol(pb, b))
                    return true;
            }

            // Quét theo cột
            for (int c = 0; c < cols; c++)
            {
                Point pa = new Point(a.X, c);
                Point pb = new Point(b.X, c);
                if (IsEmpty(pa) && IsEmpty(pb) &&
                    ClearRow(a, pa) && ClearCol(pa, pb) && ClearRow(pb, b))
                    return true;
            }

            return false;
        }

        private bool IsEmpty(Point p)
        {
            if (p.X < 0 || p.X >= rows || p.Y < 0 || p.Y >= cols) return false;
            return map[p.X, p.Y] == -1;
        }

        private bool ClearRow(Point a, Point b)
        {
            if (a.Y > b.Y) { Point t = a; a = b; b = t; }
            for (int y = a.Y + 1; y < b.Y; y++)
            {
                if (map[a.X, y] != -1) return false;
            }
            return true;
        }

        private bool ClearCol(Point a, Point b)
        {
            if (a.X > b.X) { Point t = a; a = b; b = t; }
            for (int x = a.X + 1; x < b.X; x++)
            {
                if (map[x, a.Y] != -1) return false;
            }
            return true;
        }

        private void labelTime_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thời gian sẽ hiển thị ở đây sau khi viết logic chơi!");
        }

        private void labelScore_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Điểm sẽ hiển thị ở đây sau khi viết logic chơi!");
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đây là pictureBox mặc định trong Form.");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // You can add your desired logic here, for now just show a message box
            MessageBox.Show("Logo clicked!");
        }
    }
}
