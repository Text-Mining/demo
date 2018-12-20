using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای تنظیم پارامترهای مورد نیاز تابع بررسی اشتباهات تایپی/املایی طراحی شده است
    /// </summary>
    public class SpellCheckerModel
    {
        public SpellCheckerModel()
        {
            Normalize = true;
            CandidateCount = 3;
        }

        [Newtonsoft.Json.JsonIgnore]
        public string OutputText { get; set; }

        /// <summary>
        /// متن ورودی
        /// </summary>
        /// <value>The text.</value>
        [Required(ErrorMessage = "الزامی")]
        public string Text { get; set; }

        /// <summary>
        /// آیا نرمالسازی متن نیز انجام شود
        /// </summary>
        /// <value><c>true</c> if normalize; otherwise, <c>false</c>.</value>
        [DefaultValue(true)]
        public bool Normalize { get; set; }

        /// <summary>
        /// تعداد کلمات کاندید برای جایگزین کردن با کلمه ناآشنای درون متن
        /// </summary>
        [DefaultValue(3)]
        public int CandidateCount { get; set; }
    }
}
