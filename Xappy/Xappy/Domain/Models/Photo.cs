using System;
using System.Collections.Generic;
using System.Text;

namespace Xappy.Domain.Models
{
    public class Photo
    {
        public string ImageSrc { get;set;}
        public int Id { get;set; }

        public override string ToString()
        {
            return ImageSrc;
        }
    }
}
