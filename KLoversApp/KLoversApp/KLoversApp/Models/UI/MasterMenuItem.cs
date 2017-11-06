using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KLoversApp.Models.UI
{
    public class MasterMenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Color BackgroundColor { get; set; }
        public Type TargetType { get; set; }

        public MasterMenuItem(string title, string iconSource, Color backgroundColor, Type targetType)
        {
            this.Title = title;
            this.IconSource = iconSource;
            this.BackgroundColor = backgroundColor;
            this.TargetType = targetType;
        }

    }
}
