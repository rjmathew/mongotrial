using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace mongoInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
        public class MongoEntity:IMongoEntity
        {
            [BsonId]
            public ObjectId Id { get; set; }
        }
        [BsonIgnoreExtraElements]
        public class Game : MongoEntity
        {
            public Game()
            {
                Categories = new List<string>();
            }

            [BsonElement("name")]
            public string Name { get; set; }

            [BsonElement("release_date")]
            public DateTime ReleaseDate { get; set; }

            [BsonElement("categories")]
            public List<string> Categories { get; set; }

            [BsonElement("played")]
            public bool Played { get; set; }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var connectionString = "mongodb://rajeshdisney:rajesh12@ds031982.mongolab.com:31982/retrogames";

            //// Get a thread-safe client object by using a connection string
            var mongoClient = new MongoClient(connectionString);

            //// Get a reference to a server object from the Mongo client object
            var mongoServer = mongoClient.GetServer();

            //// Get a reference to the "retrogames" database object from the Mongo server object
            var databaseName = "retrogames";
            var db = mongoServer.GetDatabase(databaseName);

            //// Get a reference to the "games" collection object from the Mongo database object
            var games = db.GetCollection<Game>("games");

            var gameQuery = Query<Game>.EQ(g => g.Name, "Invaders 2013");
            var foundGame = games.FindOne(gameQuery);
            games.Insert<Game>(new Game { Name="test2",
                Categories = new List<string> { "a","b"},
                ReleaseDate =DateTime.Now.AddDays(-3)});

        }
    }
}
