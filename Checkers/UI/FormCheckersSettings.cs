using System;
using System.Windows.Forms;

namespace Ui
{
    public class FormCheckersSettings : Form
    {
        private readonly RadioButton r_RadioButton6X6;
        private readonly RadioButton r_RadioButton8X8;
        private readonly RadioButton r_RadioButton10x10;
        private readonly Label       r_LabelPlayers;
        private readonly Label       r_LabelPlayer1;
        private readonly Label       r_LabelBoardSize;
        private readonly TextBox     r_TextBoxPlayer1;
        private readonly TextBox     r_TextBoxPlayer2;
        private readonly CheckBox    r_CheckBoxPlayer2;
        private readonly Button      r_ButtonDone;
        private bool                 m_ClickedButtonDone;

        public FormCheckersSettings()
        {
            r_RadioButton6X6 = new RadioButton();
            r_RadioButton8X8 = new RadioButton();
            r_RadioButton10x10 = new RadioButton();
            r_LabelPlayers = new Label();
            r_LabelPlayer1 = new Label();
            r_LabelBoardSize = new Label();
            r_TextBoxPlayer1 = new TextBox();
            r_TextBoxPlayer2 = new TextBox();
            r_CheckBoxPlayer2 = new CheckBox();
            r_ButtonDone = new Button();
            this.m_ClickedButtonDone = false;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.r_LabelBoardSize.AutoSize = true;
            this.r_LabelBoardSize.Location = new System.Drawing.Point(12, 9);
            this.r_LabelBoardSize.Name = "r_LabelBoardSize";
            this.r_LabelBoardSize.Size = new System.Drawing.Size(81, 17);
            this.r_LabelBoardSize.TabIndex = 0;
            this.r_LabelBoardSize.Text = "Board Size:";
            this.r_RadioButton6X6.AutoSize = true;
            this.r_RadioButton6X6.Checked = true;
            this.r_RadioButton6X6.Location = new System.Drawing.Point(30, 43);
            this.r_RadioButton6X6.Name = "r_RadioButton6X6";
            this.r_RadioButton6X6.Size = new System.Drawing.Size(54, 21);
            this.r_RadioButton6X6.TabIndex = 1;
            this.r_RadioButton6X6.TabStop = true;
            this.r_RadioButton6X6.Text = "6X6";
            this.r_RadioButton6X6.UseVisualStyleBackColor = true;
            this.r_RadioButton8X8.AutoSize = true;
            this.r_RadioButton8X8.Location = new System.Drawing.Point(90, 43);
            this.r_RadioButton8X8.Name = "r_RadioButton8X8";
            this.r_RadioButton8X8.Size = new System.Drawing.Size(54, 21);
            this.r_RadioButton8X8.TabIndex = 2;
            this.r_RadioButton8X8.Text = "8X8";
            this.r_RadioButton8X8.UseVisualStyleBackColor = true;
            this.r_RadioButton10x10.AutoSize = true;
            this.r_RadioButton10x10.Location = new System.Drawing.Point(150, 43);
            this.r_RadioButton10x10.Name = "r_RadioButton10x10";
            this.r_RadioButton10x10.Size = new System.Drawing.Size(70, 21);
            this.r_RadioButton10x10.TabIndex = 3;
            this.r_RadioButton10x10.Text = "10X10";
            this.r_RadioButton10x10.UseVisualStyleBackColor = true;
            this.r_LabelPlayers.AutoSize = true;
            this.r_LabelPlayers.Location = new System.Drawing.Point(12, 83);
            this.r_LabelPlayers.Name = "r_LabelPlayers";
            this.r_LabelPlayers.Size = new System.Drawing.Size(59, 17);
            this.r_LabelPlayers.TabIndex = 4;
            this.r_LabelPlayers.Text = "Players:";
            this.r_LabelPlayer1.AutoSize = true;
            this.r_LabelPlayer1.Location = new System.Drawing.Point(25, 110);
            this.r_LabelPlayer1.Name = "r_LabelPlayer1";
            this.r_LabelPlayer1.Size = new System.Drawing.Size(64, 17);
            this.r_LabelPlayer1.TabIndex = 5;
            this.r_LabelPlayer1.Text = "Player 1:";
            this.r_TextBoxPlayer1.Location = new System.Drawing.Point(120, 105);
            this.r_TextBoxPlayer1.Name = "r_TextBoxPlayer1";
            this.r_TextBoxPlayer1.Size = new System.Drawing.Size(100, 22);
            this.r_TextBoxPlayer1.TabIndex = 7;
            this.r_TextBoxPlayer2.Enabled = false;
            this.r_TextBoxPlayer2.Location = new System.Drawing.Point(120, 138);
            this.r_TextBoxPlayer2.Name = "r_TextBoxPlayer2";
            this.r_TextBoxPlayer2.Size = new System.Drawing.Size(100, 22);
            this.r_TextBoxPlayer2.TabIndex = 8;
            this.r_TextBoxPlayer2.Text = "[Computer]";
            this.r_CheckBoxPlayer2.AutoSize = true;
            this.r_CheckBoxPlayer2.Location = new System.Drawing.Point(28, 140);
            this.r_CheckBoxPlayer2.Name = "r_CheckBoxPlayer2";
            this.r_CheckBoxPlayer2.Size = new System.Drawing.Size(86, 21);
            this.r_CheckBoxPlayer2.TabIndex = 9;
            this.r_CheckBoxPlayer2.Text = "Player 2:";
            this.r_CheckBoxPlayer2.UseVisualStyleBackColor = true;
            this.r_CheckBoxPlayer2.CheckStateChanged += new System.EventHandler(this.CheckBoxPlayer2_CheckStateChanged);
            this.r_ButtonDone.Location = new System.Drawing.Point(145, 169);
            this.r_ButtonDone.Name = "r_ButtonDone";
            this.r_ButtonDone.Size = new System.Drawing.Size(75, 23);
            this.r_ButtonDone.TabIndex = 10;
            this.r_ButtonDone.Text = "Done";
            this.r_ButtonDone.UseVisualStyleBackColor = true;
            this.r_ButtonDone.Click += new System.EventHandler(this.buttonDone_Click);
            this.ClientSize = new System.Drawing.Size(259, 197);
            this.Controls.Add(this.r_ButtonDone);
            this.Controls.Add(this.r_CheckBoxPlayer2);
            this.Controls.Add(this.r_TextBoxPlayer2);
            this.Controls.Add(this.r_TextBoxPlayer1);
            this.Controls.Add(this.r_LabelPlayer1);
            this.Controls.Add(this.r_LabelPlayers);
            this.Controls.Add(this.r_RadioButton10x10);
            this.Controls.Add(this.r_RadioButton8X8);
            this.Controls.Add(this.r_RadioButton6X6);
            this.Controls.Add(this.r_LabelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCheckersSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void buttonDone_Click(object i_Sender, EventArgs i_E)
        {
            if(this.r_TextBoxPlayer1.Text.Length == 0 || this.r_TextBoxPlayer2.Text.Length == 0)
            {
                MessageBox.Show("Please enter a name in all text boxes!", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                m_ClickedButtonDone = i_Sender == r_ButtonDone;
                this.Close();
            }
        }

        private void CheckBoxPlayer2_CheckStateChanged(object i_Sender, EventArgs i_E)
        {
            if((i_Sender as CheckBox).Checked)
            {
                this.r_TextBoxPlayer2.Enabled = true;
                this.r_TextBoxPlayer2.Text = "Player2";
            }
            else
            {
                this.r_TextBoxPlayer2.Text = "[Computer]";
                this.r_TextBoxPlayer2.Enabled = false;
            }
        }

        public bool ClosedByDoneButton
        {
            get
            {
                return m_ClickedButtonDone;
            }
        }

        public string Player1Name
        {
            get
            {
                return r_TextBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return r_TextBoxPlayer2.Text;
            }
        }

        public int BoardSize
        {
            get
            {
                int boardSize = 6;

                if(this.r_RadioButton6X6.Checked)
                {
                    boardSize = 6;
                }
                else if(this.r_RadioButton8X8.Checked)
                {
                    boardSize = 8;
                }
                else
                {
                    boardSize = 10;
                }

                return boardSize;
            }
        }

        public bool IsVsHuman
        {
            get
            {
                return r_CheckBoxPlayer2.Checked;
            }
        }
    }
}
