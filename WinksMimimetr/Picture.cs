using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinksMimimetr
{
    
        public class Picture
        {
            public int Id { get; set; }           
            public Image Pic { get; set; }

            public void SetPicFromBlob(Stream blob)
            {
                Pic = Image.FromStream(blob);
            }
        
    }
}
