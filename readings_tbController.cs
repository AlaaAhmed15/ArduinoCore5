using ArduinoCore5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoCore5.Controllers
{
    [ApiController]
    [Route("api/readings/")]
    public class readings_tbController: ControllerBase
    {
        private readonly IConfiguration _configuration;

        public readings_tbController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select baby_id,temperature_reading,temperature_time,humidity_reading,humidity_time,oxygen_reading,oxygen_time,co2_reading,co2_time,heartbeat_reading,heartbeat_time from incubator_db.readings_tb";

            DataTable table = new();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new (query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    Console.WriteLine("query run ok");

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult(table);
        }

        [Route("GetLastRecord")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult GetRecord()
        {
            string query = @" SELECT * FROM incubator_db.readings_tb ORDER BY baby_id DESC LIMIT 1 ";
            DataTable table = new();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new (query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    Console.WriteLine("query run ok");

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult(table);
        }
        

        /*
         SELECT * FROM Table ORDER BY ID DESC LIMIT 1
         */

        [Route("Add")]
        [HttpPost]
        public JsonResult Post(readings_tb main)
        {
            DateTime now = DateTime.Now;
            string query = @"INSERT INTO incubator_db.readings_tb (baby_id,temperature_reading,temperature_time,humidity_reading,humidity_time,oxygen_reading,oxygen_time,co2_reading,co2_time,heartbeat_reading,heartbeat_time) VALUES (@baby_id,@temperature_reading,@temperature_time,@humidity_reading,@humidity_time,@oxygen_reading,@oxygen_time,@co2_reading,@co2_time,@heartbeat_reading,@heartbeat_time); ";


            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                Console.WriteLine("Open done");
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@baby_id", main.baby_id);
                    myCommand.Parameters.AddWithValue("@temperature_reading", main.temperature_reading);
                    myCommand.Parameters.AddWithValue("@temperature_time", now);
                    myCommand.Parameters.AddWithValue("@humidity_reading", main.humidity_reading);
                    myCommand.Parameters.AddWithValue("@humidity_time", now);
                    myCommand.Parameters.AddWithValue("@oxygen_reading", main.oxygen_reading);
                    myCommand.Parameters.AddWithValue("@oxygen_time", now);
                    myCommand.Parameters.AddWithValue("@co2_reading", main.co2_reading);
                    myCommand.Parameters.AddWithValue("@co2_time", now);
                    myCommand.Parameters.AddWithValue("@heartbeat_reading", main.heartbeat_reading);
                    myCommand.Parameters.AddWithValue("@heartbeat_time", now);
                    

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    Console.WriteLine("query run ok");

                    myReader.Close();
                    mycon.Close();
                }

            }

            return new JsonResult("Added Ok");
        }
    }
}
