using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace Firefight
{
    class Person
    {
        public async Task ReadAsync(String PARSE_OBJECT_ID)
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
            ParseObject result = await query.GetAsync(PARSE_OBJECT_ID);
            // Use the Get<T> method to get the values
            Console.WriteLine(result.Get<string>("Name"));
            Console.WriteLine(result.Get<int>("Age"));
        }

        public async Task UpdateAsync(String PARSE_OBJECT_ID)
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
            ParseObject myObject = await query.GetAsync("<PARSE_OBJECT_ID>");
            myObject["Name"] = "A string";
            myObject["Age"] = 1;
            await myObject.SaveAsync();
        }
    }
}
