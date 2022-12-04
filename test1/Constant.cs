using System;
using System.Collections.Generic;

namespace test1
{
    public static class Constant
    {
        public static int selected_uuid_index = 6;
        public static List<KeyUUID> key_uuid_list = new List<KeyUUID>();

        public static string web_api="";
        public static int connection_time=10;
        public static int connection_interval=20;
        public static string store_path="";
        public static int display_count=5;

        public static string Sensor_Receive_Date(string binary_number_str)
        {
            var ttt = Convert.ToString(Int32.Parse(binary_number_str), 2).PadLeft(15, '0');
            var year_middle = ttt.Substring(0, 6);

            var year = Convert.ToInt32(year_middle, 2);

            var month_middle = ttt.Substring(6, 4);
            int month = Convert.ToInt32(month_middle, 2);
            string month_result = month.ToString().PadLeft(2, '0');

            var day_middle = ttt.Substring(10, 5);
            var day = Convert.ToInt32(day_middle, 2);
            string day_result = day.ToString().PadLeft(2, '0');

            var sensor_date = "20" + year + "-" + month_result + "-" + day_result;

            return sensor_date;
        }
        public static string Sensor_Receive_Time(string binary_number_str)
        {
            var ttt = Convert.ToString(Int32.Parse(binary_number_str), 2).PadLeft(16, '0');

            var hour_middle = ttt.Substring(0, 5);
            int hour = Convert.ToInt32(hour_middle, 2);
            string hour_result = hour.ToString().PadLeft(2, '0');

            var minute_middle = ttt.Substring(5, 6);
            int minute = Convert.ToInt32(minute_middle, 2);
            string minute_result = minute.ToString().PadLeft(2, '0');

            var second_middle = ttt.Substring(11, 5);
            var second = 2 * Convert.ToInt32(second_middle, 2);
            string second_result = second.ToString().PadLeft(2, '0');

            var sensor_time = hour_result + ":" + minute_result + ":" + second_result;
            return sensor_time;
        }
    }

}
