﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteamAppIdIdentifier
{
    public class DataTableGeneration
    {
        private DataTable dataTable;

        public DataTableGeneration() { }

        public async Task<DataTable> GetDataTableAsync(DataTableGeneration dataTableGeneration) {
            HttpClient httpClient = new HttpClient();
            string content = await httpClient.GetStringAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2/");
            SteamGames steamGames = JsonConvert.DeserializeObject<SteamGames>(content);

            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("AppId", typeof(int));

            foreach (var item in steamGames.Applist.Apps)
            {
                dt.Rows.Add(item.Name, item.Appid);
            }

            dataTableGeneration.DataTableToGenerate = dt;
            return dt;
        }

        #region Get and Set
        public DataTable DataTableToGenerate{
            get { return dataTable; }   // get method
            set { dataTable = value; }  // set method
        }
        #endregion

        #region JSON Properties
        public partial class SteamGames
        {
            [JsonProperty("applist")]
            public Applist Applist { get; set; }
        }

        public partial class Applist
        {
            [JsonProperty("apps")]
            public App[] Apps { get; set; }
        }

        public partial class App
        {
            [JsonProperty("appid")]
            public long Appid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
        #endregion
    }
}
