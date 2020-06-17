using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    struct Ship
    {
        public bool exists;
        public bool shunken;
        public int beginX;
        public int beginY;
        public int endX;
        public int endY;

        public void setPlace(bool exists, int beginx, int beginy, int endx, int endy)
        {
            this.exists = exists;
            this.beginX = beginx;
            this.beginY = beginy;
            this.endX = endx;
            this.endY = endy;
        }
    }
}
