using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserToDoBL.Model;
using UserToDoBL.Service;
using UserToDoBL.ViewModel;

namespace UserToDoBatchJob
{
    public static class UserToDoHelper
    {
        const string BaseUrl = "https://jsonplaceholder.typicode.com";
        const string ApiEndPoint = "/todos/";

        public static async Task GetToDoList()
        {
            await GetData();
        }

        private static async Task GetData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var response = client.GetAsync(ApiEndPoint).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                await ParseJsonResponse(jsonResponse);
            };
        }

        private static async Task ParseJsonResponse(string json)
        {
            ToDoList toDoList = new ToDoList()
            {
                ToDosList = new List<ToDoViewModel>()
            };
            toDoList.ToDosList = JsonConvert.DeserializeObject<List<ToDoViewModel>>(json);
            await UserToDoService.PostToDoList(toDoList);
        }
    }
}
