// Form1.cs (partial)
using System;
using System.Windows.Forms;
using Pikachu;

public partial class Form1 : Form
{
    private PikachuGame game;

    public Form1()
    {
        InitializeComponent();
        game = new PikachuGame();
        game.SetBoard(PikachuGame.SampleBoard());
        RefreshBoardText();
    }

    private void RefreshBoardText()
    {
        rtbBoard.Text = game.BoardToString();
        lblMsg.Text = "Author: " + game.Author();
    }

    private void btnCheck_Click(object sender, EventArgs e)
    {
        try
        {
            int r1 = Convert.ToInt32(txtR1.Text);
            int c1 = Convert.ToInt32(txtC1.Text);
            int r2 = Convert.ToInt32(txtR2.Text);
            int c2 = Convert.ToInt32(txtC2.Text);

            bool can = game.CanConnect(r1, c1, r2, c2);
            lblMsg.Text = (can ? "Can connect! " : "Cannot connect. ") + game.LastMessage();
            if (can)
            {
                game.RemovePair(r1, c1, r2, c2);
                RefreshBoardText();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Invalid input: " + ex.Message);
        }
    }
}
