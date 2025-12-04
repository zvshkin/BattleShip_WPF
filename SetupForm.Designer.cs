namespace BattleShip_WPF
{
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.gamePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.rotateBtn = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.dellBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(173)))), ((int)(((byte)(154)))));
            this.gamePanel.Location = new System.Drawing.Point(12, 99);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(400, 400);
            this.gamePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(784, 96);
            this.label1.TabIndex = 2;
            this.label1.Text = "Расставьте флот!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.statusLabel.ForeColor = System.Drawing.Color.Navy;
            this.statusLabel.Location = new System.Drawing.Point(450, 110);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            this.statusLabel.TabIndex = 3;
            // 
            // rotateBtn
            // 
            this.rotateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(173)))), ((int)(((byte)(154)))));
            this.rotateBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(110)))));
            this.rotateBtn.FlatAppearance.BorderSize = 2;
            this.rotateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rotateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rotateBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.rotateBtn.Location = new System.Drawing.Point(450, 300);
            this.rotateBtn.Name = "rotateBtn";
            this.rotateBtn.Size = new System.Drawing.Size(259, 50);
            this.rotateBtn.TabIndex = 4;
            this.rotateBtn.Text = "Повернуть (Гориз)";
            this.rotateBtn.UseVisualStyleBackColor = false;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.OrangeRed;
            this.startButton.Enabled = false;
            this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(110)))));
            this.startButton.FlatAppearance.BorderSize = 2;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(450, 356);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(315, 60);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "В БОЙ!";
            this.startButton.UseVisualStyleBackColor = false;
            // 
            // BackButton
            // 
            this.BackButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(173)))), ((int)(((byte)(154)))));
            this.BackButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(110)))));
            this.BackButton.FlatAppearance.BorderSize = 2;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BackButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.BackButton.Location = new System.Drawing.Point(450, 449);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(315, 50);
            this.BackButton.TabIndex = 7;
            this.BackButton.Text = "Назад";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // dellBtn
            // 
            this.dellBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dellBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(173)))), ((int)(((byte)(154)))));
            this.dellBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(110)))));
            this.dellBtn.FlatAppearance.BorderSize = 2;
            this.dellBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dellBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dellBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.dellBtn.Image = ((System.Drawing.Image)(resources.GetObject("dellBtn.Image")));
            this.dellBtn.Location = new System.Drawing.Point(715, 300);
            this.dellBtn.Name = "dellBtn";
            this.dellBtn.Size = new System.Drawing.Size(50, 50);
            this.dellBtn.TabIndex = 8;
            this.dellBtn.UseVisualStyleBackColor = false;
            this.dellBtn.Click += new System.EventHandler(this.dellBtn_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.ControlBox = false;
            this.Controls.Add(this.dellBtn);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.rotateBtn);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gamePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battle Ship | Расстановка флота";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button rotateBtn;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button dellBtn;
    }
}