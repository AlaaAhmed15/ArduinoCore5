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
    [Route("api/incubator/")]
    public class incubator_tbController
    {
        private readonly IConfiguration _configuration;

        public incubator_tbController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select incubator_id,incubator_state,number_of_sensors,temperature_sensor,humidity_sensor,oxygen_sensor,co2_sensor,heartbeat_sensor,light_sensor from incubator_db.incubator_tb";

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
        public JsonResult Post(incubator_tb main)
        {
            string query = @"INSERT INTO incubator_db.incubator_tb (incubator_id,incubator_state,number_of_sensors,temperature_sensor,humidity_sensor,oxygen_sensor,co2_sensor,heartbeat_sensor,light_sensor) VALUES (@incubator_id,@incubator_state,@number_of_sensors,@temperature_sensor,@humididty_sensor,@oxygen_sensor,@co2_sensor,@heartbeat_sensor,@light_sensor); ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                Console.WriteLine("Open done");
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@incubator_id", main.incubator_id);
                    myCommand.Parameters.AddWithValue("@incubator_state", main.incubator_state);
                    myCommand.Parameters.AddWithValue("@number_of_sensors", main.number_of_sensors);
                    myCommand.Parameters.AddWithValue("@temperature_sensor", main.temperature_sensor);
                    myCommand.Parameters.AddWithValue("@humididty_sensor", main.humidity_sensor);
                    myCommand.Parameters.AddWithValue("@oxygen_sensor", main.oxygen_sensor);
                    myCommand.Parameters.AddWithValue("@co2_sensor", main.co2_sensor);
                    myCommand.Parameters.AddWithValue("@heartbeat_sensor", main.heartbeat_sensor);
                    myCommand.Parameters.AddWithValue("@light_sensor", main.light_sensor);
                   
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
