
namespace BattleShip_WPF
{
    partial class ModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BackButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CreateButton = new System.Windows.Forms.Button();
            this.FastButton = new System.Windows.Forms.Button();
            this.ClassicButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(984, 100);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите режим";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.BackButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.CreateButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.FastButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ClassicButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 461);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // BackButton
            // 
            this.BackButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BackButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackButton.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.BackButton.FlatAppearance.BorderSize = 2;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackButton.ForeColor = System.Drawing.Color.Navy;
            this.BackButton.Location = new System.Drawing.Point(71, 362);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(350, 81);
            this.BackButton.TabIndex = 6;
            this.BackButton.Text = "Назад";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(495, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(486, 112);
            this.label4.TabIndex = 5;
            this.label4.Text = "Настройка флота и расстановки кораблей вручную на поле 10x10";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(495, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(484, 112);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ускоренная классическая версия. Поле 8х8";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CreateButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CreateButton.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.CreateButton.FlatAppearance.BorderSize = 2;
            this.CreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreateButton.ForeColor = System.Drawing.Color.Navy;
            this.CreateButton.Location = new System.Drawing.Point(71, 247);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(350, 81);
            this.CreateButton.TabIndex = 2;
            this.CreateButton.Text = "Своя игра";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // FastButton
            // 
            this.FastButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.FastButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.FastButton.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.FastButton.FlatAppearance.BorderSize = 2;
            this.FastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FastButton.ForeColor = System.Drawing.Color.Navy;
            this.FastButton.Location = new System.Drawing.Point(71, 132);
            this.FastButton.Name = "FastButton";
            this.FastButton.Size = new System.Drawing.Size(350, 81);
            this.FastButton.TabIndex = 0;
            this.FastButton.Text = "Ускоренный";
            this.FastButton.UseVisualStyleBackColor = false;
            this.FastButton.Click += new System.EventHandler(this.FastButton_Click);
            // 
            // ClassicButton
            // 
            this.ClassicButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ClassicButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClassicButton.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.ClassicButton.FlatAppearance.BorderSize = 2;
            this.ClassicButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClassicButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClassicButton.ForeColor = System.Drawing.Color.Navy;
            this.ClassicButton.Location = new System.Drawing.Point(71, 17);
            this.ClassicButton.Name = "ClassicButton";
            this.ClassicButton.Size = new System.Drawing.Size(350, 81);
            this.ClassicButton.TabIndex = 1;
            this.ClassicButton.Text = "Классический";
            this.ClassicButton.UseVisualStyleBackColor = false;
            this.ClassicButton.Click += new System.EventHandler(this.ClassicButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(495, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(486, 112);
            this.label2.TabIndex = 3;
            this.label2.Text = "Стандартные правила игры, пошаговые выстрелы. Поле 10х10";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(191)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ModeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battle Ship | Режим игры";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button FastButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button ClassicButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BackButton;
    }
}