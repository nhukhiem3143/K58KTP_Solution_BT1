using System;
using System.Web;
using Pikachu;

namespace PikachuWebApp
{
    public partial class Api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.BufferOutput = true;

            try
            {
                string action = Request.Form["action"] ?? "";
                PikachuGame game = new PikachuGame();

                if (action == "generate")
                {
                    int rows = 20, cols = 30;
                    int totalCells = rows * cols;

                    if (totalCells % 2 != 0)
                    {
                        WriteJson(false, "⚠️ Tổng số ô phải là số chẵn");
                        return;
                    }

                    int maxPairs = 30;
                    int[,] board = new int[rows, cols];
                    Random rand = new Random();

                    // Danh sách vị trí
                    int[] positions = new int[totalCells];
                    for (int i = 0; i < totalCells; i++) positions[i] = i;
                    Array.Sort(positions, (a, b) => rand.Next(-1, 2));

                    // Gán cặp giá trị
                    for (int i = 0; i < maxPairs; i++)
                    {
                        int val = i + 1;
                        int idx1 = i * 2, idx2 = i * 2 + 1;
                        if (idx2 < totalCells)
                        {
                            int pos1 = positions[idx1];
                            int pos2 = positions[idx2];
                            board[pos1 / cols, pos1 % cols] = val;
                            board[pos2 / cols, pos2 % cols] = val;
                        }
                    }

                    // Ô còn trống thì random
                    for (int i = 0; i < rows; i++)
                        for (int j = 0; j < cols; j++)
                            if (board[i, j] == 0) board[i, j] = rand.Next(1, maxPairs + 1);

                    PikachuGame gameGen = new PikachuGame();
                    if (gameGen.SetBoard(board))
                    {
                        string result = SafeJson(gameGen.BoardToString());
                        Response.Write("{\"success\": true, \"message\": \"Bàn cờ tạo thành công\", \"newBoard\": \"" + result + "\", \"tacgia\": \"" + gameGen.Author() + "\"}");
                    }
                    else
                    {
                        WriteJson(false, "❌ Không thể tạo bàn cờ");
                    }
                }
                else if (action == "check")
                {
                    // Lấy tọa độ
                    int r1, c1, r2, c2;
                    if (!int.TryParse(Request.Form["r1"], out r1) ||
                        !int.TryParse(Request.Form["c1"], out c1) ||
                        !int.TryParse(Request.Form["r2"], out r2) ||
                        !int.TryParse(Request.Form["c2"], out c2))
                    {
                        WriteJson(false, "⚠️ Toạ độ không hợp lệ");
                        return;
                    }

                    // Lấy dữ liệu bàn cờ hiện tại
                    string boardStr = Request.Form["board"] ?? "";
                    boardStr = HttpUtility.UrlDecode(boardStr);

                    if (string.IsNullOrEmpty(boardStr))
                    {
                        WriteJson(false, "⚠️ Không có dữ liệu bàn cờ");
                        return;
                    }

                    PikachuGame gameCheck = new PikachuGame();
                    string[] lines = boardStr.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    int[,] currentBoard = new int[20, 30];
                    for (int i = 0; i < 20 && i < lines.Length; i++)
                    {
                        string[] cells = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < 30 && j < cells.Length; j++)
                            int.TryParse(cells[j], out currentBoard[i, j]);
                    }
                    gameCheck.SetBoard(currentBoard);

                    // Kiểm tra giá trị 2 ô
                    int val1 = gameCheck.GetBoard()[r1, c1];
                    int val2 = gameCheck.GetBoard()[r2, c2];
                    if (val1 != val2 || val1 == 0)
                    {
                        WriteJson(false, "❌ Hai ô không giống nhau hoặc đã rỗng");
                        return;
                    }

                    // Kiểm tra nối
                    if (gameCheck.CanConnect(r1, c1, r2, c2))
                    {
                        gameCheck.RemovePair(r1, c1, r2, c2);
                        string newBoard = SafeJson(gameCheck.BoardToString());
                        Response.Write("{\"success\": true, \"message\": \"✅ Ghép cặp thành công!\", \"newBoard\": \"" + newBoard + "\"}");
                    }
                    else
                    {
                        WriteJson(false, "🚫 Không thể nối 2 ô này (quá 3 đoạn thẳng hoặc bị chặn)");
                    }
                }
                else
                {
                    WriteJson(false, "⚠️ Hành động không hợp lệ");
                }
            }
            catch (Exception ex)
            {
                WriteJson(false, "🔥 Lỗi máy chủ: " + ex.Message);
            }

            Response.End();
        }

        // Escape JSON an toàn cho .NET 2.0
        private string SafeJson(string text)
        {
            if (text == null) return "";
            text = text.Replace("\\", "\\\\");
            text = text.Replace("\"", "\\\"");
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            text = text.Replace("\n", "\\n");
            return text.Trim();
        }

        private void WriteJson(bool success, string message)
        {
            string safeMsg = SafeJson(message);
            Response.Write("{\"success\": " + (success ? "true" : "false") + ", \"message\": \"" + safeMsg + "\"}");
        }
    }
}
