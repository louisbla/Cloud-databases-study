using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Systemes_repartis
{
    class FirebaseConnection
    {
        private const String databaseUrl = "https://systemes-repartis.firebaseio.com/persons";

        private const String databaseSecret = "10TvmIOk5YdlSbilUNmE7Hzf7SlIuh8Ye6tdcuDP";




        private FirebaseClient firebase;



        public FirebaseConnection()
        {
            this.firebase = new FirebaseClient(

                databaseUrl,

                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)

                });

        }
        

        public async Task Insert(Person person, string key)
        {
            await firebase.Child(key).PutAsync(person); //PostAsync<Person>(person);
        }

        public async Task<Person> GetPersonFromKey(string key)
        {
            return await firebase.Child(key).OnceSingleAsync<Person>();
        }

        public async Task DeletePersonFromKey(string key)
        {
             await firebase.Child(key).DeleteAsync();
        }

    }
}

