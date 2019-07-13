using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using Textmining.Demo.Web.Models;

namespace Textmining.Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor and Index Action
        private readonly string _urlPath = "https://api.text-mining.ir/api/";
        private readonly IConfiguration _config;
        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return RedirectToAction("SpellCorrector");
        }

        #endregion

        #region FormalConverter

        public IActionResult FormalConverter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FormalConverter(ApiViewModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}TextRefinement/FormalConverter", model.InputText);
            return PartialView("_ApiOutput");
        }

        #endregion

        #region SpellCorrector
        public IActionResult SpellCorrector()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SpellCorrector(SpellCheckerModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}TextRefinement/SpellCorrector", model);
            return PartialView("_ApiOutput");
        }

        #endregion

        #region SwwarWordTagger

        public IActionResult SwearWordTagger()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwearWordTagger(ApiViewModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}TextRefinement/SwearWordTagger", model.InputText);
            return PartialView("_ApiOutput");
        }

        #endregion

        #region SentimentClassifier
        public IActionResult SentimentClassifier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SentimentClassifier(ApiViewModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}SentimentAnalyzer/SentimentClassifier", model.InputText);
            return PartialView("_ApiOutput");
        }
        #endregion

        #region NamedEntityRecognitionDetect
        public IActionResult NamedEntityRecognitionDetect()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NamedEntityRecognitionDetect(ApiViewModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}NamedEntityRecognition/Detect", model.InputText);
            return PartialView("_ApiOutput");
        }
        #endregion

        #region LanguageDetectionPredict
        public IActionResult LanguageDetectionPredict()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LanguageDetectionPredict(ApiViewModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}LanguageDetection/Predict", model.InputText);
            return PartialView("_ApiOutput");
        }
        #endregion

        #region LemmatizeText2Text
        public IActionResult LemmatizeText2Text()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LemmatizeText2Text(ApiViewModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}Stemmer/LemmatizeText2Text", model.InputText);
            return PartialView("_ApiOutput");
        }
        #endregion

        #region GetMostSimilarWord
        public IActionResult GetMostSimilarWord()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetMostSimilarWord(GetMostSimilarWordModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}TextSimilarity/GetMostSimilarWord", model);
            return PartialView("_ApiOutput");
        }

        #endregion

        #region KeywordExtraction
        public IActionResult KeywordExtraction()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KeywordExtraction(KeywordExtractionModel model)
        {
            ViewData["Output"] = CallApi($"{_urlPath}InformationRetrieval/KeywordExtraction", model);
            return PartialView("_ApiOutput");
        }

        #endregion

        #region Common Methods   

        private string GetJWTToken()
        {
            var textMiningApiKey = _config.GetValue<string>("TextMiningApiKey");
            string jwtToken = string.Empty;
            var client = new RestClient($"{_urlPath}Token/GetToken?apikey={textMiningApiKey}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                jwtToken = (string)JObject.Parse(response.Content)["token"];
            }
            return jwtToken;
        }

        private string CallApi(string apiUrl, object inputModel)
        {
            try
            {
                //ToDo: check model validation
                var client = new RestClient(apiUrl);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Authorization", $"Bearer {GetJWTToken()}");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("input", ObjectModelToString(inputModel), ParameterType.RequestBody); //$"\"{inputText}\""
                IRestResponse response = client.Execute(request);
                return response.Content.Replace("\"", string.Empty);
            }
            catch (Exception)
            {
                //ToDo: better error message
                return "Error";
            }
        }

        private string ObjectModelToString(object model)
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return jsonStr;
        }


        #endregion
    }
}
