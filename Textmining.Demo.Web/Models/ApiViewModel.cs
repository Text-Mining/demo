
using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    public class ApiViewModel
    {
        [Required(ErrorMessage ="الزامی")]
        public string InputText { get; set; }
        public string OutputText { get; set; }
    }
}
