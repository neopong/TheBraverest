using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace TheBraverest.Classes
{
    public class RESTHelpers
    {
        public static async Task<RESTResult<T>> RESTRequest<T>(string baseAddress, string relativePath, string apiKey, string extraGetVariables)
        {
            RESTResult<T> restResult = new RESTResult<T>();
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                string getVariables = "?api_key=" + apiKey;

                if (!extraGetVariables.IsNullOrWhiteSpace())
                {
                    if (!extraGetVariables.StartsWith("&"))
                    {
                        getVariables += "&";
                    }

                    getVariables += extraGetVariables;
                }
                
                HttpResponseMessage result = new HttpResponseMessage();

                result = await client.GetAsync(relativePath + getVariables);


                if (result.IsSuccessStatusCode)
                {
                    restResult.Success = true;

                    string resultContent = await result.Content.ReadAsStringAsync();


                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    T jsonResult = jss.Deserialize<T>(resultContent);

                    restResult.ReturnObject = jsonResult;
                }
                else
                {
                    restResult.Success = false;
                }

                restResult.StatusCode = result.StatusCode;

                return restResult;
            }
        }
    }
}