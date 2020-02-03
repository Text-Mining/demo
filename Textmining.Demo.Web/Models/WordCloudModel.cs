using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    public class WordCloudModel
    {
        [Required(ErrorMessage = "الزامی")]
        public string Text { get; set; }

        public string Theme { get; set; } // = "CoolBlackColorTheme";
    }
}