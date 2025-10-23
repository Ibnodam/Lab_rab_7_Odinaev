using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using ModernHttpClient;

namespace GrClient.DataAccess
{
    public class Query
    {
        private static GraphQLHttpClient graphQLHttpClient;

        static Query()
        {
            var uri = new Uri("https://localhost:7213/graphql");
            var graphQLOptions = new GraphQLHttpClientOptions
            {
                EndPoint = uri,
                HttpMessageHandler = new NativeMessageHandler(),
            };
            graphQLHttpClient = new GraphQLHttpClient(graphQLOptions, new NewtonsoftJsonSerializer());
        }

        public static async Task<List<T>> ExecuteQueryReturnListAsync<T>(string graphQLQueryType, string completeQueryString)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = completeQueryString
                };
                var response = await graphQLHttpClient.SendQueryAsync<object>(request);
                var stringResult = response.Data.ToString();
                stringResult = stringResult!.Replace($"\"{graphQLQueryType}\":", string.Empty);
                stringResult = stringResult.Remove(0, 1);
                stringResult = stringResult.Remove(stringResult.Length - 1, 1);
                var result = System.Text.Json.JsonSerializer.Deserialize<List<T>>(stringResult);
                return result!;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<T> ExecuteQueryAsync<T>(string graphQLQueryType, string completeQueryString)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = completeQueryString
                };
                var response = await graphQLHttpClient.SendQueryAsync<object>(request);
                var stringResult = response.Data.ToString();
                stringResult = stringResult!.Replace($"\"{graphQLQueryType}\":", string.Empty);
                stringResult = stringResult.Remove(0, 1);
                stringResult = stringResult.Remove(stringResult.Length - 1, 1);
                var result = System.Text.Json.JsonSerializer.Deserialize<T>(stringResult);
                return result!;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    // Response models (оставляем те же)
    public class Train
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }

    public class Passenger
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class FreeSeatInfo
    {
        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }
        public string WagonType { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class PassengerTrip
    {
        public string TrainNumber { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class PassengerInfo
    {
        public string FullName { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}