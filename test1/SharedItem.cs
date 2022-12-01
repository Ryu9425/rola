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
        public string standard { set; get; }
        public bool is_gradient { set; get; }
        public bool is_humidity { set; get; }
        public bool is_temperature { set; get; }
        public bool is_pressure { set; get; }
        public bool is_voltage { set; get; }

        public KeyUUID(){
            display_name="";
            uuid="";
            standard="";
            is_gradient=false;
            is_humidity=false;
            is_temperature=false;
            is_pressure=false;
            is_voltage=false;
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
