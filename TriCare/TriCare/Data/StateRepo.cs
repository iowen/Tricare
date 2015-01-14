using Newtonsoft.Json;
using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TriCare.Models;
using Xamarin.Forms;

namespace TriCare.Data
{
    public class StateRepo
    {
        protected SQLiteConnection database;
		private int isCreated;
        public StateRepo()
        {
            database = DependencyService.Get<ISqLite>().GetConnection();
            database.CreateTable<State>();
        }
		public bool InsertRecords()
		{
			try
			{
			var ic = (from i in database.Table<State>() select i).ToList();
				return ic.Count <= 0;
			}
			catch(Exception ex) {
				return true;
			}

		}
		public string GetStates()
		{
			var states = new List<string> () {"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky",
				"Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
				"New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont",
				"Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"
			};
           
			foreach (var item in states) {
				var state = new State (){ Name = item };
				database.Insert (state);
			}
			return "success";
            
		}

        public List<State> GetAllStates()
        {
            return (from i in database.Table<State>() select i).ToList();
        }

        public State GetState(int id)
        {
            return database.Table<State>().FirstOrDefault(x => x.StateId == id);
        }

        public int AddState(State item)
        {
            return database.Insert(item);
        }

        public int DeleteState(int id)
        {
            return database.Delete<State>(id);
        }
    }
}
