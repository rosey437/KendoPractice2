using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KendoPractice2.Models;

namespace KendoPractice2.Models
{
    public class PeopleService
    {
        public List<PersonModel> GetPeople() 
        {
            JsonReadWrite readWrite = new JsonReadWrite();
            var dataJson = readWrite.Read("people.json", "data");

            if (string.IsNullOrEmpty(dataJson))
                return new List<PersonModel>();

            return JsonConvert.DeserializeObject<List<PersonModel>>(dataJson);
        }

        public PersonModel GetPerson(int id) 
        {
            var people = GetPeople();
            PersonModel person = people.FirstOrDefault(x => x.Id == id);

            return person;
        }


        public void SavePerson(PersonModel personModel) 
        {
            var people = GetPeople();

            PersonModel person = people.FirstOrDefault(x => x.Id == personModel.Id);

            if (person == null)
            {
                people.Add(personModel);
            }
            else
            {
                int index = people.FindIndex(x => x.Id == personModel.Id);
                people[index] = personModel;
            }

            WriteJson(people);            
        }

        public void DeletePerson(int id) 
        {
            List<PersonModel> people = GetPeople();
           
            int index = people.FindIndex(x => x.Id == id);
            people.RemoveAt(index);

            WriteJson(people);
        }

        private void WriteJson(List<PersonModel> people) 
        {
            var readWrite = new JsonReadWrite();

            string jSONString = JsonConvert.SerializeObject(people);
            readWrite.Write("people.json", "data", jSONString);
        }
    }
}
