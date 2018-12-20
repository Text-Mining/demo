namespace Textmining.Demo.Web.Models
{
    /// <summary>
    /// Represents an instance of an inflected verb
    /// </summary>
    public class VerbInflectionModel
    {
        public string NonVerbalElement;

        /// <summary>
        /// verb root
        /// </summary>
        public VerbModel VerbRoot;

        /// <summary>
        /// Type of the attached pronoun
        /// </summary>
        public AttachedPronounType ZamirPeyvasteh;

        /// <summary>
        /// String representation of the attached pronoun
        /// </summary>
        public string AttachedPronounString;

        /// <summary>
        /// Person type of the verb inflection
        /// </summary>
        public PersonType Person;

        /// <summary>
        /// Represents the formation of the tense
        /// </summary>
        public TenseFormationType TenseForm;

        /// <summary>
        /// Positive/Negative tense
        /// </summary>
        public TensePositivity Positivity;

        /// <summary>
        /// Active/Passive tense
        /// </summary>
        public TensePassivity Passivity;

        public bool IsSingular
        {
            get
            {
                return Person == PersonType.FIRST_PERSON_SINGULAR ||
                       Person == PersonType.SECOND_PERSON_SINGULAR ||
                       Person == PersonType.THIRD_PERSON_SINGULAR;
            }
        }

        public bool IsPlural
        {
            get
            {
                return Person == PersonType.FIRST_PERSON_PLURAL ||
                       Person == PersonType.SECOND_PERSON_PLURAL ||
                       Person == PersonType.THIRD_PERSON_PLURAL;
            }
        }

        public bool IsGozashteh
        {
            get
            {
                return TenseForm == TenseFormationType.GOZASHTEH_ABAD ||
                       TenseForm == TenseFormationType.GOZASHTEH_BAEED ||
                       TenseForm == TenseFormationType.GOZASHTEH_ELTEZAMI ||
                       TenseForm == TenseFormationType.GOZASHTEH_ESTEMRAARI ||
                       TenseForm == TenseFormationType.GOZASHTEH_MOSTAMAR ||
                       TenseForm == TenseFormationType.GOZASHTEH_NAGHLI_ESTEMRAARI ||
                       TenseForm == TenseFormationType.GOZASHTEH_NAGHLI_SADEH ||
                       TenseForm == TenseFormationType.GOZASHTEH_SADEH;
            }
        }

        public bool IsHAAL
        {
            get
            {
                return TenseForm == TenseFormationType.HAAL_ELTEZAMI ||
                       TenseForm == TenseFormationType.HAAL_MOSTAMAR ||
                       TenseForm == TenseFormationType.HAAL_SAADEH ||
                       TenseForm == TenseFormationType.HAAL_SAADEH_EKHBARI;
            }
        }
    }
}
