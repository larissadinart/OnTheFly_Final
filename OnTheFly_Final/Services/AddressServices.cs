using System.Collections.Generic;
using System.IO;
using System.Net;
using MongoDB.Driver;
using OnTheFly_Final.Models;
using OnTheFly_Final.Utils;

namespace OnTheFly_Final.Services
{
    public class AddressServices
    {
        private readonly IMongoCollection<Address> _address;
        public AddressServices(IDataBaseSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.PassengerDataBaseName); //objeto vazio
            _address = database.GetCollection<Address>(settings.AddressCollectionName);

        }
        public string GetAddress(string cep) //método utilizado para executar uma requisição web
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + cep + "/json/"); //url
            request.AllowAutoRedirect = false;
            HttpWebResponse verificaServidor = (HttpWebResponse)request.GetResponse();
            Stream stream = verificaServidor.GetResponseStream();
            if (stream == null) return null;
            StreamReader answerReader = new StreamReader(stream);
            string message = answerReader.ReadToEnd();
            return message;
        }
        public Address Create(Address address)
        {
            _address.InsertOne(address);
            return address;
        }
        public List<Address> Get() => _address.Find(address => true).ToList();
        public void Update(string ID, Address AddressIN)
        {
            _address.ReplaceOne(address => address.ZipCode == ID, AddressIN);
        }
        public void Remove(Address AddressIN) => _address.DeleteOne(address => address.ZipCode == address.ZipCode);
    }
}
