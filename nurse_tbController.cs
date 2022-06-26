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
    [Route("api/nurse/")]
    public class nurse_tbController
    {
        private readonly IConfiguration _configuration;

        public nurse_tbController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select nurse_id, nurse_firstname, nurse_lastname, nurse_username, nurse_password from incubator_db.nurse_tb";

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

        [Route("deleteRecord")]
        // Create new endpoint
        [HttpDelete]  //Endpoint will respond to HTTP get request.
        public JsonResult DeleteRecord(int nurse_id)
        {
            //DELETE FROM `movies` WHERE `movie_id` = 18;
            string query = @" DELETE FROM incubator_db.nurse_tb WHERE nurse_id = " + nurse_id;
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

            return new JsonResult("Deleted!");
        }

        [HttpPost]
        [Route("Add")]
        public JsonResult Post(nurse_tb main)
        {
            string query = @"INSERT INTO incubator_db.nurse_tb (nurse_id, nurse_firstname, nurse_lastname, nurse_username, nurse_password) VALUES (@nurse_id, @nurse_firstname, @nurse_lastname, @nurse_username, @nurse_password); ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                Console.WriteLine("Open done");
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@nurse_id", main.nurse_id);
                    myCommand.Parameters.AddWithValue("@nurse_firstname", main.nurse_firstname);
                    myCommand.Parameters.AddWithValue("@nurse_lastname", main.nurse_lastname);
                    myCommand.Parameters.AddWithValue("@nurse_username", main.nurse_username);
                    myCommand.Parameters.AddWithValue("@nurse_password", main.nurse_password);

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
