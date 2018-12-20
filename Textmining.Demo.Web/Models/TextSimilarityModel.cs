using System.ComponentModel;

namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// این کلاس برای تنظیم پارامترهای مورد نیاز تابع تعیین شباهت کاراکتری دو رشته (توالی کاراکترها) یا جمله (توالی کلمات/توکنها) طراحی شده است
    /// </summary>
    public class TextSimilarityModel
    {
        public TextSimilarityModel()
        {
            DistanceFunc = WordComparisonOptions.LevenshteinDistance; 
            MinDistThreshold = 0.0;
        }

        /// <summary>
        /// کلمه یا رشته ورودی اول
        /// </summary>
        public string String1 { get; set; }

        /// <summary>
        /// کلمه یا رشته ورودی دوم
        /// </summary>
        public string String2 { get; set; }

        /// <summary>
        /// نحوه محاسبه شباهت (کاراکتری) دو رشته
        /// <para>this parameter used in 'GetSyntacticDistance', 'SentenceSimilarityBipartiteMatching', 'SentenceSimilarityWithIntersectionMatching', 'SentenceSimilarityWithNGramMatching' functions</para>
        /// <para>
        /// <list type="bullet">
        /// <item>
        /// <description>HammingDistance (1)</description>
        /// </item>
        /// <item>
        /// <description>JaccardDistance (2)</description>
        /// </item>
        /// <item>
        /// <description>JaroDistance (4)</description>
        /// </item>
        /// <item>
        /// <description>JaroWinklerDistance (8)</description>
        /// </item>
        /// <item>
        /// <description>LevenshteinDistance (16)</description>
        /// </item>
        /// <item>
        /// <description>LongestCommonSubsequence (32)</description>
        /// </item>
        /// <item>
        /// <description>LongestCommonSubstring (64)</description>
        /// </item>
        /// <item>
        /// <description>NormalizedLevenshteinDistance (128)</description>
        /// </item>
        /// <item>
        /// <description>OverlapCoefficient (256)</description>
        /// </item>
        /// <item>
        /// <description>RatcliffObershelpSimilarity (512)</description>
        /// </item>
        /// <item>
        /// <description>SorensenDiceDistance (1024)</description>
        /// </item>
        /// <item>
        /// <description>TanimotoCoefficient (2048)</description>
        /// </item>
        /// </list>
        /// </para>
        /// </summary>
        public WordComparisonOptions DistanceFunc { get; set; }

        /// <summary>
        /// حداقل فاصله دو کلمه برای انطباق (یکسان فرض نمودن) آنها
        /// <para>this parameter used in 'SentenceSimilarityWithIntersectionMatching', 'SentenceSimilarityWithNGramMatching' functions</para> 
        /// </summary>
        [DefaultValue(0)]
        public double MinDistThreshold { get; set; }
    }
}