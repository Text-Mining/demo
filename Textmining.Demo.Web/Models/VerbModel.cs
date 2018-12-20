namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// To represent an instance of a verb with its details
    /// </summary>
    public class VerbModel
    {
        /// <summary>
        /// Shows the preposition of the verb in the cases of compound verbs with prepositional phrases
        /// </summary>
        public string PrepositionOfVerb;

        /// <summary>
        /// non-verbal element
        /// </summary>
        public string NonVerbalElement;

        /// <summary>
        /// Prefix of the verb
        /// </summary>
        public string Prefix;

        /// <summary>
        /// Past tense root of the verb (in Persian there are two types of root for each verb; i.e. present tense and past tense
        /// </summary>
        public string PastTenseRoot;

        /// <summary>
        /// Present tense root of the verb (in Persian there are two types of root for each verb; i.e. present tense and past tense
        /// </summary>
        public string PresentTenseRoot;

        /// <summary>
        /// Shows whether a verb is Transitive or not
        /// </summary>
        public VerbTransitivity Transitivity;

        /// <summary>
        /// Shows verb type; i.e. 
        /// </summary>
        public VerbType Type;

        /// <summary>
        /// shows whether the verb can be used in imperative form or not
        /// </summary>
        public bool CanBeImperative;

        /// <summary>
        /// Shows the type of vowel at the end of the present tense root
        /// </summary>
        public string PresentRootConsonantVowelEndStem;

        /// <summary>
        /// Shows the type of vowel at the start of the past tense root
        /// </summary>
        public string PastRootVowelStart;

        /// <summary>
        /// Shows the type of vowel at the start of the present tense root
        /// </summary>
        public string PresentRootVowelStart;
    }
}
