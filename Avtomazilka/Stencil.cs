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
         * Если картинка будет найдена, то координаты будут храниться тут.
         */
        private Rectangle rec = new Rectangle();


        /**
         * Конструктор
         * @param String filename имя файла с картинкой
         * @param int delta = 0, размер участка, в котором могут колебаться цвета.
         */
        public Stencil(String filename)
        {
            String path = this.imageFolder + filename;
            loadSmallImage(@path);
        } // Stencil()


        /**
         * Возвращает координаты объекта
         */
        public Rectangle getRec()
        {
            return this.rec;
        } // getRec()


        /**
         * Есть ли картинка?
         */
        public Boolean isFound()
        {
            if (!this.rec.IsEmpty)
            { // Картинка уже была найдена
                return true;
            } // if

            BotClass.create_screen_shot();

            Rectangle rec = BotClass.imageSearch(this.image, this.delta);
            return this.rectangleToBool(rec);
        } // isFound()


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
            if (!this.rec.IsEmpty)
            { // Картинка уже была найдена
                // Двигаем курсор на заранее найденый прямоугольник
                BotClass.moveCursor(this.rec);
                // И кликаем
                BotClass.mouseClick();

                return true;
            } // if

            BotClass.create_screen_shot();

            Rectangle rec = BotClass.imageSearchAndMouseClick(this.image, this.delta);

            return this.rectangleToBool(rec);
        } // mouseClick()


        /**
         * Ищет шаблон и двигает на него мышку.
         */
        public Boolean mouseMove()
        {
            if (!this.rec.IsEmpty)
            { // Картинка уже была найдена
                // Двигаем курсор на заранее найденый прямоугольник
                BotClass.moveCursor(this.rec);

                return true;
            } // if

            BotClass.create_screen_shot();

            Rectangle rec = BotClass.imageSearchAndMouseMove(this.image, this.delta);

            return this.rectangleToBool(rec);
        } // imageSearchRect()


        /**
         * Если действительно нашли маленькую картинку, то сохраняем её координаты.
         * @return Boolean true - если картинка была найдена, false - картинка не была найдена.
         */
        private Boolean rectangleToBool(Rectangle rec = new Rectangle())
        {
            if (rec.IsEmpty)
            { // Картинка не была найдена
                // На всякий случай сбрасываем старые данные.
                this.resetRec();

                return false;
            } // if()

            // Сохраняем координаты объекта.
            this.rec = rec;

            return true;
        } // rectangleToBool()


        /**
         * Сбрасывает координаты объекта, если раньше что-то было найдено.
         */
        public Boolean resetRec()
        {
            this.rec = new Rectangle();
            return true;
        } // resetRec()


        /**
         * Задаёт допустимую погрешность при поиске цвета.
         */
        public void setColorDelta(int delta)
        {
            this.delta = delta;
        } // setColorDelta()

    }
}
