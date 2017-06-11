using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avtomazilka
{
    static class Windows7Class
    {
        public static void mouseClickOverFirefoxIcon()
        {
            //BotClass.create_screen_shot();

            Stencil firefoxIcon = new Stencil("mozilla-firefox.bmp");
            firefoxIcon.setColorDelta(15); // 12 <= delta
            firefoxIcon.mouseClick();
        } // mouseClickOverFirefoxIcon()

        public static void switchToGermanLanguage()
        {
            Stencil deIcon = new Stencil("windows-language-icon-de.png");
            deIcon.setColorDelta(100); // 110 <= delta < 120

            bool test = deIcon.isFound();
            while (!test)
            {
                BotClass.keyDown(Keys.Alt); // зажимаем Alt
                BotClass.keyDown(Keys.ShiftKey, Keys.Alt); // зажимаем Shift

                BotClass.keyUp(Keys.ShiftKey, Keys.Alt); // отжимаем Shift
                BotClass.keyUp(Keys.Alt); // отжимаем Alt

                test = deIcon.isFound();
            } // while()

        } // switchToGermanLanguage()
    } // class Windows7Class
}
