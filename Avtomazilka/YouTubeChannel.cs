using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomazilka
{
    class YouTubeChannel
    {
        /**
         * имя канала
         */
        private String name;

        /**
         * адрес канала
         */
        private String url;

        /**
         * сколько видео за раз смотреть с канала
         */
        private int limit;


        /**
         * Конструктор
         * @param String name - имя канала
         * @param String url - адрес канала
         * @param int limit - сколько видео за раз смотреть с канала
         */
        public YouTubeChannel(String name, String url, int limit = 0)
        {
            this.setName(name);
            this.setUrl(url);
            this.setLimit(limit);
        } // YouTubeChannels()


        /**
         * Возвращает лимит канала
         */
        public int getLimit()
        {
            return this.limit;
        } // getName()


        /**
         * Возвращает название канала
         */
        public String getName()
        {
            return this.name;
        } // getName()


        /**
         * Возвращает url канала
         */
        public String getUrl()
        {
            return this.url;
        } // getName()


        /**
         * Сохраняет лимит канала
         * @param int limit лимит канала
         */
        public void setLimit(int limit = 0)
        {
            this.limit = limit;
        } // setLimit()


        /**
         * Сохраняет имя канала
         * @param name имя канала
         */
        public void setName(String name)
        {
            this.name = name;
        } // setName()


        /**
         * Сохраняет url канала
         * @param url канала
         */
        public void setUrl(String url)
        {
            this.url = url;
        } // setUrl()
    }
}
