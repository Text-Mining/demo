using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Textmining.Demo.Web.Models;
using NToastNotify;

namespace Textmining.Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor and Index Action
        private readonly string _urlPath = "https://api.text-mining.ir/api/";
        private readonly IConfiguration _config;
        private readonly IToastNotification _toastNotification;
        private static int _feedback;

        public HomeController(IConfiguration config, IToastNotification toastNotification)
        {
            _config = config;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Virastar"); //SpellCorrector
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

        #region SwearWordTagger

        public IActionResult SwearWordTagger()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwearWordTagger(ApiViewModel model)
        {
            var apiOutput = CallApi($"{_urlPath}TextRefinement/SwearWordTagger", model.InputText);

            var viewModel = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiOutput);

            return PartialView("_SwearTaggerOutput", viewModel);
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
            var apiOutput = CallApi($"{_urlPath}SentimentAnalyzer/SentimentClassifier", model.InputText);

            var viewModel = JsonConvert.DeserializeObject<int>(apiOutput);

            return PartialView("_SentimentClassifierOutput", viewModel);
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
            var apiOutput = CallApi($"{_urlPath}NamedEntityRecognition/Detect", model.InputText);

            var viewModel = JsonConvert.DeserializeObject<List<Phrase>>(apiOutput);

            return PartialView("_NamedEntityRecognitionDetectOutput", viewModel);
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
            //ViewData["Output"] = CallApi($"{_urlPath}TextSimilarity/GetMostSimilarWord", model);
            //return PartialView("_ApiOutput");
            var result = CallApi($"{_urlPath}TextSimilarity/GetMostSimilarWord", model);
            var viewModel = JsonConvert.DeserializeObject<string[]>(result);
            return PartialView("_GetMostSimilarWordOutput", viewModel);
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
            model.MinWordLength = 3;
            model.MaxWordCount = 3;
            model.MinKeywordFrequency = 1;
            model.Method = "TFIDF";

            var apiOutput = CallApi($"{_urlPath}InformationRetrieval/KeywordExtraction", model);

            var viewModel = JsonConvert.DeserializeObject<Dictionary<string, double>>(apiOutput);

            return PartialView("_KeywordExtractionOutput", viewModel);
        }

        #endregion

        #region Virastar
        public IActionResult Virastar()
        {
            CheckFeedbackNotif();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Virastar(VirastarModel model)
        {
            var result = CallApi($"{_urlPath}Virastar/ScanText", model);

            var viewModel = JsonConvert.DeserializeObject<List<TokenInfo>>(result);

            return PartialView("_VirastarOutput", viewModel);
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
                request.AddParameter("input", ObjectModelToString(inputModel), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception)
            {
                //ToDo: better error message
                return "Error";
            }
        }

        private string ObjectModelToString(object model)
        {
            string jsonStr = JsonConvert.SerializeObject(model);
            return jsonStr;
        }


        #endregion

        #region Report Error 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ReportError(string errorReportInputText, string errorReportComment)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(_config.GetValue<string>("ErrorReport:SmtpHost"))
                {
                    Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("ErrorReport:SmtpUsername"),
                        _config.GetValue<string>("ErrorReport:SmtpPassword"))
                    //,Timeout = 30000
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(_config.GetValue<string>("ErrorReport:SmtpUsername")),
                    IsBodyHtml = true,
                    Subject = "گزارش خطا در دمو ابزارهای فارسی‌یار"
                };
                mail.To.Add(_config.GetValue<string>("ErrorReport:ReportEmail"));
                mail.Body = $"<p dir='rtl'>" +
                            $"ورودی: {errorReportInputText} " +
                            $"<br/> " +
                            $"توضیحات:{errorReportComment}" +
                            $"<br/> " +
                            $"آدرس ابزار:{Request.Headers["Referer"].ToString()}" +
                            $"</p>";

                //smtp.Send(mail);
                smtp.SendCompleted += (s, e) => {
                    smtp.Dispose();
                    mail.Dispose();
                };
                smtp.SendAsync(mail, null);
                _feedback = 1;
            }
            catch //(Exception ex)
            {
                _feedback = 2;
            }

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            return RedirectToAction("Index");
        }

        private void CheckFeedbackNotif()
        {
            if(_feedback == 1)
                _toastNotification.AddSuccessToastMessage("با تشکر از همکاری شما",
                    new ToastrOptions()
                    {
                        Title = "ارسال موفق گزارش"
                    });
            else if(_feedback == 2)
                _toastNotification.AddErrorToastMessage("خطایی در ارسال گزارش به وجود آمد",
                    new ToastrOptions()
                    {
                        Title = "خطا"
                    });

            _feedback = 0;
        }

        #endregion
    }
}
