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
        private readonly IConfiguration _config;
        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormalConverter()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult FormalConverter(ApiViewModel model)
        {
            model.OutputText = CallApi("http://api.text-mining.ir/api/TextRefinementController/FormalConverter", model.InputText);
            return View(model);
        }

        public IActionResult SpellCorrector()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SpellCorrector(SpellCheckerModel model)
        {
            model.OutputText = CallApi("http://api.text-mining.ir/api/TextRefinementController/SpellCorrector", model);
            return View(model);
        }

        private string GetJWTToken()
        {
            var textMiningApiKey = _config.GetValue<string>("TextMiningApiKey");
            string jwtToken = string.Empty;
            var client = new RestClient($"http://api.text-mining.ir/api/Token/GetToken?apikey={textMiningApiKey}");
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
            catch (Exception ex)
            {
                //ToDo: better error message
                return $"Error: {ex.Message}";
            }
        }

        private string ObjectModelToString(object model)
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return jsonStr;
        }
    }
}
