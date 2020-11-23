using StartProject.Enums;
using StartProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace StartProject.Helper
{
    public class ApiHelper
    {

        /// <summary>
        /// Post 等待回傳
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="param"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public static T Post<T>(string ApiUrl, string param = null, ContentTypeEnum ContentType = ContentTypeEnum.json)
        {
            try
            {
                HttpClient ClientF = new HttpClient();
                var httpContent = new StringContent(param, Encoding.UTF8);

                switch (ContentType)
                {
                    case ContentTypeEnum.urlencoded:
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                        break;
                    case ContentTypeEnum.json:
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        break;
                }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (HttpResponseMessage responseMessage = ClientF.PostAsync(ApiUrl, httpContent).GetAwaiter().GetResult())
                {
                    return JsonSerializer.Deserialize<T>(responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return default(T);
        }


        /// <summary>
        /// Get 等待回傳
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="param"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public static T Get<T>(string ApiUrl)
        {
            try
            {
                HttpClient ClientF = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (HttpResponseMessage responseMessage = ClientF.GetAsync(ApiUrl).GetAwaiter().GetResult())
                {
                    return JsonSerializer.Deserialize<T>(responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return default(T);
        }


        /// <summary>
        /// Get 等待回傳
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <typeparam name="TT">obj, model</typeparam>
        /// <param name="ApiUrl"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Get<T, TT>(string ApiUrl, TT obj)
        {
            try
            {
                string param = string.Empty;
                var properties = obj.GetType().GetProperties();

                foreach (var p in properties)
                {
                    string name = p.Name;
                    var value = p.GetValue(obj, null);

                    if (value != null && value.GetType().Name == "DateTime")
                    {
                        value = Convert.ToDateTime(value).ToString("s");
                    }

                    param += (param == "") ? name + "=" + value : "&" + name + "=" + value;
                }

                if (!string.IsNullOrWhiteSpace(param))
                {
                    ApiUrl = ApiUrl + "?" + param;
                }


                HttpClient ClientF = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (HttpResponseMessage responseMessage = ClientF.GetAsync(ApiUrl).GetAwaiter().GetResult())
                {
                    return JsonSerializer.Deserialize<T>(responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return default(T);
        }


    }
}
