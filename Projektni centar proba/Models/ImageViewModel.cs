using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projektni_centar_proba.Models
{
    public class ImageViewModel
    {
        public byte[] FotoPecat { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}