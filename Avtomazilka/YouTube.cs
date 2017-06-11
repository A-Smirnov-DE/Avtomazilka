using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avtomazilka
{
    static class YouTube
    {
        /**
         * Открыта ли страница Ютуба?
         * 
         * @return bool true - страница Ютуба открыта, в противном случае false
         */
        public static bool isYouTubePage()
        {
            Stencil youTubeIcon = new Stencil("YouTube-Icon.png");
            //youTubeIcon.setColorDelta(12);
            return youTubeIcon.isFound();
        } // isYouTubePage()


        /**
         * Ставит лайк у видео
         */
        public static bool makeLike()
        {
            Stencil likeIcon = new Stencil("YouTube-Like-Icon.png");
            //likeIcon.setColorDelta(0);

            //@TODO проверить есть ли у браузера вертикальный бегунок и в зависимости от этого работать дальше.

            // Счётчик сколько раз мы "прокрутим" экран браузера вниз.
            int counter = 0;

            // Пока не увидим символ лайка, "прокручиваем" экран браузера вниз и считаем эти "прокручивания".
            while (!likeIcon.mouseClick())
            {
                counter++;
                BotClass.keyDown(Keys.PageDown);
                BotClass.keyUp(Keys.PageDown);

                // Не забываем сбросить старые результаты поиска
                likeIcon.resetRec();

                // Ждём две секунды, чтобы экран прокрутился вниз.
                System.Threading.Thread.Sleep(2000);
            }

            // Возвращаем экран обратно (не пойму почему, но не всегда срабатывает)
            for (int i = 0; i<=counter+1; i++)
            {
                BotClass.keyDown(Keys.PageUp);
                BotClass.keyUp(Keys.PageUp);

                // Ждём две секунды, чтобы экран прокрутился вниз.
                System.Threading.Thread.Sleep(5000);
            }

            return true;
        } // makeLike()


        /**
         * Открываем непросмотренные видео.
         */
        public static bool openNewVideo()
        {
            // Нашли двоеточие во времени у непросмотренного видео.
            Rectangle rec = YouTube.searchNewVideos();

            // Размер картинки видео в списке 196*110
            // Размер картинки видео над полоской времени 196*94
            // Сдвигаем найденый прямоугольник на 150 и 80 пиксель, и растягиваем прямоугольник до 125 и 70 пикселей.
            rec.Offset(-150, -80);
            rec.Size = new Size(125, 70);

            // Двигаем курсор мышки и кликаем
            BotClass.moveCursor(rec);
            BotClass.mouseClick();

            return true;
        } // openNewVideo()


        /**
         * Открывает верын канал.
         * 
         * @return bool true - если находится изображения канала, в противном случае false
         */
        public static bool openVeraChanel()
        {
            YouTube.waitUntilPageIsLoaded();
            Stencil emptyChanelIcon = new Stencil("YouTube-EmptyChanel-Icon.png");
            do
            { // Ждём
                System.Threading.Thread.Sleep(500);

                // надо сбрасывать старые находки
                emptyChanelIcon.resetRec();
            } while (emptyChanelIcon.isFound());

            Stencil veraChanel = new Stencil("VeraChanel-Icon.png");
            //veraChanel.setColorDelta(0);
            return veraChanel.mouseClick();
        } // openVeraChanel()


        /**
         * Открываем закладку "видео".
         */
        public static bool openVideosOfChanel()
        {
            YouTube.waitUntilPageIsLoaded();

            Stencil videosOfChanel = new Stencil("VideosOfChanel-DE.png");
            //videosOfChanel.setColorDelta(0);

            return videosOfChanel.mouseClick();
        } // openVideosOfChanel()


        /**
         * По двоеточии во времени ищет непросмотренные видео.
         */
        public static Rectangle searchNewVideos()
        {
            YouTube.waitUntilPageIsLoaded();

            Stencil videoTimeColon = new Stencil("YouTube-Video-TimeColon.png");
            videoTimeColon.setColorDelta(30);
            videoTimeColon.isFound();

            return videoTimeColon.getRec();
        } // searchNewVideos()


        /**
         * Проверяем загрузилась ли страница. Ajax (красная полоска вверху экрана)
         */
        public static void waitUntilPageIsLoaded()
        {
            //@TODO
            // Ждём
            System.Threading.Thread.Sleep(7500);
        } // waitUntilPageIsLoaded()

        
        /**
         * Ждёт пока видео не просмотрится до конца
         */
        public static bool waitUntilVideoSeen()
        {
            Stencil videoSeen = new Stencil("YouTube-Video-Seen.png");
            //videoSeen.setColorDelta(0);

            do
            {
                // Ждём по 10 секунд
                System.Threading.Thread.Sleep(10000);
                // Не забываем сбросить результаты прошлого поиска.
                videoSeen.resetRec();
            } while (!videoSeen.isFound());

            return true;
        } // waitUntilVideoSeen()
    }
}
