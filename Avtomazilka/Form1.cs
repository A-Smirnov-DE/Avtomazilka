using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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


            //Boolean bbb = false;
        } // Form1_Load()


        /**
         * На кнопку нажали.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            addNewLineToRichTextBox1("на кнопку нажали");

            // Считываем пользователей
            List<YouTubeUser> userList = this.readYouTubeUsersFromFile();

            // Считываем каналы
            List<YouTubeChannel> channelsList = this.readYouTubeChannelsFromFile();

            // Заходим на Ютуб
            WebBrowser firefox = new WebBrowser();

            foreach (YouTubeUser aUser in userList)
            {
                // Активируем фаерфокс
                Windows7Class.mouseClickOverFirefoxIcon();

                if (firefox.openPrivateWindow())
                { // Если удалось открыть новое окно

                    // Проверяем язык виндоувса под фаерфоксом
                    Windows7Class.switchToGermanLanguage();

                    // Заходим и логинимся на ютуб
                    firefox.openYouTube(aUser);

                    foreach (YouTubeChannel aChannel in channelsList)
                    {
                        /*
                        YouTube.openVeraChanel();
                        YouTube.openVideosOfChanel();
                        */
                        YouTube.openChanel(aChannel.getUrl());

                        // счётчик просмотренных видео
                        int videoCount = 0;
                        int channelLimit = aChannel.getLimit();

                        while ((channelLimit == 0 || channelLimit > videoCount) && // не достигли границы просмотра
                                YouTube.openNewVideo()) // или пока новые видео находятся
                        {
                            YouTube.makeLike();
                            YouTube.deactivateAutoplay();
                            YouTube.waitUntilVideoSeen();

                            firefox.historyBack();
                            firefox.refreshPage();

                            videoCount++;
                        } // while
                    } // foreach

                    firefox.closeBrowserWindow();
                } // if

            } // foreach
        } // button1_Click()


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



        /**
         * Читаем информацию о каналах из ХМЛь-файла
         * @return List список классов, где хранится информация о канале
         */
        private List<YouTubeChannel> readYouTubeChannelsFromFile()
        {
            // Открываем файл
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load("../../../data/channels.xml");

            // Эльфийская магия
            XmlNode xmlChannelsNode = doc.LastChild;
            XmlNodeList xmlChannelsList = xmlChannelsNode.ChildNodes;

            // Создаём список
            List<YouTubeChannel> channelsList = new List<YouTubeChannel>();

            int limit;

            foreach (XmlNode xmlAChannel in xmlChannelsList)
            {
                if (xmlAChannel.HasChildNodes)
                { // Отсеиваем пустые узлы
                    if (xmlAChannel["limit"].IsEmpty)
                    {
                        limit = 0;
                    }
                    else
                    {
                        limit = Int32.Parse(xmlAChannel["limit"].InnerText);
                    } // if

                    // Добавляем новый канал
                    channelsList.Add(new YouTubeChannel(xmlAChannel["name"].InnerText, xmlAChannel["url"].InnerText, limit));
                } // if
            } // foreach

            return channelsList;
        } // readYouTubeUsersFromFile()


        /**
         * Читаем логины и пароли пользователей из ХМЛь-файла
         * @return List список классов, где хранятся логины и пароли к ним
         */
        private List<YouTubeUser> readYouTubeUsersFromFile()
        {
            // Открываем файл
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load("../../../data/logins.xml");

            // Эльфийская магия
            XmlNode xmlUsersNode = doc.LastChild;
            XmlNodeList xmlUserList = xmlUsersNode.ChildNodes;

            // Создаём список
            List<YouTubeUser> userList = new List<YouTubeUser>();

            foreach (XmlNode xmlAUser in xmlUserList)
            {
                if (xmlAUser.HasChildNodes)
                { // Отсеиваем пустые узлы
                    /*
                    addNewLineToRichTextBox1(xmlAUser.Name);
                    addNewLineToRichTextBox1(xmlAUser["email"].InnerText);
                    addNewLineToRichTextBox1(xmlAUser["password"].InnerText);
                    addNewLineToRichTextBox1("");
                    */

                    // Добавляем нового пользователя
                    userList.Add(new YouTubeUser(xmlAUser["email"].InnerText, xmlAUser["password"].InnerText));
                } // if
            } // foreach

            return userList;
        } // readYouTubeUsersFromFile()
    }
}
