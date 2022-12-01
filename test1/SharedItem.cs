using System;
using System.Collections.Generic;

namespace test1
{  
    public class DisplayItem
    {
        public string temperature { set; get; }
        public string humidity { set; get; }
        public string voltage { set; get; }
        public string pressure { set; get; }
        public string gradient { set; get; }
        public string uuid { set; get; }
        public string datetime { set; get; }
    }  

     public class Item
    {
        public string sensorid { set; get; }
        public string uuid { set; get; }
        public string data_id { set; get; }
        public string data { set; get; }
        public string datetime { set; get; }
    } 

    public class KeyUUID{
        public string display_name { set; get; }
        public string uuid { set; get; }

        public KeyUUID(string _display_name, string _uuid){
              display_name=_display_name;
              uuid=_uuid;
        }

        public string GetDisplayName(){
            return display_name;
        }

        public bool IsEqual(string _str){
            if(uuid==_str) return true;
            if(display_name==_str) return true;
            return false;
        }
    }
}
