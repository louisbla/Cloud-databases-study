using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Systemes_repartis
{
    class ParseConnection
    {
        public ParseConnection()
        {
            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = "NP26N2nAa6JLyasVy9BawQVg1AhbUmHjOC1I354T",
                WindowsKey = "9LVvXxzeUQp8EtUJPkw2gOW1PZw4SBnfYrwozz9a",
                Server = "https://parseapi.back4app.com"
            });
        }

        public async System.Threading.Tasks.Task insertAsync()
        {
            ParseObject myFirstClass = new ParseObject("MyFirstClass");
            myFirstClass["name"] = "I'm able to save objects!";
            await myFirstClass.SaveAsync();
        }

    }
}
