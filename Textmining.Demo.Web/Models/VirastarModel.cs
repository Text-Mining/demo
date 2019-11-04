using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    public class VirastarModel
    {
        [Required(ErrorMessage = "الزامی")]
        public string Text { get; set; }
        public string OutputText { get; set; }
    }
}
