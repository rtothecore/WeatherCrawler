using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public class DbManager
    {
        private string Ip = null;
        private string DbName = null;
        private string CollectionName = null;
        private string Id = null;
        private string Pw = null;
        private MongoClient DbClient = null;
        private IMongoDatabase Db = null;
        // private IMongoCollection<BsonDocument> collection = null;

        public DbManager(string ip, string dbName, string collectionName, string id, string pw)
        {
            this.Ip = ip;
            this.DbName = dbName;
            this.CollectionName = collectionName;
            this.Id = id;
            this.Pw = pw;
        }

        public bool Connect()
        {
            try
            {
                /* ORIGINAL
                DbClient = new MongoClient
                (
                    // https://blog.oz-code.com/how-to-mongodb-in-c-part-2/
                    new MongoClientSettings
                    {
                        Server = new MongoServerAddress(Ip, 27017),
                        ServerSelectionTimeout = TimeSpan.FromSeconds(3)
                    }
                );
                */

                // https://stackoverflow.com/questions/27747503/mongodb-can-connect-from-mongo-client-but-not-from-c-sharp-driver
                string mongoDbAuthMechanism = "SCRAM-SHA-1";
                MongoInternalIdentity internalIdentity = new MongoInternalIdentity(DbName, Id);
                PasswordEvidence passwordEvidence = new PasswordEvidence(Pw);
                MongoCredential mongoCredential = new MongoCredential(mongoDbAuthMechanism, internalIdentity, passwordEvidence);

                MongoClientSettings settings = new MongoClientSettings();
                settings.Credential = mongoCredential;
                MongoServerAddress address = new MongoServerAddress(Ip, 27017);
                settings.Server = address;

                DbClient = new MongoClient(settings);

                Db = DbClient.GetDatabase(DbName);
                var collections = Db.ListCollections().ToList();
                foreach (var item in collections)
                {
                    // Console.WriteLine(item);
                }
            }
            catch(System.TimeoutException e)
            {
                Console.WriteLine("TimeoutException : {0}", e.Message);
                return false;
            }

            return true;
        }

        public List<string> ReadFwjournalLands()
        {
            IMongoCollection<BsonDocument> collection = Db.GetCollection<BsonDocument>("lands");
            var filter = new BsonDocument();
            List<string> distinctResult = collection.Distinct<string>("address", filter).ToList();

            for(int i = 0; i < distinctResult.Count; i++)
            {
                distinctResult[i] = UtilManager.TrimAddress(distinctResult[i]);
            }

            return distinctResult;
        }

        public void InsertWeatherData(WeatherCrawlerData wcd)
        {
            IMongoCollection<BsonDocument> collection = Db.GetCollection<BsonDocument>("wcdatas");
            string text = JsonConvert.SerializeObject(wcd);
            var document = BsonSerializer.Deserialize<BsonDocument>(text);
            collection.InsertOneAsync(document);
        }

        public bool IsExistAddress(string address)
        {
            IMongoCollection<BsonDocument> collection = Db.GetCollection<BsonDocument>("wcdatas");
            var filter = new BsonDocument("address", address);

            List<BsonDocument> result = collection.Find(filter).ToList();
            if (0 < result.Count)
            {
                return true;
            }
            return false;
        }

        public List<CurrentData> GetCurrentData(string address)
        {
            var collection = Db.GetCollection<WeatherCrawlerData>("wcdatas");
            // https://stackoverflow.com/questions/7704290/get-only-a-specified-field
            var results = collection.Find(Builders<WeatherCrawlerData>.Filter.Eq(wcd => wcd.address, address)).Project(u => new { u.currentData }).ToList();           
            return results[0].currentData;
        }

        public void DeleteDocumentByAddress(string address)
        {
            var collection = Db.GetCollection<WeatherCrawlerData>("wcdatas");
            // https://stackoverflow.com/questions/8867032/how-to-remove-one-document-by-id-using-the-official-c-sharp-driver-for-mongo
            collection.DeleteOne(a => a.address == address);
        }
    }
}
