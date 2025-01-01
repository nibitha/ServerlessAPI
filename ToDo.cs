using Amazon.DynamoDBv2.DataModel;

namespace ToDoApi
{
    [DynamoDBTable("ToDo")]
    public class ToDo
    {
        [DynamoDBHashKey("id")]
        public string id { get; set; }

        [DynamoDBProperty("name")]
        public string name { get; set; }

        [DynamoDBProperty("status")]
        public string status { get; set; }
    }
}
