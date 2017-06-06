using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomazilka
{
    class Stencil
    {
        /**
         * Позволенная погрешность при поиске цвета.
         */
        private int delta = 0;

        /**
         * Тут хранится изображение, которое позже будем искать.
         */
        private Bitmap image;


        /**
         * Папка, где хранятся все картинки.
         */
        private String imageFolder = @"..\..\..\imgs\";


        /**
         * Конструктор
         * @param String filename имя файла с картинкой
         * @param int delta = 0, размер участка, в котором могут колебаться цвета.
         */
        public Stencil(String filename)
        {
            String path = this.imageFolder + filename;
            loadSmallImage(@path);
        }
        

        /**
         * Загрузка миникартинки.
         * @param String path путь к картинке
         */
        private Bitmap loadSmallImage(String path)
        {
            // Create a Bitmap object from an image file.
            Bitmap image = new Bitmap(path);
            this.image = image;
            return image;
        } // loadSmallImage()


        /**
         * Ищет шаблон и кликает по нему мышкой.
         */
        public Boolean mouseClick()
        {
            BotClass.create_screen_shot();

            return BotClass.imageSearchAndMouseClick(this.image, this.delta);
        } // mouseClick()


        /**
         * Ищет шаблон и двигает на него мышку.
         */
        public Boolean mouseMove()
        {
            BotClass.create_screen_shot();
            
            return BotClass.imageSearchAndMouseMove(this.image, this.delta);
        } // imageSearchRect()


        /**
         * Задаёт допустимую погрешность при поиске цвета.
         */
        public void setColorDelta(int delta)
        {
            this.delta = delta;
        } // setColorDelta()



/*
        public Bitmap getImage()
        {
            return this.image;
        } // getImage()

        public Rectangle searchRect()
        {
            Bot thisBot = new Bot();
            return thisBot.imageSearchRect(this.image);
        } // imageSearchRect()

        public Boolean isFound()
        {
            myBot thisBot = new myBot();
            return thisBot.imageSearch(this.image);
        } // isFound()

        public Rectangle searchRectInRect(Rectangle area)
        {
            myBot thisBot = new myBot(area);
            return thisBot.imageSearchRect(this.image);
        } // imageSearchRect()

        public Boolean isFoundInRect(Rectangle rect)
        {
            myBot thisBot = new myBot(rect);
            return thisBot.imageSearch(this.image);
        } // isFoundInRect()

        /**
         * Поиск шаблона в определённой области.
         * /
        public Boolean mouseMove(Rectangle area)
        {
            myBot thisBot = new myBot(area);
            return thisBot.imageSearchAndMouseMove(this.image);
        } // imageSearchRect()
    */
    }
}
