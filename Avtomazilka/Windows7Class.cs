using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomazilka
{
    static class Windows7Class
    {
        public static void mouseClickOverFirefoxIcon()
        {
            BotClass.create_screen_shot();

            Stencil firefoxIcon = new Stencil("mozilla-firefox.bmp");
            firefoxIcon.setColorDelta(3);
            firefoxIcon.mouseClick();

            //firefoxIcon.mouseClick();
        } // mouseClickOverFirefoxIcon()
    } // class Windows7Class
}
