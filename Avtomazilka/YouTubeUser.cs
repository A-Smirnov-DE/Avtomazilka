using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtomazilka
{
    class YouTubeUser
    {
        private String email;
        private String password;

        public YouTubeUser(String email, String password)
        {
            this.setEmail(email);
            this.setPassword(password);
        }

        public String getEmail()
        {
            return this.email;
        }

        public String getPassword()
        {
            return this.password;
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

        public void setPassword(String password)
        {
            this.password = password;
        }
    }
}
