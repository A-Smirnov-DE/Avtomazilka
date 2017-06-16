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
        public bool closeBrowserWindow()
        {
            Stencil closeWindow = new Stencil("mozilla-firefox-closeWindow.png");
            closeWindow.setColorDelta(20);
            if (closeWindow.mouseClick())
            { // увидели значёк закрытия окна и смогли по нему кликнуть.
                //сдвинуть курсор в сторону, а то он перекрывает менюшку
                BotClass.moveCursor(new System.Drawing.Rectangle(10, 10, 50, 50));
                return true;
            }

            return false;
        } // closeBrowserWindow()

        /**
         * Открывает приватное окно браузера
         */
        public bool openPrivateWindow()
        {
            Stencil menuIcon = new Stencil("mozilla-firefox-menuIcon.png");
            menuIcon.mouseClick();

            Stencil privateWindowIcon = new Stencil("mozilla-firefox-privateWindowIcon.png");
            return privateWindowIcon.mouseClick();
        } // openPrivateWindow()


        public bool openYouTube(YouTubeUser user)
        {
            if (YouTube.isYouTubePage())
            { // Мы уже на странице "Ютуба"
                //@TODO проверить залогинен ли пользователь
                return this.loginYouTube(user);
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

            result = result && this.loginYouTube(user);

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


        private bool loginYouTube(YouTubeUser user)
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
            this.waitUntilPageIsLoaded();

            // Надпись Логин (если её нет, то не надо будет вводить имя пользователя)
            Stencil login = new Stencil("YouTube-Login-DE.png");

            if (login.isFound())
            { // Если спрашивают логин, то его печатаем
                // Печатаем логин-майл
                BotClass.printString(user.getEmail() + Environment.NewLine);

                // Ждём
                this.waitUntilPageIsLoaded();
            } // if

            // Печатаем логин-майл
            //BotClass.printString("yaq123456" + Environment.NewLine);
            BotClass.printString(user.getPassword() + Environment.NewLine);

            this.waitUntilPageIsLoaded();

            return true;
        } // loginYouTube()


        /**
         * Обновляет страницу браузера.
         */
        public Boolean refreshPage()
        {
            this.waitUntilPageIsLoaded();

            BotClass.keyDown(Keys.F5);
            BotClass.keyUp(Keys.F5);

            this.waitUntilPageIsLoaded();

            return true;
        } // refreshPage()


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
