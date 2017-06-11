using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avtomazilka
{
    class WebBrowser
    {
        public bool openYouTube()
        {
            if (YouTube.isYouTubePage())
            { // Мы уже на странице "Ютуба"
                //@TODO проверить залогинен ли пользователь
                return this.loginYouTube();
            } // if()

            
            Stencil emptyUrl = new Stencil("mozilla-firefox-empty-url-de.png");

            emptyUrl.isFound();

            if (!emptyUrl.mouseClick())
            { // Адресная строка была не пустой.
                Stencil httpUrl = new Stencil("mozilla-firefox-url-http.png");
                emptyUrl.setColorDelta(10);
                emptyUrl.mouseClick();
            }

            
            // Печатаем адрес ютуба, а заодно на всякий случай выходим из аккаунта.
            BotClass.printString("www.youtube.com/logout" + Environment.NewLine);

            this.waitUntilPageIsLoaded();

            // Тут будет хранится результат выполнений функций
            bool result = true;

            result = result && this.loginYouTube();

            return result;
        } // openYouTube()


        /**
         * Открывает предыдущую страницу.
         */
        public bool historyBack()
        {
            BotClass.keyDown(Keys.BrowserBack);
            BotClass.keyUp(Keys.BrowserBack);
            return true;
        } // historyBack()


        private bool loginYouTube()
        {
            // Ждём
            this.waitUntilPageIsLoaded();

            // Жмём на кнопку логина
            Stencil loginButtom = new Stencil("YouTube-Login-DE-Icon.png");
            //loginButtom.setColorDelta(5);
            if (!loginButtom.mouseClick())
            { // Что-то пошло не так
                return false;
            }

            // Ждём
            this.waitUntilPageIsLoaded();

            // Надпись Логин (если её нет, то не надо будет вводить имя пользователя)
            Stencil login = new Stencil("YouTube-Login-DE.png");

            if (login.isFound())
            { // Если спрашивают логин, то его печатаем
                // Печатаем логин-майл
                BotClass.printString("Your.Best.Jeweler@gmail.com" + Environment.NewLine);

                // Ждём
                this.waitUntilPageIsLoaded();
            }

            // Печатаем логин-майл
            BotClass.printString("yaq123456" + Environment.NewLine);

            this.waitUntilPageIsLoaded();

            return true;
        } // loginYouTube()


        /**
         * Проверяем загрузилась ли страница
         */
        private void waitUntilPageIsLoaded()
        {
            // ждём, пока не пропадёт надпись "соединяемся"
            Stencil connecting = new Stencil("mozilla-firefox-connecting.png");
            //connecting.setColorDelta(0);

            do
            { // Ждём
                System.Threading.Thread.Sleep(500);

                // сбрасываем старые находки
                connecting.resetRec();
            } while (connecting.isFound());
        } // waitUntilPageIsLoaded()
    }
}
