using System;
using System.Collections;
using System.Text;

namespace Pikachu
{
    public class PikachuGame
    {
        private int[,] board;
        private int rows;
        private int cols;
        private string lastMessage = "";

        private string author = "Khiem";

        public string Author() => author;

        public string LastMessage() => lastMessage;

        public bool SetBoard(int[,] b)
        {
            if (b == null) return false;
            rows = b.GetLength(0);
            cols = b.GetLength(1);
            board = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    board[i, j] = b[i, j];
            lastMessage = $"Da tao ban choi ({rows}x{cols})";
            return true;
        }

        public int[,] GetBoard()
        {
            if (board == null) return null;
            int[,] copy = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    copy[i, j] = board[i, j];
            return copy;
        }

        public string BoardToString()
        {
            if (board == null) return "[ban trong]";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(board[i, j].ToString().PadLeft(2));
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public bool RemovePair(int r1, int c1, int r2, int c2)
        {
            if (!IsValidPos(r1, c1) || !IsValidPos(r2, c2))
            {
                lastMessage = "Toa do khong hop le";
                return false;
            }
            if (board[r1, c1] == 0 || board[r2, c2] == 0)
            {
                lastMessage = "Mot trong 2 o trong";
                return false;
            }
            if (board[r1, c1] != board[r2, c2])
            {
                lastMessage = "Hai o khac nhau";
                return false;
            }
            if (CanConnectInternal(r1, c1, r2, c2))
            {
                board[r1, c1] = 0;
                board[r2, c2] = 0;
                lastMessage = "Da xoa cap. by " + author;
                return true;
            }
            lastMessage = "Khong the noi duoc";
            return false;
        }

        public bool CanConnect(int r1, int c1, int r2, int c2)
        {
            if (!IsValidPos(r1, c1) || !IsValidPos(r2, c2)) return false;
            if (board[r1, c1] == 0 || board[r2, c2] == 0) return false;
            if (board[r1, c1] != board[r2, c2]) return false;
            return CanConnectInternal(r1, c1, r2, c2);
        }

        private bool IsValidPos(int r, int c)
        {
            if (board == null) return false;
            return (r >= 0 && r < rows && c >= 0 && c < cols);
        }

        private bool CanConnectInternal(int r1, int c1, int r2, int c2)
        {
            if (r1 == r2 && c1 == c2) return false;

            int R = rows + 2;
            int C = cols + 2;
            int[,] pad = new int[R, C];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    pad[i + 1, j + 1] = board[i, j];

            int sr = r1 + 1, sc = c1 + 1;
            int tr = r2 + 1, tc = c2 + 1;

            Queue q = new Queue();
            bool[,,] visited = new bool[R, C, 4];

            int[] dr = new int[] { -1, 0, 1, 0 };
            int[] dc = new int[] { 0, 1, 0, -1 };

            for (int d = 0; d < 4; d++)
            {
                int nr = sr + dr[d];
                int nc = sc + dc[d];
                if (nr < 0 || nr >= R || nc < 0 || nc >= C) continue;
                if (pad[nr, nc] == 0 || (nr == tr && nc == tc))
                {
                    q.Enqueue(new int[] { nr, nc, d, 0 }); // [r, c, dir, turns]
                    visited[nr, nc, d] = true;
                }
            }

            while (q.Count > 0)
            {
                int[] node = (int[])q.Dequeue();
                int r = node[0], c = node[1], dir = node[2], turns = node[3];
                if (r == tr && c == tc && turns <= 2) return true;

                int nr = r + dr[dir], nc = c + dc[dir];
                if (nr >= 0 && nr < R && nc >= 0 && nc < C)
                {
                    if (!visited[nr, nc, dir] && (pad[nr, nc] == 0 || (nr == tr && nc == tc)))
                    {
                        visited[nr, nc, dir] = true;
                        q.Enqueue(new int[] { nr, nc, dir, turns });
                    }
                }

                for (int turn = -1; turn <= 1; turn += 2)
                {
                    int nd = (dir + turn + 4) % 4;
                    if (turns + 1 > 2) continue;
                    int trr = r + dr[nd], tcc = c + dc[nd];
                    if (trr >= 0 && trr < R && tcc >= 0 && tcc < C)
                    {
                        if (!visited[trr, tcc, nd] && (pad[trr, tcc] == 0 || (trr == tr && tcc == tc)))
                        {
                            visited[trr, tcc, nd] = true;
                            q.Enqueue(new int[] { trr, tcc, nd, turns + 1 });
                        }
                    }
                }
            }
            return false;
        }

        public static int[,] SampleBoard()
        {
            return new int[6, 8] {
                {1,2,3,4,4,3,2,1},
                {5,6,7,8,8,7,6,5},
                {1,2,3,4,4,3,2,1},
                {5,6,7,8,8,7,6,5},
                {1,2,3,4,4,3,2,1},
                {5,6,7,8,8,7,6,5}
            };
        }
    }
}