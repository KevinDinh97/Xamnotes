using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamnotes.Models
{
    public class Note
    {
        // Inhalt der Notiz
        public string Content { get; set; }
        // Datum der Notiz
        public DateTime DateTime { get; set; }
        // URI fuer picsum
        public ImageSource Image{ get; set;}

        public Note(string Content, DateTime DateTime, string ImgSource)
        {
            this.Content = Content;
            this.DateTime = DateTime;
            this.Image = ImageSource.FromUri(new Uri(ImgSource));
        }
    }
}
