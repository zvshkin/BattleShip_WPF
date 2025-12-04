namespace BattleShip_WPF
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.turnIndicator = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.myBoardLabel = new System.Windows.Forms.Label();
            this.enemyBoardLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // turnIndicator
            // 
            this.turnIndicator.Dock = System.Windows.Forms.DockStyle.Top;
            this.turnIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.turnIndicator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.turnIndicator.Location = new System.Drawing.Point(0, 0);
            this.turnIndicator.Name = "turnIndicator";
            this.turnIndicator.Size = new System.Drawing.Size(1000, 96);
            this.turnIndicator.TabIndex = 3;
            this.turnIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.myBoardLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.enemyBoardLabel, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 96);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 500);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // myBoardLabel
            // 
            this.myBoardLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.myBoardLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.myBoardLabel.Location = new System.Drawing.Point(3, 4);
            this.myBoardLabel.Name = "myBoardLabel";
            this.myBoardLabel.Size = new System.Drawing.Size(344, 41);
            this.myBoardLabel.TabIndex = 0;
            this.myBoardLabel.Text = "Ваше Поле";
            this.myBoardLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enemyBoardLabel
            // 
            this.enemyBoardLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enemyBoardLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.enemyBoardLabel.Location = new System.Drawing.Point(653, 4);
            this.enemyBoardLabel.Name = "enemyBoardLabel";
            this.enemyBoardLabel.Size = new System.Drawing.Size(344, 41);
            this.enemyBoardLabel.TabIndex = 1;
            this.enemyBoardLabel.Text = "Поле Компьютера";
            this.enemyBoardLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1000, 596);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.turnIndicator);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battle Ship | Игра";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label turnIndicator;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label myBoardLabel;
        private System.Windows.Forms.Label enemyBoardLabel;
    }
}