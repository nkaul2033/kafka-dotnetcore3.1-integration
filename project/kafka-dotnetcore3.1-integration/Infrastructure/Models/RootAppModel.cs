using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class App
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
    }

    public class RootApp
    {
        public List<App> apps { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public int id { get; set; }
        public string last_name { get; set; }
    }

    public class Root
    {
        public List<RootApp> rootApps { get; set; }
    }


    //public static class RootAppsExtension
    //{
    //    public static IEnumerator<RootApp> getCustomEnumerator(this RootApp rootApp)
    //    {
    //        return rootApp.GetEnumerator();
    //    }
    //}

}