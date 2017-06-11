using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avtomazilka
{
    static class BotClass
    {
        [Flags]

        public enum MOUSEEVENTF
        {
            MOVE = 0x01,
            LEFTDOWN = 0x02,
            LEFTUP = 0x04,
            RIGHTDOWN = 0x08,
            RIGHTUP = 0x10,
            ABSOLUTE = 0x8000
        }
        
        public enum KEYEVENT
        {
            KEYDOWN = 0x0001, //Key down flag
            KEYUP = 0x0002, //Key up flag
        }


        /*
http://www.kbdedit.com/manual/low_level_vk_list.html

https://msdn.microsoft.com/de-de/library/aa243025(v=VS.60).aspx

https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
        */

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, IntPtr dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);



        /**
         * Папка, где хранятся все картинки.
         */
        private static String imageFolder = @"..\..\..\imgs\";


        /**
         * 
         */
        static Rectangle area;

        /**
         * В этой переменной хранится изображение, где надо искать мини-картинку.
         */
        static Bitmap screenShot;


        /**
         * Конструктор.
         * Ищем картинку на всём экране.
         * /
        public BotClass()
        {
            this.create_screen_shot();
        } // BotClass()

        public BotClass(Rectangle area)
        {
            BotClass.area = area;
            this.create_screen_shot();
        }
*/

        public static Boolean _screenShotSubSearch(Bitmap smallImage, Point screenShotPoint, int colorDelta = 0)
        { // Стартовые координаты на большом скриншоте
            int xPositionStartPoint = screenShotPoint.X;
            int yPositionStartPoint = screenShotPoint.Y;

            // Идём по маленькой картинке
            for (int yPosition = 0; yPosition < smallImage.Height; yPosition++)
            { // Прогон маленькой картинки по оси y (Столбики)
                for (int xPosition = 0; xPosition < smallImage.Width; xPosition++)
                { // Прогон маленькой картинки по оси x (Строчки)

                    Color screenShotPixelColor      = smallImage.GetPixel(xPosition, yPosition);
                    Color smallImageFirstPixelColor = screenShot.GetPixel(xPosition + xPositionStartPoint, yPosition + yPositionStartPoint);
                    
                    //434, 732

                    //if (!smallImage.GetPixel(xPosition, yPosition).Equals(
                    //     screenShot.GetPixel(xPosition + xPositionStartPoint, yPosition + yPositionStartPoint)))
                    if (!(((screenShotPixelColor.R - colorDelta) <= smallImageFirstPixelColor.R && smallImageFirstPixelColor.R <= (screenShotPixelColor.R + colorDelta)) &&
                          ((screenShotPixelColor.G - colorDelta) <= smallImageFirstPixelColor.G && smallImageFirstPixelColor.G <= (screenShotPixelColor.G + colorDelta)) &&
                          ((screenShotPixelColor.B - colorDelta) <= smallImageFirstPixelColor.B && smallImageFirstPixelColor.B <= (screenShotPixelColor.B + colorDelta))))
                    {
//                        BotClass.moveCursor(xPosition + xPositionStartPoint, yPosition + yPositionStartPoint);
                        return false;
                    } // if
                } // for
            } // for

            return true;
        } // _screenShotSubSearch()


        /**
         * Создаём скриншот зоны, где будем искать картинку.
         * @param String subString = "" позволяет сохранить скриншот под другим именем.
         */
        //private Bitmap create_screen_shot(int sourceX = 0, int sourceY = 0)
        public static Bitmap create_screen_shot(int number = 0)
        {
            int imageWidth, imageHeight, sourceX, sourceY;
            Graphics graph = null;

            if (!BotClass.area.IsEmpty)
            {   // Ищем картинку только в определённом регионе
                imageWidth = BotClass.area.Width;
                imageHeight = BotClass.area.Height;

                sourceX = BotClass.area.X;
                sourceY = BotClass.area.Y;
            }
            else
            {   // Ищем картинку на всём экране
                imageWidth = Screen.PrimaryScreen.Bounds.Width;
                imageHeight = Screen.PrimaryScreen.Bounds.Height;

                sourceX = 0;
                sourceY = 0;
            }

            var bmp = new Bitmap(imageWidth, imageHeight);

            graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(sourceX, sourceY, 0, 0, bmp.Size);

            screenShot = bmp;

            String path = imageFolder + "screen" + (number != 0 ? number.ToString() : "") + ".bmp";
            bmp.Save(@path);
            return bmp;
        } // create_screen_shot()

        
        /**
         * Ищет маленькую картинку в большой картинке
         * @param Bitmap smallImage - искомая картинка
         * @param int colorDelta - допустимая погрешность в цвете
         * @return Boolean
         */
        /*
        public static Rectangle imageSearch(Bitmap smallImage, int colorDelta = 0)
        {
            return BotClass.image Search Rect(smallImage, colorDelta);
        } // imageSearch()
        */


        /**
         * Ищет маленькую картинку в большой картинке. И кликает по ней мышкой.
         * @param Bitmap smallImage - искомая картинка
         * @param int colorDelta - допустимая погрешность в цвете
         */
        public static Rectangle imageSearchAndMouseClick(Bitmap smallImage, int colorDelta = 0)
        {
            Rectangle rec = BotClass.imageSearchAndMouseMove(smallImage, colorDelta);

            if (!rec.IsEmpty)
            { // Картинка была найдена, курсор сдвинут

                // Ждём
                System.Threading.Thread.Sleep(2000);

                // Кликаем
                BotClass.mouseClick();
            } // if

            return rec;
        } // imageSearchAndMouseClick()


        /**
         * Ищет маленькую картинку в большой картинке. И двигает на неё курсор мышки.
         * @param Bitmap smallImage - искомая картинка
         * @param int colorDelta - допустимая погрешность в цвете
         * @return Rectangle - координаты найденой картинки (Если картинка не найдена, то пустой прямоугольник)
         */
        public static Rectangle imageSearchAndMouseMove(Bitmap smallImage, int colorDelta = 0)
        {
            Rectangle rec = BotClass.imageSearch(smallImage, colorDelta);

            if (!rec.IsEmpty)
            { // Картинка была найдена
                if (!BotClass.area.IsEmpty)
                {
                    rec.Offset(BotClass.area.Location);
                }

                // Двигаем курсор мыши
                BotClass.moveCursor(rec);
            }

            return rec;
        } // imageSearchAndMouseMove()


        /**
         * Ищет картинку
         */
        public static Rectangle imageSearch(Bitmap smallImage, int colorDelta = 0)
        {
            Color smallImageFirstPixelColor = smallImage.GetPixel(0, 0);

            Color screenShotPixelColor = new Color();
            // Ищем до совпадения по первому пикселю, потом переходим в подпрограмму.
            for (int yPosition = 0; yPosition < screenShot.Height /*- smallImage.Height */; yPosition++)
            { // Прогон большой картинки по оси y (Столбики)
                //BotClass.moveCursor(10, yPosition);

                for (int xPosition = 0; xPosition < screenShot.Width - smallImage.Width; xPosition++)
                { // Прогон большой картинки по оси x (Строчки)

                    screenShotPixelColor = screenShot.GetPixel(xPosition, yPosition);

//                  if (screenShotPixelColor.Equals(smallImageFirstPixelColor))
                    if (((screenShotPixelColor.R - colorDelta) <= smallImageFirstPixelColor.R && smallImageFirstPixelColor.R <= (screenShotPixelColor.R + colorDelta)) &&
                        ((screenShotPixelColor.G - colorDelta) <= smallImageFirstPixelColor.G && smallImageFirstPixelColor.G <= (screenShotPixelColor.G + colorDelta)) &&
                        ((screenShotPixelColor.B - colorDelta) <= smallImageFirstPixelColor.B && smallImageFirstPixelColor.B <= (screenShotPixelColor.B + colorDelta)))
                        { // пиксель сошёлся
                        
                        Point firstPoint = new Point(xPosition, yPosition);

                        //BotClass.moveCursor(firstPoint);


                        if (_screenShotSubSearch(smallImage, firstPoint, colorDelta))
                        { // Ура!!! Картинку нашли!!!
                            return new Rectangle(firstPoint, new Size(smallImage.Width, smallImage.Height));
                        } // if Картинку нашли!!!
                    } // if пиксель сошёлся

                } // строчки
            } // столбики

            return new Rectangle();
        } // imageSearch()


        /**
         * Сдвинуть курсор мышки по определённым координатам.
         * @param Point p - координаты точки.
         */
        public static void moveCursor(Point p)
        {
            System.Windows.Forms.Cursor Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = p;
        } // moveCursor(Point p)


        /**
         * Нажимаем на клавишу.
         * @param byte vKey - ???
         * @param byte sKey = 0 - ???
         */
        public static void keyDown(byte bVk, byte key = 0)
        {
            uint scanCode;

            if (key == 0)
            {
                scanCode = 0;
            }
            else
            {
                scanCode = MapVirtualKey((uint)key, 0);
            }
            keybd_event(bVk, (byte)scanCode, (int)KEYEVENT.KEYDOWN, 0);

            Random rnd = new Random();
            System.Threading.Thread.Sleep((int)rnd.Next(120, 200));
        } // keyDown()


        /**
         * Нажимаем на клавишу.
         * @param Keys vKey - ???
         * @param Keys sKey = 0 - ???
         */
        public static void keyDown(Keys vKey, Keys sKey = 0)
        {
            keyDown((byte)vKey, (byte)sKey);
        } // keyDown()


        /**
         * Отпускаем клавишу.
         * @param byte bVk
         * @param byte key = 0
         */
        public static void keyUp(byte bVk, byte key = 0)
        {
            uint scanCode;

            if (key == 0)
            {
                scanCode = 0;
            }
            else
            {
                scanCode = MapVirtualKey((uint)key, 0);
            }

            if (bVk == (byte)Keys.RMenu || bVk == (byte)Keys.ShiftKey)
            {
                keybd_event(bVk, (byte)scanCode, (int)KEYEVENT.KEYDOWN | (int)KEYEVENT.KEYUP, 0);
            }
            else
            {
                keybd_event(bVk, (byte)scanCode, (int)KEYEVENT.KEYUP, 0);
            }

            Random rnd = new Random();
            System.Threading.Thread.Sleep((int)rnd.Next(120, 200));
        } // keyUp()


        /**
         * Отпускаем клавишу.
         * @param Keys bVk
         * @param Keys key = 0
         */
        public static void keyUp(Keys vKey, Keys sKey = 0)
        {
            keyUp((byte)vKey, (byte)sKey);
        } // keyUp()


        /**
         * Кликает мышкой.
         */
        public static void mouseClick()
        {
            //нажали правую кнопку
            mouse_event((uint)MOUSEEVENTF.ABSOLUTE | (uint)MOUSEEVENTF.LEFTDOWN | (uint)MOUSEEVENTF.LEFTUP, 0, 0, 0, IntPtr.Zero);
            System.Threading.Thread.Sleep(2000);
        } // mouseClick()


        /**
         * Сдвинуть курсор мышки по определённым координатам.
         * @param int x - x координата точки.
         * @param int y - y координата точки.
         */
        public static void moveCursor(int x, int y)
        {
            moveCursor(new Point(x, y));
        } // MoveCursor(int x, int y)


        /**
         * Сдвинуть курсор мышки в определённый район.
         * @param Rectangle r - прямоугольник с координатами.
         */
        public static void moveCursor(Rectangle r)
        {
            Random rnd = new Random();
            moveCursor(
                new Point(
                    (int)rnd.Next(r.Left, r.Right),
                    (int)rnd.Next(r.Top, r.Bottom)));
        } // MoveCursor(int x, int y)


        public static void printString(String s)
        { // В переменой s может быть следующие группы символов a-z, A-Z, @, /, 0-9, Shift, Ctrl, Win-Fn, Alt, Alt Gr и т.д.
            foreach (char cOrig in s)
            {
                Char c;
                Keys pressedKey = 0;
                Boolean pressedSHIFT = false, pressedCTRL = false, pressedALT = false, pressedALTGR = false;
                if ('a' <= cOrig && cOrig <= 'z')
                {   // маленькие буквы a-z
                    c = Char.ToUpper(cOrig);
                }
                else if ('A' <= cOrig && cOrig <= 'Z')
                {   // большие буквы A-Z (Shift + a-z)

                    keyDown(Keys.ShiftKey); // зажимаем Shift
                    pressedSHIFT = true;

                    c = cOrig;
                }
                else if ('.' == cOrig)
                {   // точка
                    c = (char)Keys.OemPeriod;
                }
                else if ('/' == cOrig)
                {   // точка
                    c = (char)Keys.Divide;
                }
                else if ('@' == cOrig)
                {   // символ @

                    pressedKey = Keys.RMenu;
                    keyDown(pressedKey, pressedKey); // зажимаем AltGr
                    pressedALTGR = true;

                    c = 'Q';
                }
                else
                {
                    c = cOrig;
                }

                byte b = (byte)c;
                byte bPressedKey = 0; // (byte)pressedKey;
                keyDown(b, bPressedKey);
                keyUp(b, bPressedKey);

                if (pressedALTGR)
                {
                    pressedKey = Keys.RMenu;
                    keyUp(pressedKey, pressedKey); // отжимаем AltGr
                    pressedKey = 0;
                    pressedALTGR = false;
                }

                if (pressedALT)
                {
                    keyUp(0x12); // отжимаем Alt
                    pressedALT = false;
                }

                if (pressedCTRL)
                {
                    keyUp(0x11); // отжимаем Ctrl
                    pressedCTRL = false;
                }

                if (pressedSHIFT)
                {
                    keyUp(Keys.ShiftKey); // отжимаем Shift
                    pressedSHIFT = false;
                }

            } // foreach

            System.Threading.Thread.Sleep(2000);
        }








        /*


        [Flags]
        
        
        /*
        public enum KEYCODE
        {
            Point = 0x2e, // Точка

            A = 0x41, //A key code
            B = 0x42, //B key code
            C = 0x43, //C key code

            a = 0x61, //a key code
            b = 0x62,
            c = 0x63,

            VK_LCONTROL = 0xA2, //Left Control key code
        }
        * /
        

constructor



        public static Color getColor(int xPosition, int yPosition)
        {
            /*
            if (!area.IsEmpty)
            { // Поправка на тот случай, если поиск делался не по всему экрану, а только в части.
                xPosition += area.X;
                yPosition += area.Y;
            } // if
            * /

            return screenShot.GetPixel(xPosition, yPosition);
        } // getColor()

        public static Color getColor(Point p)
        {
            return getColor(p.X, p.Y);
        } // getColor()
        
*/
    }
}
