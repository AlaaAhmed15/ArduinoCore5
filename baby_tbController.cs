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
    [Route("api/baby/")]
    public class baby_tbController
    {
        private readonly IConfiguration _configuration;

        public baby_tbController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select baby_id,doctor_id,nurse_id,baby_firstname,baby_lastname,baby_birthDate,baby_entry_date,parents_phone_number,baby_reason_of_entry,baby_medical_history from incubator_db.baby_tb";

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
        public JsonResult DeleteRecord(int baby_id)
        {
            //DELETE FROM `movies` WHERE `movie_id` = 18;
            string query = @" DELETE FROM incubator_db.baby_tb WHERE baby_id = " + baby_id;
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
        public JsonResult Post(baby_tb main)
        {
            DateTime now = DateTime.Now;
            string query = @"INSERT INTO incubator_db.baby_tb (baby_id,doctor_id,nurse_id,baby_firstname,baby_lastname,baby_birthDate,baby_entry_date,parents_phone_number,baby_reason_of_entry,baby_medical_history) VALUES (@baby_id,@doctor_id,@nurse_id,@baby_firstname,@baby_lastname,@baby_birthDate,@baby_entry_date,@parents_phone_number,@baby_reason_of_entry,@baby_medical_history); ";

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
                    myCommand.Parameters.AddWithValue("@doctor_id", main.doctor_id);
                    myCommand.Parameters.AddWithValue("@nurse_id", main.nurse_id);
                    myCommand.Parameters.AddWithValue("@baby_firstname", main.baby_firstname);
                    myCommand.Parameters.AddWithValue("@baby_lastname", main.baby_lastname);
                    myCommand.Parameters.AddWithValue("@baby_birthDate", now);
                    myCommand.Parameters.AddWithValue("@baby_entry_date", now);
                    myCommand.Parameters.AddWithValue("@parents_phone_number", main.parents_phone_number);
                    myCommand.Parameters.AddWithValue("@baby_reason_of_entry", main.baby_reason_of_entry);
                    myCommand.Parameters.AddWithValue("@baby_medical_history", main.baby_medical_history);
                    
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
