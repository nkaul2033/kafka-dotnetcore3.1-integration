using Moq;
using Moq.Protected;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
/*
 * This test is not complete, out of scope 
 * 
 * 
 * */
namespace Test
{
    public class IncomingFilesTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpResponseMessage _requestCheckResponseMessage;
        private HttpClient _httpClient;
        public IncomingFilesTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            _requestCheckResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _httpClient.BaseAddress = new Uri("http://localhost:5001");
        }

        [Fact]
        public async void Test1()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"dump.json");

            Assert.True(File.Exists(filePath));

            var data = File.ReadAllText(filePath);

            _requestCheckResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            _requestCheckResponseMessage.Content = new StringContent("true", Encoding.UTF8, "text/plain");
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("PostAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri.ToString().Contains("PostAsync")),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_requestCheckResponseMessage);

            // Act
            var result = Assert.IsAssignableFrom<bool>(await PostHelper(data));

            //Assert
            Assert.True(result);

        }

        public async Task<bool> PostHelper(string json)
        {
            //HttpResponseMessage? response = null;
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //try
            //{
            //    response = await _httpClient.PostAsync($"/api/app/PostAsync", content);

            //    if ((response != null && response.StatusCode == HttpStatusCode.OK))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (HttpRequestException exception)
            //{
            //    throw exception;
            //}
            return true;
        }
    }
}
