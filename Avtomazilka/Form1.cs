using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avtomazilka
{
    public partial class Form1 : Form
    {
//        private String imageFolder = @"..\..\imgs\";

        public Form1()
        {
            InitializeComponent();
        }


        /**
         * Виндоувс-форма загрузилась.
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            addNewLineToRichTextBox1("форма загрузилась");

            Windows7Class.mouseClickOverFirefoxIcon();
        } // Form1_Load()


        /**
         * На кнопку нажали.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            addNewLineToRichTextBox1("на кнопку нажали");
        }


        /**
         * Добавляем новую строчку в текстовое поле.
         */
        public void addNewLineToRichTextBox1(String s)
        {
            richTextBox1.AppendText(s + Environment.NewLine);
        }
    }
}


/*
 * Ваш Ювелир
 * Your.Best.Jeweler@gmail.com
 * yaq123456
 * 29.01.1980
 * M
*/
