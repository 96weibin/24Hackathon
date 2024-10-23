using knowledgeBase.DAL;
using knowledgeBase.DataContract;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace knowledgeBase.Controllers
{
    [RoutePrefix("api/GraphQL")]
    public class GraphQLController : ApiController
    {
        private readonly ILogger<GraphQLController> _logger;

        public GraphQLController()
        {

        }

        public GraphQLController(ILogger<GraphQLController> logger)
        {
            _logger = logger;
        }

        private async Task<string> _Execute(string modelName, string queryContent)
        {
            string requestUrl = UpdateUrl(modelName);
            var username = "Administrator";
            var password = "Aspen100";
            // GraphQL 查询
            var query = $"{queryContent}";
            // 创建请求负载
            var payload = new
            {
                query = query,
                operationName = ""
            };
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            using (var handler = new HttpClientHandler
            {
                Credentials = new System.Net.NetworkCredential(username, password)
            })
            using (var client = new HttpClient(handler))
            {
                // 设置请求头
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    // 发起 POST 请求
                    var response = await client.PostAsync(requestUrl, content);
                    // 确保请求成功
                    if (response.IsSuccessStatusCode)
                    {
                        // 获取并返回响应内容
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent; // 返回给前端
                    }
                    else
                    {
                        // 请求失败时返回错误状态码及消息
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return errorContent;
                    }
                }
                catch (Exception ex)
                {
                    // 返回捕获的异常消息
                    return $"An error occurred: {ex.Message}";
                }
            }
        }

        [Route("test")]
        [HttpPost]
        public async Task<IHttpActionResult> test(string queryContent)
        {
            //string requestUrl = UpdateUrl(modelName);
            string requestUrl = $"http://psc-w2022-ch.qae.aspentech.com/aspentech/aspenunified/api/v1/model/Gulf%20Coast/graphql";

            var encoding = Encoding.UTF8;
            var content = $"{{'query':'{queryContent}', 'variables': null, 'operationName': null}}";
            _logger.LogInformation($"Executing GraphQL content {content}");
            byte[] bs = encoding.GetBytes(content);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.ContinueTimeout = 240 * 1000;
            request.Method = "POST";

            // input user name and pass work (corp)
            request.Credentials = new NetworkCredential("Administrator", "Aspen100");
            request.KeepAlive = false;
            request.ContentType = "application/json";
            request.ContentLength = bs.Length;
            request.AllowAutoRedirect = true;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                using (Stream resStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(resStream, encoding))
                    {
                        string result = await reader.ReadToEndAsync();
                        _logger.LogInformation($"The Response is {result}");
                        return Ok(result);
                    }
                }
            }
        }

        // Construct URL
        private string UpdateUrl(string modelName)
        {
            //_logger.LogInformation($"Current input name is {modelName}");
            string actualUrl = $"http://psc-w2022-ch.qae.aspentech.com/aspentech/aspenunified/api/v1/model/{modelName}/graphql";
            return actualUrl;
        }

        [Route("Execute")]
        [HttpPost]
        public async Task<IHttpActionResult> ExecuteGraphQL([FromBody] GraphQLRequest request)
        {
            if (string.IsNullOrEmpty(request.ModelName) || string.IsNullOrEmpty(request.QueryContent))
            {
                return BadRequest("Model name and query content are required");
            }
            try
            {
                string result = await _Execute(request.ModelName, request.QueryContent);
                return Ok(result);
            }
            catch (WebException ex)
            {
                _logger.LogError($"Error while executing GraphQL request: {ex.Message}");
                return InternalServerError(ex);
            }
        }


        public class GraphQLRequest
        {
            public string ModelName { get; set; }
            public string QueryContent { get; set; }
        }

        // 检查用户输入是否是有效的 GraphQL 查询
        private bool IsGraphQLQuery(string input)
        {
            return input.StartsWith("query") || input.StartsWith("mutation");
        }

        // 解析 GraphQL 查询中的 modelName
        private string ExtractModelNameFromInput(string input)
        {
            // 在这里实现从用户输入中解析 modelName 的逻辑
            // 假设用户输入是类似 "execute model <modelName> { query... }" 的字符串
            var parts = input.Split(' ');
            return parts.Length > 2 ? parts[2] : "defaultModel";  // 假设第三个部分是 modelName
        }

        // 解析 GraphQL 查询中的 queryContent
        private string GetPredefinedQuery(string input)
        {
            switch (input.ToLower())
            {
                case "Purchases":
                    return File.ReadAllText("");
                case "Sales":
                    return File.ReadAllText("");
                case "ProcessLimits":
                    return File.ReadAllText("");
                case "Capacity":
                    return File.ReadAllText("");
                default:
                    return string.Empty;
            }
        }



        private string _itemName;

        public class Type
        {
            public string Format
            {
                get;
                private set;
            }

            private Type(string format)
            {
                Format = format + ":\"\"";
            }

            public readonly static Type FromName = new Type("fromName");
            public readonly static Type ToName = new Type("toName");
            public readonly static Type PlantName = new Type("plantName");
            public readonly static Type JobName = new Type("name");
            public readonly static Type CaseName = new Type("name");
            public readonly static Type Field = new Type("field");

        }
    }
}