using ArduinoCore5.Models;
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
    [Route("api/nicu/")]
    public class nicuController
    {
        private readonly IConfiguration _configuration;

        public nicuController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select curr_date,doctor_id,nurse_id,baby_id,incubator_id,temperature_reading,humidity_reading,oxygen_reading,co2_reading,light_state,heartbeat_reading from incubator_db.nicu";

            DataTable table = new();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new(query, mycon))
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


        [HttpPost]
        [Route("Add")]
        public JsonResult Post(nicu main)
        {
            string query = @"INSERT INTO incubator_db.nicu (curr_date,doctor_id,nurse_id,baby_id,incubator_id,temperature_reading,humidity_reading,oxygen_reading,co2_reading,light_state,heartbeat_reading) VALUES (@curr_date,@doctor_id,@nurse_id,@baby_id,@incubator_id,@temperature_reading,@humidity_reading,@oxygen_reading,@co2_reading,@light_state,@heartbeat_reading); ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                Console.WriteLine("Open done");
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@curr_date", main.curr_date);
                    myCommand.Parameters.AddWithValue("@doctor_id", main.doctor_id);
                    myCommand.Parameters.AddWithValue("@nurse_id", main.nurse_id);
                    myCommand.Parameters.AddWithValue("@baby_id", main.baby_id);
                    myCommand.Parameters.AddWithValue("@incubator_id", main.incubator_id);
                    myCommand.Parameters.AddWithValue("@temperature_reading", main.temperature_reading);
                    myCommand.Parameters.AddWithValue("@humidity_reading", main.humidity_reading);
                    myCommand.Parameters.AddWithValue("@oxygen_reading", main.oxygen_reading);
                    myCommand.Parameters.AddWithValue("@co2_reading", main.co2_reading);
                    myCommand.Parameters.AddWithValue("@light_state", main.light_state);
                    myCommand.Parameters.AddWithValue("@heartbeat_reading", main.heartbeat_reading);
                    
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
