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
    [Route("api/doctor/")]
    public class doctor_tbController
    {
        private readonly IConfiguration _configuration;

        public doctor_tbController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select doctor_id,doctor_firstname,doctor_lastname,doctor_username,doctor_password,doctor_email,doctor_mobile,doctor_insertion_date,doctor_last_modification from incubator_db.doctor_tb";

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
        public JsonResult DeleteRecord(int doctor_id)
        {
            //DELETE FROM `movies` WHERE `movie_id` = 18;
            string query = @" DELETE FROM incubator_db.doctor_tb WHERE doctor_id = " + doctor_id;
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
        public JsonResult Post(doctor_tb main)
        {
            DateTime now = DateTime.Now;
            string query = @"INSERT INTO incubator_db.doctor_tb (doctor_id,doctor_firstname,doctor_lastname,doctor_username,doctor_password,doctor_email,doctor_mobile,doctor_insertion_date,doctor_last_modification) VALUES (@doctor_id,@doctor_firstname,@doctor_lastname,@doctor_username,@doctor_password,@doctor_email,@doctor_mobile,@doctor_insertion_date,@doctor_last_modification); ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ArduinoCoreConn");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                Console.WriteLine("Open done");
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@doctor_id", main.doctor_id);
                    myCommand.Parameters.AddWithValue("@doctor_firstname", main.doctor_firstname);
                    myCommand.Parameters.AddWithValue("@doctor_lastname", main.doctor_lastname);
                    myCommand.Parameters.AddWithValue("@doctor_username", main.doctor_username);
                    myCommand.Parameters.AddWithValue("@doctor_password", main.doctor_password);
                    myCommand.Parameters.AddWithValue("@doctor_email", main.doctor_email);
                    myCommand.Parameters.AddWithValue("@doctor_mobile", main.doctor_mobile);
                    myCommand.Parameters.AddWithValue("@doctor_insertion_date", now);
                    myCommand.Parameters.AddWithValue("@doctor_last_modification", now);
                    
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
