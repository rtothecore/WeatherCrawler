using MongoDB.Bson;
using MongoDB.Driver;
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
                DbClient = new MongoClient
                (
                    // https://blog.oz-code.com/how-to-mongodb-in-c-part-2/
                    new MongoClientSettings
                    {
                        Server = new MongoServerAddress(Ip, 27017),
                        ServerSelectionTimeout = TimeSpan.FromSeconds(3)
                    }
                );

                Db = DbClient.GetDatabase(DbName);
                var collections = Db.ListCollections().ToList();
                foreach (var item in collections)
                {
                    Console.WriteLine(item);
                }
                
                /*
                var dbList = DbClient.ListDatabases().ToList();
                Console.WriteLine("The list of databases are :");
                foreach (var item in dbList)
                {
                    Console.WriteLine(item);
                }
                */
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
    }
}
