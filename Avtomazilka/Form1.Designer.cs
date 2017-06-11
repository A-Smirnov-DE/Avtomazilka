namespace Avtomazilka
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.fileNameField = new System.Windows.Forms.TextBox();
            this.deltaField = new System.Windows.Forms.TextBox();
            this.TestButton = new System.Windows.Forms.Button();
            this.TestBox = new System.Windows.Forms.GroupBox();
            this.TestBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(350, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 70);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(350, 197);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // fileNameField
            // 
            this.fileNameField.Location = new System.Drawing.Point(6, 19);
            this.fileNameField.Name = "fileNameField";
            this.fileNameField.Size = new System.Drawing.Size(241, 20);
            this.fileNameField.TabIndex = 2;
            this.fileNameField.Text = "Имя файла";
            // 
            // deltaField
            // 
            this.deltaField.Location = new System.Drawing.Point(253, 19);
            this.deltaField.Name = "deltaField";
            this.deltaField.Size = new System.Drawing.Size(45, 20);
            this.deltaField.TabIndex = 3;
            this.deltaField.Text = "Дельта";
            // 
            // TestButton
            // 
            this.TestButton.BackColor = System.Drawing.SystemColors.Control;
            this.TestButton.Location = new System.Drawing.Point(304, 19);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(40, 23);
            this.TestButton.TabIndex = 4;
            this.TestButton.Text = "Тест";
            this.TestButton.UseVisualStyleBackColor = false;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // TestBox
            // 
            this.TestBox.Controls.Add(this.fileNameField);
            this.TestBox.Controls.Add(this.TestButton);
            this.TestBox.Controls.Add(this.deltaField);
            this.TestBox.Location = new System.Drawing.Point(12, 273);
            this.TestBox.Name = "TestBox";
            this.TestBox.Size = new System.Drawing.Size(350, 51);
            this.TestBox.TabIndex = 5;
            this.TestBox.TabStop = false;
            this.TestBox.Text = "TestBox";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 336);
            this.Controls.Add(this.TestBox);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Avtomatizilka";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TestBox.ResumeLayout(false);
            this.TestBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox fileNameField;
        private System.Windows.Forms.TextBox deltaField;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.GroupBox TestBox;
    }
}

