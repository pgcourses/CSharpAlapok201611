using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02RestAPI
{

    /// <summary>
    /// Figyelem, a bonyolultabb API-khoz általában jár kliens oldali könyvtár. Pl:
    /// Stackoverflow: http://stackapps.com/questions/3411/stacman-net-client-for-stack-exchange-api-v2
    /// PayPal: https://github.com/paypal/PayPal-NET-SDK
    /// SendGrid: https://github.com/sendgrid/sendgrid-csharp
    /// Facebook-hoz használható az ASP.NET MVC identity: https://www.asp.net/mvc/overview/security/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            //Lekérdezéshez a RestSharp csomagot használjuk
            //http://restsharp.org/
            //forráskód a githubon: https://github.com/restsharp/RestSharp
            //nugettel telepíthető: https://www.nuget.org/packages/RestSharp

            var hostUrl = "http://jsonplaceholder.typicode.com/";

            var client = new RestClient(hostUrl);
            //használhatjuk az Uri segítségét is
            //var client = new RestClient(new Uri(hostUrl));

            var request = new RestRequest("posts", Method.GET);
            var result = client.Execute<List<Post>>(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            { //a Contentben a válasz, feldolgozás
                var posts = result.Data;
                Console.WriteLine("A postok száma: {0}", posts.Count);
                Console.WriteLine("Az első post id-je: {0}", posts[0].id);
                Console.WriteLine("Az első post userId-je: {0}", posts[0].userId);
            }
            else
            { //valami egyéb üzenettel válaszolt a szerver, ezt fel kell dolgozni
                Console.WriteLine("Szerver válasza: {0}, {1}", result.StatusCode, result.StatusDescription);
                //például: Szerver válasza: NotFound, Not Found
            }

            Console.WriteLine();
            var post1 = client.Execute<Post>(new RestRequest("posts/1", Method.GET));
            Console.WriteLine("Az id-je: {0}", post1.Data.id);
            Console.WriteLine("A userId-je: {0}", post1.Data.userId);
            Console.WriteLine("A title-je: {0}", post1.Data.title);
            Console.WriteLine("A body-ja: {0}", post1.Data.body);

            var commentsRequest = new RestRequest("comments", Method.GET);
            commentsRequest.AddParameter("postId", 2);

            Console.WriteLine();
            var comments = client.ExecuteAsync<List<Comment>>(commentsRequest, MegjottekAzAdatok);
            Console.WriteLine("Aszinkron kérés elindítva");

            Console.ReadLine();
        }

        private static void MegjottekAzAdatok(IRestResponse<List<Comment>> response, RestRequestAsyncHandle arg2)
        {
            foreach (var comment in response.Data)
            {
                Console.WriteLine("Comment érkezett tőle: {0}", comment.email);
            }
        }

        //{
        //  "userId": 1,
        //  "id": 1,
        //  "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
        //  "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
        //}

        public class Post
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }

        //  {
        //    "postId": 1,
        //    "id": 1,
        //    "name": "id labore ex et quam laborum",
        //    "email": "Eliseo@gardner.biz",
        //    "body": "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
        //  }


        public class Comment
        {
            public int postId { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string body { get; set; }
        }


    }
}
