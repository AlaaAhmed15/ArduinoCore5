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
    [Route("api/doctorNotes/")]
    public class doctor_notesController
    {
        private readonly IConfiguration _configuration;

        public doctor_notesController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select doctor_id,baby_id,note_text,note_date,note_state,note_progress from incubator_db.doctor_notes";

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
        public JsonResult Post(doctor_notes main)
        {
            string query = @"INSERT INTO incubator_db.doctor_notes (doctor_id,baby_id,note_text,note_date,note_state,note_progress) VALUES (@doctor_id,@baby_id,@note_text,@note_date,@note_state,@note_progress); ";

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
                    myCommand.Parameters.AddWithValue("@baby_id", main.baby_id);
                    myCommand.Parameters.AddWithValue("@note_text", main.note_text);
                    myCommand.Parameters.AddWithValue("@note_date", main.note_date);
                    myCommand.Parameters.AddWithValue("@note_state", main.note_state);
                    myCommand.Parameters.AddWithValue("@note_progress", main.note_progress);
                    
                    
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
