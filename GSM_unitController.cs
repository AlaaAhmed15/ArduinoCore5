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
    [Route("api/gsm/")]
    public class GSM_unitController
    {
        private readonly IConfiguration _configuration;

        public GSM_unitController(IConfiguration configuration) => _configuration = configuration;


        [Route("getAllrecords")]
        // Create new endpoint
        [HttpGet]  //Endpoint will respond to HTTP get request.
        public JsonResult Get()
        {
            string query = @"select doctor_id,doctor_mobile,baby_id,msg_txt,msg_date_time from incubator_db.gsm_unit";

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


        [Route("Add")]
        [HttpPost]
        public JsonResult Post(GSM_unit main)
        {
            DateTime now = DateTime.Now;
            string query = @"INSERT INTO incubator_db.GSM_unit (doctor_id,doctor_mobile,baby_id,msg_txt,msg_date_time) VALUES (@doctor_id,@doctor_mobile,@baby_id,@msg_txt,@msg_date_time); ";


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
                    myCommand.Parameters.AddWithValue("@doctor_mobile", main.doctor_mobile);
                    myCommand.Parameters.AddWithValue("@baby_id", main.baby_id);
                    myCommand.Parameters.AddWithValue("@msg_txt", main.msg_txt);
                    myCommand.Parameters.AddWithValue("@msg_date_time", now);

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
