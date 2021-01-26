using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    public class VirastarModel
    {
        [Required(ErrorMessage = "الزامی")]
        public string Text { get; set; }

        public int SpellCheckerCandidateCount { get; set; } = 3;

        public bool CheckRealWordSpell { get; set; } = true;

        public bool SpellCheckHighSensitive { get; set; } = true;

        public string OutputText { get; set; }
    }
}
