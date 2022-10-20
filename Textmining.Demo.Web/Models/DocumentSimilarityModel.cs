using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Textmining.Demo.Web.Models
{
    public class DocumentSimilarityModel
    {
        /// <summary>
        /// جمله یا متن ورودی اول
        /// </summary>
        [Required(ErrorMessage = "الزامی")]
        public string Document1 { get; set; }

        /// <summary>
        /// جمله یا متن ورودی دوم
        /// </summary>
        [Required(ErrorMessage ="الزامی")]
        public string Document2 { get; set; }

        /// <summary>
        /// برای تشابه بین کلمات از ریشه یاب استفاده شود
        /// </summary>
        [DefaultValue(true)]
        public bool UseStemming { get; set; }

        /// <summary>
        /// برای محاسبه تشابه متون به کلمات هم معنی توجه شود
        /// </summary>
        [DefaultValue(true)]
        public bool UseSynonyms { get; set; }

        /// <summary>
        /// برای محاسبه تشابه متون به کلمات هم کاربرد (مشابهت در مدل زبانی) توجه شود
        /// </summary>
        [DefaultValue(true)]
        public bool UseStatisticalSimilarity { get; set; }

        /// <summary>
        /// از ویراستاری (اصلاح کلمات بهم چسبیده و ...) برای تعیین شباهت استفاده شود
        /// </summary>
        [DefaultValue(true)]
        public bool UseVirastar { get; set; }

        /// <summary>
        /// از غلط یابی برای تعیین شباهت استفاده شود
        /// </summary>
        [DefaultValue(false)]
        public bool UseSpellChecker { get; set; }
    }
}
