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
            
            // Активируем фаерфокс
            Windows7Class.mouseClickOverFirefoxIcon();
            
            // Заходим на Ютуб
            WebBrowser firefox = new WebBrowser();
            //firefox.openYouTube();

            // Проверяем язык виндоувса под фаерфоксом
            Windows7Class.switchToGermanLanguage();

            // Заходим и логинимся на ютуб
            firefox.openYouTube();

            YouTube.openVeraChanel();
            YouTube.openVideosOfChanel();
            YouTube.openNewVideo();
            YouTube.makeLike();
            YouTube.waitUntilVideoSeen();

            firefox.historyBack();

            //Boolean bbb = 
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

        private void TestButton_Click(object sender, EventArgs e)
        {
            this.TestButton.BackColor = Color.Transparent;
            this.Refresh();
            this.Invalidate();

            // Ждём
            System.Threading.Thread.Sleep(5000);

            String fileName    = fileNameField.Text;
            String deltaString = deltaField.Text;

            int delta;

            if (deltaString == "Дельта")
            {
                delta = 0;
            }
            else
            {
                delta = Int32.Parse(deltaField.Text);
            } // if
            

            // Если не передали расширение файла, то это будет .png
            if (fileName.IndexOf('.') == -1)
            {
                fileName += ".png";
            } // if

            Stencil testStencil = new Stencil(fileName);
            testStencil.setColorDelta(delta);
            if (testStencil.mouseMove())
            {
                this.TestButton.BackColor = Color.FromArgb(128, 255, 128);
            }
            else
            {
                this.TestButton.BackColor = Color.FromArgb(255, 128, 128);
            } // if

            this.Refresh();
            this.Invalidate();
        } // TestButton_Click()
    }
}


/*
 * Ваш Ювелир
 * Your.Best.Jeweler@gmail.com
 * yaq123456
 * 29.01.1980
 * M
*/
